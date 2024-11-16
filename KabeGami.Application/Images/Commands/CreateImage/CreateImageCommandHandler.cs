using ErrorOr;
using KabeGami.Application.Common.Interfaces.Persistence;
using KabeGami.Application.Common.Interfaces.Services;
using KabeGami.Application.Images.Results;
using MapsterMapper;
using MediatR;

namespace KabeGami.Application.Images.Commands.CreateImage;
internal sealed class CreateImageCommandHandler(
    IImageRepository imageRepository,
    IMapper mapper,
    IOperatingSystemService operatingSystemService,
    IUnitOfWork unitOfWork)
        : IRequestHandler<CreateImageCommand, ErrorOr<ImageResult>>
{
    private readonly IImageRepository _imageRepository = imageRepository;
    private readonly IMapper _mapper = mapper;
    private readonly IOperatingSystemService _operatingSystemService = operatingSystemService;
    private readonly IUnitOfWork _unitOfWork = unitOfWork;

    public async Task<ErrorOr<ImageResult>> Handle(CreateImageCommand request, CancellationToken cancellationToken)
    {
        var image = Domain.Images.Image.Create(
            Path.GetExtension(request.ImagePath));
        if (image.IsError)
        {
            return image.Errors;
        }

        await _imageRepository.AddImageAsync(image.Value, cancellationToken);

        var imageResult = _mapper.Map<ImageResult>(image.Value);

        var imageSystemSaved = await _operatingSystemService.MoveImage(
            imageResult,
            request.ImagePath,
            cancellationToken);
        if (imageSystemSaved.IsError)
        {
            return imageSystemSaved.Errors;
        }

        await _unitOfWork.SaveChangeAsync(cancellationToken);

        return imageResult;
    }
}

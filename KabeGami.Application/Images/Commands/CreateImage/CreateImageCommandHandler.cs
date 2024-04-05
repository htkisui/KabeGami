using ErrorOr;
using KabeGami.Application.Common.Interfaces.Persistence;
using KabeGami.Application.Common.Interfaces.Services;
using KabeGami.Application.Images.Common;
using KabeGami.Domain.Images;
using MapsterMapper;
using MediatR;

namespace KabeGami.Application.Images.Commands.CreateImage;
internal sealed class CreateImageCommandHandler(
    IImageRepository imageRepository,
    IMapper mapper,
    IOperatingSystemService operatingSystemService,
    IUnitOfWork unitOfWork)
    : IRequestHandler<CreateImageCommand, ErrorOr<Unit>>
{
    private readonly IImageRepository _imageRepository = imageRepository;
    private readonly IMapper _mapper = mapper;
    private readonly IOperatingSystemService _operatingSystemService = operatingSystemService;
    private readonly IUnitOfWork _unitOfWork = unitOfWork;

    public async Task<ErrorOr<Unit>> Handle(CreateImageCommand request, CancellationToken cancellationToken)
    {
        var image = Image.Create(
            request.ImageExtension,
            request.ImageCategory,
            request.IsSFW);
        if (image.IsError)
        {
            return image.Errors;
        }
        await _imageRepository.AddAsync(image.Value);

        var imageResult = _mapper.Map<ImageResult>(image.Value);
        var saveImage = await _operatingSystemService.MoveImage(
            imageResult,
            request.ImageSourcePath);
        if (saveImage.IsError)
        {
            return saveImage.Errors;
        }

        await _unitOfWork.SaveChangeAsync(cancellationToken);

        return Unit.Value;
    }
}

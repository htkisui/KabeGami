using ErrorOr;
using KabeGami.Application.Common.Interfaces.Persistence;
using KabeGami.Application.Common.Interfaces.Services;
using KabeGami.Application.Images.Results;
using KabeGami.Domain.Images.ValueObjects;
using MapsterMapper;
using MediatR;

namespace KabeGami.Application.Images.Commands.DeleteImage;
internal sealed class DeleteImageCommandHandler(
    IImageRepository imageRepository,
    IMapper mapper,
    IOperatingSystemService operatingSystemService,
    IUnitOfWork unitOfWork)
        : IRequestHandler<DeleteImageCommand, ErrorOr<Unit>>
{
    private readonly IImageRepository _imageRepository = imageRepository;
    private readonly IMapper _mapper = mapper;
    private readonly IOperatingSystemService _operatingSystemService = operatingSystemService;
    private readonly IUnitOfWork _unitOfWork = unitOfWork;

    public async Task<ErrorOr<Unit>> Handle(DeleteImageCommand request, CancellationToken cancellationToken)
    {
        var imageId = ImageId.Create(request.ImageGuid);
        var imageDeleted = await _imageRepository.RemoveImageByIdAsync(imageId, cancellationToken);
        if (imageDeleted.IsError)
        {
            return imageDeleted.Errors;
        }

        var imageDeletedResult = _mapper.Map<ImageResult>(imageDeleted.Value);
        var imageSystemDeleted = await _operatingSystemService.DeleteImage(imageDeletedResult, cancellationToken);

        await _unitOfWork.SaveChangeAsync(cancellationToken);

        return Unit.Value;
    }
}

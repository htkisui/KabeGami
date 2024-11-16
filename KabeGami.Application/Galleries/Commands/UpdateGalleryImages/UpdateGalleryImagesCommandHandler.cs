using ErrorOr;
using KabeGami.Application.Common.Interfaces.Persistence;
using KabeGami.Application.Galleries.Results;
using KabeGami.Domain.Galleries.ValueObjects;
using KabeGami.Domain.Images.ValueObjects;
using MapsterMapper;
using MediatR;

namespace KabeGami.Application.Galleries.Commands.UpdateGalleryImages;
internal sealed class UpdateGalleryImagesCommandHandler(
    IGalleryRepository galleryRepository,
    IImageRepository imageRepository,
    IMapper mapper,
    IUnitOfWork unitOfWork)
        : IRequestHandler<UpdateGalleryImagesCommand, ErrorOr<GalleryResult>>
{
    private readonly IGalleryRepository _galleryRepository = galleryRepository;
    private readonly IImageRepository _imageRepository = imageRepository;
    private readonly IMapper _mapper = mapper;
    private readonly IUnitOfWork _unitOfWork = unitOfWork;

    public async Task<ErrorOr<GalleryResult>> Handle(UpdateGalleryImagesCommand request, CancellationToken cancellationToken)
    {
        var galleryId = GalleryId.Create(request.Payload.GalleryGuid);

        var gallery = await _galleryRepository.GetGalleryByIdAsync(galleryId, cancellationToken);
        if (gallery.IsError)
        {
            return gallery.Errors;
        }
        
        var imageIds = request.Payload.ImageGuids.Select(ImageId.Create).ToList();
        var imageIdsExisted = await _imageRepository.IsImageIdsExistAsync(imageIds, cancellationToken);
        if (imageIdsExisted.IsError)
        {
            return imageIdsExisted.Errors;
        }

        var imageIdsUpdated = gallery.Value.SetImageIds(imageIds);

        var galleryResult = _mapper.Map<GalleryResult>(gallery.Value);

        await _unitOfWork.SaveChangeAsync(cancellationToken);

        return galleryResult;
    }
}

using ErrorOr;
using KabeGami.Application.Common.Interfaces.Persistence;
using KabeGami.Domain.Galleries.ValueObjects;
using KabeGami.Domain.Images.ValueObjects;
using MediatR;

namespace KabeGami.Application.Galleries.Commands.AddImageIdsToSubGallery;
internal sealed class AddImageIdsToSubGalleryCommandHandler(
    IGalleryRepository galleryRepository,
    IImageRepository imageRepository,
    IUnitOfWork unitOfWork)
        : IRequestHandler<AddImageIdsToSubGalleryCommand, ErrorOr<Unit>>
{
    private readonly IGalleryRepository _galleryRepository = galleryRepository;
    private readonly IImageRepository _imageRepository = imageRepository;
    private readonly IUnitOfWork _unitOfWork = unitOfWork;

    public async Task<ErrorOr<Unit>> Handle(AddImageIdsToSubGalleryCommand request, CancellationToken cancellationToken)
    {
        var gallery = await _galleryRepository.GetGalleryByIdAsync(GalleryId.Create(request.GalleryGuid), cancellationToken);
        if (gallery.IsError)
        {
            return gallery.Errors;
        }

        var imageIds = request.ImageGuids.Select(ImageId.Create).ToList();
        var imageIdsExisted = await _imageRepository.IsImageIdsExist(imageIds, cancellationToken);
        if (imageIdsExisted.IsError)
        {
            return imageIdsExisted.Errors;
        }

        var imageIdsAdded = gallery.Value.AddImageIdsToSubGallery(SubGalleryId.Create(request.SubGalleryGuid), imageIds);
        if (imageIdsAdded.IsError)
        {
            return imageIdsAdded.Errors;
        }

        await _unitOfWork.SaveChangeAsync(cancellationToken);
        return Unit.Value;
    }
}

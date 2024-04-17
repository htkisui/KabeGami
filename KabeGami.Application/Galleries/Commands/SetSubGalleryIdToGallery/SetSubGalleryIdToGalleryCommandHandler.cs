using ErrorOr;
using KabeGami.Application.Common.Interfaces.Persistence;
using KabeGami.Domain.Galleries.ValueObjects;
using MediatR;

namespace KabeGami.Application.Galleries.Commands.SetSubGalleryIdToGallery;
internal sealed class SetSubGalleryIdToGalleryCommandHandler(
    IGalleryRepository galleryRepository,
    IUnitOfWork unitOfWork)
        : IRequestHandler<SetSubGalleryIdToGalleryCommand, ErrorOr<Unit>>
{
    private readonly IGalleryRepository _galleryRepository = galleryRepository;
    private readonly IUnitOfWork _unitOfWork = unitOfWork;

    public async Task<ErrorOr<Unit>> Handle(SetSubGalleryIdToGalleryCommand request, CancellationToken cancellationToken)
    {
        var galleryId = GalleryId.Create(request.GalleryGuid);
        var gallery = await _galleryRepository.GetGalleryByIdAsync(galleryId, cancellationToken);
        if (gallery.IsError)
        {
            return gallery.Errors;
        }

        var subGalleryId = SubGalleryId.Create(request.SubGalleryGuid);
        var subGalleryIdSet = gallery.Value.SetSubGalleryId(subGalleryId);
        if (subGalleryIdSet.IsError)
        {
            return subGalleryIdSet.Errors;
        }

        await _unitOfWork.SaveChangeAsync(cancellationToken);

        return Unit.Value;
    }
}

using ErrorOr;
using KabeGami.Application.Common.Errors;
using KabeGami.Application.Common.Interfaces.Persistence;
using KabeGami.Application.Common.Interfaces.Services;
using KabeGami.Domain.Galleries.DomainEvents;
using KabeGami.Domain.Galleries.ValueObjects;
using MediatR;

namespace KabeGami.Application.Galleries.Commands.DeleteGallery;
internal sealed class DeleteGalleryCommandHandler(
    IGalleryRepository galleryRepository,
    IPublisher publisher,
    IUnitOfWork unitOfWork,
    IWallpaperSystemService wallpaperSystemService)
        : IRequestHandler<DeleteGalleryCommand, ErrorOr<Unit>>
{
    private readonly IGalleryRepository _galleryRepository = galleryRepository;
    private readonly IPublisher _publisher = publisher;
    private readonly IUnitOfWork _unitOfWork = unitOfWork;
    private readonly IWallpaperSystemService _wallpaperSystemService = wallpaperSystemService;

    public async Task<ErrorOr<Unit>> Handle(DeleteGalleryCommand request, CancellationToken cancellationToken)
    {
        if (_wallpaperSystemService.GalleryGuid == request.GalleryGuid)
        {
            return Errors.DeleteGalleryCommandHandler.GalleryIsAlreadyUsed(request.GalleryGuid);
        }

        var galleryId = GalleryId.Create(request.GalleryGuid);
        var galleryDeleted = await _galleryRepository.RemoveGalleryAsync(galleryId, cancellationToken);
        if (galleryDeleted.IsError)
        {
            return galleryDeleted.Errors;
        }

        await _unitOfWork.SaveChangeAsync(cancellationToken);

        await _publisher.Publish(new GalleryDeletedDomainEvent(Guid.NewGuid(), galleryId), cancellationToken);

        return Unit.Value;
    }
}

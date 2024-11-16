using ErrorOr;
using KabeGami.Application.Common.Interfaces.Persistence;
using KabeGami.Domain.Galleries.ValueObjects;
using MediatR;

namespace KabeGami.Application.Galleries.Commands.DeleteGallery;
internal sealed class DeleteGalleryCommandHandler(
    IGalleryRepository galleryRepository,
    IHomeRepository homeRepository,
    IUnitOfWork unitOfWork)
        : IRequestHandler<DeleteGalleryCommand, ErrorOr<Unit>>
{
    private readonly IGalleryRepository _galleryRepository = galleryRepository;
    private readonly IHomeRepository _homeRepository = homeRepository;
    private readonly IUnitOfWork _unitOfWork = unitOfWork;

    public async Task<ErrorOr<Unit>> Handle(DeleteGalleryCommand request, CancellationToken cancellationToken)
    {
        var galleryId = GalleryId.Create(request.GalleryGuid);

        var home = await _homeRepository.GetHomeAsync(cancellationToken);
        if (home.IsError)
        {
            return home.Errors;
        }

        var galleryAlreadyAssigned = home.Value.IsGalleryAlreadyAssigned(galleryId);
        if (galleryAlreadyAssigned.IsError)
        {
            return galleryAlreadyAssigned.Errors;
        }

        var galleryDeleted = await _galleryRepository.RemoveGalleryByIdAsync(galleryId, cancellationToken);
        if (galleryDeleted.IsError)
        {
            return galleryDeleted.Errors;
        }

        await _unitOfWork.SaveChangeAsync(cancellationToken);

        return Unit.Value;
    }
}

using ErrorOr;
using KabeGami.Application.Common.Errors;
using KabeGami.Application.Common.Interfaces.Persistence;
using KabeGami.Application.Common.Interfaces.Services;
using KabeGami.Domain.Galleries.ValueObjects;
using MediatR;

namespace KabeGami.Application.Galleries.Commands.DeleteSubGallery;
internal sealed class DeleteSubGalleryCommandHandler(
    IGalleryRepository galleryRepository,
    IUnitOfWork unitOfWork,
    IWallpaperSystemService wallpaperSystemService)
        : IRequestHandler<DeleteSubGalleryCommand, ErrorOr<Unit>>
{
    private readonly IGalleryRepository _galleryRepository = galleryRepository;
    private readonly IUnitOfWork _unitOfWork = unitOfWork;
    private readonly IWallpaperSystemService _wallpaperSystemService = wallpaperSystemService;

    public async Task<ErrorOr<Unit>> Handle(DeleteSubGalleryCommand request, CancellationToken cancellationToken)
    {
        if (_wallpaperSystemService.SubGalleryGuid == request.SubGalleryGuid)
        {
            return Errors.DeleteSubGalleryCommandHandler.SubGalleryIsAlreadyUsed(request.SubGalleryGuid);
        } 

        var galleryId = GalleryId.Create(request.GalleryGuid);
        var gallery = await _galleryRepository.GetGalleryByIdAsync(galleryId, cancellationToken);
        if (gallery.IsError)
        {
            return gallery.Errors;
        }

        var subGalleryId = SubGalleryId.Create(request.GalleryGuid);
        gallery.Value.DeleteSubGallery(subGalleryId);

        await _unitOfWork.SaveChangeAsync(cancellationToken);

        return Unit.Value;
    }
}

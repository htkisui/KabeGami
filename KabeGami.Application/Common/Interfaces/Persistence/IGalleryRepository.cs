
using ErrorOr;
using KabeGami.Domain.Galleries;
using KabeGami.Domain.Galleries.ValueObjects;

namespace KabeGami.Application.Common.Interfaces.Persistence;
public interface IGalleryRepository
{
    Task AddGalleryAsync(Gallery gallery, CancellationToken cancellationToken);
    Task<List<Gallery>> GetGalleriesAsync(CancellationToken cancellationToken);
    Task<List<GalleryId>> GetGalleryIdsAsync(CancellationToken cancellationToken);
    Task<ErrorOr<Gallery>> GetGalleryByIdAsync(GalleryId galleryId, CancellationToken cancellationToken);
    Task<ErrorOr<Gallery>> GetGalleryByNameAsync(string name, CancellationToken cancellationToken);
    Task<ErrorOr<bool>> IsGalleryNameAvailableAsync(string name, CancellationToken cancellationToken);
    Task<ErrorOr<Gallery>> RemoveGalleryByIdAsync(GalleryId galleryId, CancellationToken cancellationToken);
}

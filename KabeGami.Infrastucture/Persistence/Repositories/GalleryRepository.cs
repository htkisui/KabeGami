using ErrorOr;
using KabeGami.Application.Common.Errors.Persistence;
using KabeGami.Application.Common.Interfaces.Persistence;
using KabeGami.Domain.Galleries;
using KabeGami.Domain.Galleries.ValueObjects;
using Microsoft.EntityFrameworkCore;

namespace KabeGami.Infrastucture.Persistence.Repositories;
internal sealed class GalleryRepository(
    ApplicationDbContext context)
        : IGalleryRepository
{
    private readonly ApplicationDbContext _context = context;

    public async Task AddGalleryAsync(Gallery gallery, CancellationToken cancellationToken)
    {
        await _context.Galleries.AddAsync(gallery, cancellationToken);
    }

    public async Task<List<Gallery>> GetGalleriesAsync(CancellationToken cancellationToken)
    {
        return await _context.Galleries.ToListAsync(cancellationToken);
    }

    public async Task<ErrorOr<Gallery>> GetGalleryByIdAsync(GalleryId galleryId, CancellationToken cancellationToken)
    {
        var gallery = await _context.Galleries.SingleOrDefaultAsync(g => g.Id == galleryId, cancellationToken);
        if (gallery is null)
        {
            return Errors.GalleryRepository.GalleryNotFound(galleryId);
        }
        return gallery;
    }

    public async Task<List<GalleryId>> GetGalleryIdsAsync(CancellationToken cancellationToken)
    {
        return await _context.Galleries.Select(g => g.Id).ToListAsync(cancellationToken);
    }

    public async Task<ErrorOr<Gallery>> GetGalleryByNameAsync(string name, CancellationToken cancellationToken)
    {
        var gallery = await _context.Galleries.SingleOrDefaultAsync(g => g.Name == name, cancellationToken);
        if (gallery is null)
        {
            return Errors.GalleryRepository.GalleryNotFound(name);
        }
        return gallery;
    }

    public async Task<ErrorOr<bool>> IsGalleryNameAvailableAsync(string name, CancellationToken cancellationToken)
    {
        if (await _context.Galleries.AnyAsync(g => g.Name == name, cancellationToken))
        {
            return Errors.GalleryRepository.GalleryNameNotAvailable(name);
        }

        return true;
    }

    public async Task<ErrorOr<Gallery>> RemoveGalleryByIdAsync(GalleryId galleryId, CancellationToken cancellationToken)
    {
        var gallery = await _context.Galleries.SingleOrDefaultAsync(g => g.Id == galleryId, cancellationToken);
        if (gallery is null)
        {
            return Errors.GalleryRepository.GalleryNotFound(galleryId);
        }

        _context.Galleries.Remove(gallery);

        return gallery;
    }
}

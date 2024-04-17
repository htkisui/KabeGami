using ErrorOr;
using KabeGami.Application.Common.Errors;
using KabeGami.Application.Common.Interfaces.Persistence;
using KabeGami.Domain.Galleries;
using KabeGami.Domain.Galleries.ValueObjects;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace KabeGami.Infrastructure.Persistence.Repositories;
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

    public async Task<ErrorOr<Gallery>> GetGalleryByIdAsync(
        GalleryId galleryId,
        CancellationToken cancellationToken)
    {
        var gallery = await _context.Galleries
            .Include(g => g.SubGalleries)
            .FirstOrDefaultAsync(g => g.Id == galleryId, cancellationToken);
        if (gallery is null)
        {
            return Errors.GalleryRepository.GalleryNotFound(galleryId);
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

    public async Task<ErrorOr<Gallery>> RemoveGalleryAsync(GalleryId galleryId, CancellationToken cancellationToken)
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

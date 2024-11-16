using ErrorOr;
using KabeGami.Application.Common.Errors.Persistence;
using KabeGami.Application.Common.Interfaces.Persistence;
using KabeGami.Domain.Images;
using KabeGami.Domain.Images.ValueObjects;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace KabeGami.Infrastucture.Persistence.Repositories;
internal sealed class ImageRepository(
    ApplicationDbContext context)
    : IImageRepository
{
    private readonly ApplicationDbContext _context = context;

    public async Task AddImageAsync(Image image, CancellationToken cancellationToken)
    {
        await _context.Images.AddAsync(image, cancellationToken);
    }

    public async Task<List<Image>> GetImagesAsync(CancellationToken cancellationToken)
    {
        return await _context.Images.ToListAsync(cancellationToken);
    }

    public async Task<ErrorOr<List<Image>>> GetImagesByIdsAsync(List<ImageId> imageIds, CancellationToken cancellationToken)
    {
        List<Error> errors = [];
        var images = await _context.Images
            .Where(i => imageIds
            .Contains(i.Id))
            .ToListAsync(cancellationToken);

        var imageIdsNotFound = imageIds
            .Except(images.Select(i => i.Id))
            .ToList();

        foreach (var imageIdNotFound in imageIdsNotFound)
        {
            errors.Add(Errors.ImageRepository.ImageNotFound(imageIdNotFound));
        }

        if (errors.Count > 0)
        {
            return errors;
        }

        return images;
    }

    public async Task<ErrorOr<Unit>> IsImageIdsExistAsync(List<ImageId> imageIds, CancellationToken cancellationToken)
    {
        List<Error> errors = [];

        foreach (var imageId in imageIds)
        {
            if (await _context.Images.AnyAsync(i => i.Id == imageId, cancellationToken) is false)
            {
                errors.Add(Errors.ImageRepository.ImageNotFound(imageId));
            }
        }

        if (errors.Count > 0)
        {
            return errors;
        }

        return Unit.Value;
    }

    public async Task<ErrorOr<Image>> RemoveImageByIdAsync(ImageId imageId, CancellationToken cancellationToken)
    {
        var image = await _context.Images.SingleOrDefaultAsync(i => i.Id == imageId, cancellationToken);
        if (image is null)
        {
            return Errors.ImageRepository.ImageNotFound(imageId);
        }

        _context.Images.Remove(image);

        return image;
    }
}

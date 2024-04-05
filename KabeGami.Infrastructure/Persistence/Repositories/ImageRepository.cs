using KabeGami.Application.Common.Interfaces.Persistence;
using KabeGami.Domain.Images;

namespace KabeGami.Infrastructure.Persistence.Repositories;
internal sealed class ImageRepository(
    ApplicationDbContext context) 
    : IImageRepository
{
    private readonly ApplicationDbContext _context = context;

    public async Task AddAsync(Image image)
    {
        await _context.Images.AddAsync(image);
    }
}

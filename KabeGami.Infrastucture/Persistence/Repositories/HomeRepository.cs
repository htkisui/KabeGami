using ErrorOr;
using KabeGami.Application.Common.Errors.Persistence;
using KabeGami.Application.Common.Interfaces.Persistence;
using KabeGami.Domain.Homes;
using Microsoft.EntityFrameworkCore;

namespace KabeGami.Infrastucture.Persistence.Repositories;
internal sealed class HomeRepository(
    ApplicationDbContext context)
        : IHomeRepository
{
    private readonly ApplicationDbContext _context = context;

    public async Task<ErrorOr<Home>> GetHomeAsync(CancellationToken cancellationToken)
    {
        var home = await _context.Homes.FirstAsync(cancellationToken);
        if (home is null)
        {
            return Errors.HomeRepository.HomeNotFound;
        }
        return home;
    }
}

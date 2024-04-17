using ErrorOr;
using KabeGami.Application.Common.Errors;
using KabeGami.Application.Common.Interfaces.Persistence;
using KabeGami.Domain.KabeGamis;
using Microsoft.EntityFrameworkCore;

namespace KabeGami.Infrastructure.Persistence.Repositories;
internal sealed class KabeGamiCoreRepository(
    ApplicationDbContext context)
        : IKabeGamiCoreRepository
{
    private readonly ApplicationDbContext _context = context;

    public async Task<ErrorOr<KabeGamiCore>> GetKabeGamiCoreAsync(CancellationToken cancellationToken)
    {
        if (await _context.KabeGamiCores.AnyAsync(cancellationToken) is false)
        {
            return Errors.KabeGamiCoreRepository.KabeGamiCoreNotFound;
        }
        return await _context.KabeGamiCores.FirstAsync(cancellationToken);
    }
}

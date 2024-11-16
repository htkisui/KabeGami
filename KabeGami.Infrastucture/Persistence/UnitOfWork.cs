using KabeGami.Application.Common.Interfaces.Persistence;

namespace KabeGami.Infrastucture.Persistence;
internal sealed class UnitOfWork(
    ApplicationDbContext context)
        : IUnitOfWork
{
    private readonly ApplicationDbContext _context = context;
    public async Task SaveChangeAsync(CancellationToken cancellationToken) 
        => await _context.SaveChangesAsync(cancellationToken);
}

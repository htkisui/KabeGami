namespace KabeGami.Application.Common.Interfaces.Persistence;
public interface IUnitOfWork
{
    Task SaveChangeAsync(CancellationToken cancellationToken);
}

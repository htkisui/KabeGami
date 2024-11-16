using ErrorOr;
using KabeGami.Domain.Homes;

namespace KabeGami.Application.Common.Interfaces.Persistence;
public interface IHomeRepository
{
    Task<ErrorOr<Home>> GetHomeAsync(CancellationToken cancellationToken);
}

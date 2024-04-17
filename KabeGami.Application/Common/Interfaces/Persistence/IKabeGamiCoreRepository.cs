using ErrorOr;
using KabeGami.Domain.KabeGamis;

namespace KabeGami.Application.Common.Interfaces.Persistence;
public interface IKabeGamiCoreRepository
{
    Task<ErrorOr<KabeGamiCore>> GetKabeGamiCoreAsync(CancellationToken cancellationToken);
}

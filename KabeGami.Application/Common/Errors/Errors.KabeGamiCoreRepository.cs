using ErrorOr;

namespace KabeGami.Application.Common.Errors;
public static partial class Errors
{
    public static class KabeGamiCoreRepository
    {
        public static Error KabeGamiCoreNotFound => Error.NotFound(
            code: "KabeGamiCoreRepository.KabeGamiCoreNotFound",
            description: "KabeGamiCore is not found.");
    }
}

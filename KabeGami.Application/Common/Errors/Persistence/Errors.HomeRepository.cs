using ErrorOr;

namespace KabeGami.Application.Common.Errors.Persistence;

public static partial class Errors
{
    public static class HomeRepository
    {
        public static Error HomeNotFound => Error.NotFound(
            code: "HomeRepository.HomeNotFound",
            description: "Home is not found.");
    }
}

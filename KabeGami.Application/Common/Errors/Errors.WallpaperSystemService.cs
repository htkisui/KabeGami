using ErrorOr;

namespace KabeGami.Application.Common.Errors;
public static partial class Errors
{
    public static class WallpaperSystemService
    {
        public static Error ImageResultsEmpty => Error.Validation(
            code: "WallpaperSystemService.ImageResultsEmpty",
            description: "Image results is empty.");
    }
}

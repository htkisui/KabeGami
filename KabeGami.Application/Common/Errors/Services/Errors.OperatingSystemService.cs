using ErrorOr;

namespace KabeGami.Application.Common.Errors.Services;
public static partial class Errors
{
    public static class OperatingSystemService
    {
        public static Error DeleteImageFailed => Error.Failure(
            code: "OperatingSystemService.DeleteImageFailed",
            description: "Image cannot be delete.");

        public static Error DirectoryAccessFailed => Error.Validation(
            code: "OperatingSystemService.DirectoryAccessFailed",
            description: "Directory cannot be access.");

        public static Error ImageAccessFailed => Error.Validation(
            code: "OperatingSystemService.ImageAccessFailed",
            description: "Image cannot be access.");

        public static Error MoveImageFailed => Error.Failure(
            code: "OperatingSystemService.MoveImageFailed",
            description: "Image cannot be move.");
    }
}

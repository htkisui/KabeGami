using ErrorOr;

namespace KabeGami.Application.Common.Errors;
public static class Errors
{
    public static class OperatingSystemService
    {
        public static Error CreateDirectoryFailed => Error.Failure(
            code: "OperatingSystemService.CreateDirectoryFailed",
            description: "Directory cannot be created.");
        public static Error DirectoryAccessFailed => Error.Validation(
            code: "OperatingSystemService.DirectoryAccessFailed",
            description: "Directory cannot be accessed.");
        public static Error ImageSourceAccessFailed => Error.Validation(
            code: "OperatingSystemService.ImageSourceAccessFailed",
            description: "Image source cannot be accessed.");
        public static Error MoveImageFailed => Error.Failure(
            code: "OperatingSystemService.MoveImageFailed",
            description: "Image cannot be moved.");
    }
}

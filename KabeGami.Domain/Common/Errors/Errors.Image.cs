using ErrorOr;

namespace KabeGami.Domain.Common.Errors;
public static partial class Errors
{
    public static class Image
    {
        public static Error DuplicateImage => Error.Conflict(
            code: "Image.DuplicateImage",
            description: "Image is already in use.");

        public static class FileExtension
        {
            public static Error InvalidFileExtension => Error.Validation(
                code: "Image.FileExtension.InvalidFileExtension",
                description: "Invalid file extension name.");
        }

        public static class ImageCategory
        {
            public static Error InvalidImageCategory => Error.Validation(
                code: "Image.ImageCategory.InvalidImageCategory",
                description: "Invalid image category name.");
        }
    }
}

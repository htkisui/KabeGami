using ErrorOr;

namespace KabeGami.Domain.Common.Errors;
public static partial class Errors
{
    public static class Image
    {
        public static class ImageCategory
        {
            public static Error InvalidImageCategory(string imageCategoryString) => Error.Validation(
                code: "Image.ImageCategory.InvalidImageCategory",
                description: $"{imageCategoryString} is not a valid category name.");
        }

        public static class ImageExtension
        {
            public static Error InvalidImageExtension(string imageExtensionString) => Error.Validation(
                code: "Image.ImageExtension.InvalidImageExtension",
                description: $"{imageExtensionString} is not a valid extension name.");
        }

    }
}

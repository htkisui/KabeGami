using ErrorOr;

namespace KabeGami.Domain.Common.Errors;
public static partial class Errors
{
    public static class Image
    {
        public static class ImageExtension
        {
            public static Error InvalidImageExtension(string imageExtensionName) => Error.Validation(
                code: "Image.ImageExtension.InvalidImageExtension",
                description: $"{imageExtensionName} is not a valid extension name.");
        }
    }
}

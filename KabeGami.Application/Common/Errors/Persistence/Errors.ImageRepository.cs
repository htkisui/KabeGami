using ErrorOr;
using KabeGami.Domain.Images.ValueObjects;

namespace KabeGami.Application.Common.Errors.Persistence;
public static partial class Errors
{
    public static class ImageRepository
    {
        public static Error ImageNotFound(ImageId imageId) => Error.NotFound(
            code: "ImageRepository.ImageNotFound",
            description: $"Image {imageId.Value} is not found.");
    }
}

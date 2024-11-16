using ErrorOr;
using KabeGami.Domain.Galleries.ValueObjects;

namespace KabeGami.Application.Common.Errors.Persistence;
public static partial class Errors
{
    public static class GalleryRepository
    {
        public static Error GalleryNameNotAvailable(string name) => Error.Conflict(
            code: "GalleryRepository.GalleryNameNotAvailable",
            description: $"Gallery's name : {name} is already in use.");

        public static Error GalleryNotFound(GalleryId galleryId) => Error.NotFound(
            code: "GalleryRepository.GalleryNotFound",
            description: $"Gallery {galleryId.Value} is not found.");

        public static Error GalleryNotFound(string name) => Error.NotFound(
            code: "GalleryRepository.GalleryNotFound",
            description: $"Gallery {name} is not found.");
    }
}

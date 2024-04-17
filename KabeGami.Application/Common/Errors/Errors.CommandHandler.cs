using ErrorOr;

namespace KabeGami.Application.Common.Errors;
public static partial class Errors
{
    public static class ChangeWallpaperCommandHandler
    {
        public static Error DefaultGalleryNotFound => Error.NotFound(
            code: "ChangeWallpaperCommandHandler.DefaultGalleryNotFound",
            description: "Default gallery is not found.");
    }

    public static class DeleteImageCommandHandler
    {
        public static Error ImageIsAlreadyUsed(Guid imageGuid) => Error.Conflict(
            code: "DeleteImageCommandHandler.ImageIsAlreadyUsed",
            description: $"Image {imageGuid} is already used.");
    }

    public static class DeleteGalleryCommandHandler
    {
        public static Error GalleryIsAlreadyUsed(Guid galleryGuid) => Error.Conflict(
            code: "DeleteGalleryCommandHandler.GalleryIsAlreadyUsed",
            description: $"Image {galleryGuid} is already used.");
    }

    public static class DeleteSubGalleryCommandHandler
    {
        public static Error SubGalleryIsAlreadyUsed(Guid subGalleryGuid) => Error.Conflict(
            code: "DeleteSubGalleryCommandHandler.SubGalleryIsAlreadyUsed",
            description: $"Image {subGalleryGuid} is already used.");
    }

    public static class StartupWallpaperCommandHandler
    {
        public static Error DefaultGalleryNotFound => Error.NotFound(
            code: "StartupWallpaperCommandHandler.DefaultGalleryNotFound",
            description: "Default gallery is not found.");

        public static Error DefaultSubGalleryNotFound => Error.NotFound(
            code: "StartupWallpaperCommandHandler.DefaultSubGalleryNotFound",
            description: "Default sub gallery is not found.");
    }
}

using ErrorOr;
using KabeGami.Domain.Galleries.ValueObjects;

namespace KabeGami.Domain.Common.Errors;
public static partial class Errors
{
    public static class Gallery
    {
        public static Error SubGalleryNameDuplicated(string name) => Error.Conflict(
            code: "Gallery.SubGalleryNameDuplicated",
            description: $"SubGallery's name : {name} is already used in this gallery.");

        public static Error SubGalleryNotFound(SubGalleryId subGalleryId) => Error.NotFound(
            code: "Gallery.SubGalleryNotFound",
            description: $"SubGallery {subGalleryId.Value} is not found in this gallery.");

        public static Error SubGalleryShortcutKeyDuplicated(string shortcutKey) => Error.Conflict(
            code: "Gallery.SubGalleryShortcutKeyDuplicated",
            description: $"SubGallery's shortcut key {shortcutKey} is already used in this gallery.");

        public static class SubGallery
        {
            public static Error CronScheduleNotValid(string cronSchedule) => Error.Validation(
                code: "Gallery.SubGallery.CronScheduleNotValid",
                description: $"CronSchedule {cronSchedule} is not valid. (* * * * * ? *)");

            public static Error ShortcutKeyNotValid(string shortcutKey) => Error.Validation(
                code: "Gallery.SubGallery.ShortcutKeyNotValid",
                description: $"Shortcutkey {shortcutKey} is not valid. (Control+NumPadX)");
        }
    }
}

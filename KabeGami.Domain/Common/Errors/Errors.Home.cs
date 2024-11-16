using ErrorOr;
using KabeGami.Domain.Galleries.ValueObjects;
using KabeGami.Domain.Homes.ValueObjects;

namespace KabeGami.Domain.Common.Errors;
public static partial class Errors
{
    public static class Home
    {
        public static Error DefaultKabeCannotBeDeleted(KabeId kabeId) => Error.Forbidden(
            code: "Home.DefaultKabeCannotBeDeleted",
            description: $"Kabe {kabeId.Value} is already assigned to Default and cannot be deleted.");

        public static class Kabe
        {
            public static Error GalleryAlreadyAssigned(GalleryId galleryId) => Error.Conflict(
                code: "Home.Kabe.GalleryAlreadyAssigned",
                description: $"Gallery {galleryId.Value} is already assigned");
            public static Error KabeNameDuplicated(string name) => Error.Conflict(
                code: "Home.Kabe.KabeNameDuplicated",
                description: $"Kabe's name : {name} is already used.");

            public static Error KabeNotFound(KabeId kabeId) => Error.NotFound(
                code: "Home.Kabe.KabeNotFound",
                description: $"Kabe {kabeId.Value} is not found.");

            public static Error KabeCombinationDuplicated(string combination) => Error.Conflict(
                code: "Home.Kabe.KabeCombinationDuplicated",
                description: $"Kabe's combination : {combination} is already used.");

            public static Error KabeCombinationNotValid(string combination) => Error.Validation(
                code: "Home.Kabe.KabeCronScheduleNotValid",
                description: $"Kabe's Combination {combination} is not valid.");

            public static Error KabeCronScheduleNotValid(string cronSchedule) => Error.Validation(
                code: "Home.Kabe.KabeCronScheduleNotValid",
                description: $"Kabe's CronSchedule {cronSchedule} is not valid. (* * * * * ? *)");
        }
    }
}

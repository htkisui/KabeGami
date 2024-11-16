namespace KabeGami.Application.Wallpapers.Requests;
public sealed record ChangeWallpaperRequest(
    Guid GalleryGuid,
    string CronSchedule);

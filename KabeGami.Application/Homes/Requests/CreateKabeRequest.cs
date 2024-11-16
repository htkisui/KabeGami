namespace KabeGami.Application.Homes.Requests;
public sealed record CreateKabeRequest(
    string Name,
    string Combination,
    string CronSchedule,
    string GalleryName);

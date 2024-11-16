namespace KabeGami.Application.Homes.Results;
public sealed record KabeResult(
    Guid KabeGuid,
    string Name,
    string Combination,
    string CronSchedule,
    Guid GalleryGuid);

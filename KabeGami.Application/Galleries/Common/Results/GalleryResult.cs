using KabeGami.Application.Images.Common.Results;

namespace KabeGami.Application.Galleries.Common.Results;
public sealed record GalleryResult(
    Guid GalleryGuid,
    Guid SubGalleryGuid,
    string Name,
    List<SubGalleryResult> SubGalleriesResult);

public sealed record SubGalleryResult(
    Guid SubGalleryGuid,
    string Name,
    string Combination,
    string CronSchedule,
    List<ImageGuidResult> ImageIdsResult);

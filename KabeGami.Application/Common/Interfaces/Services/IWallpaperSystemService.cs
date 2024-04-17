using ErrorOr;
using KabeGami.Application.Images.Common.Results;
using MediatR;

namespace KabeGami.Application.Common.Interfaces.Services;
public interface IWallpaperSystemService
{
    Guid GalleryGuid { get; }
    Guid SubGalleryGuid { get; }
    List<Guid> ImageGuids { get; }
    Task<ErrorOr<Unit>> SetWallpaperSystemJob(List<ImageResult> imageResults, string cronSchedule, CancellationToken cancellationToken);
    Task SetWallpaperToDesktop();
}

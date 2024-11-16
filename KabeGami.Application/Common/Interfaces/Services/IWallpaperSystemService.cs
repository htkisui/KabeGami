using KabeGami.Application.Images.Results;

namespace KabeGami.Application.Common.Interfaces.Services;
public interface IWallpaperSystemService
{
    Task SetWallpaperSystemJobAsync(List<ImageResult> imageResults, string cronSchedule, CancellationToken cancellationToken);
    Task SetWallpaperToDesktop(CancellationToken cancellationToken);
}

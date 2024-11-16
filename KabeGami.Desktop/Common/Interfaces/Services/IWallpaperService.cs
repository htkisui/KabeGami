using KabeGami.Desktop.ViewModels.Common.Models;

namespace KabeGami.Desktop.Common.Interfaces.Services;

public interface IWallpaperService
{
    Task ChangeWallpaper(KabeDisplayModel kabe);
    Task SetNextWallpaper(KabeDisplayModel kabe);
    Task StartupWallpaperAsync();
}

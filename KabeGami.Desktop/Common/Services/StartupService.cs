using KabeGami.Desktop.Common.Interfaces.Services;
using KabeGami.Desktop.Common.Interfaces.Stores;

namespace KabeGami.Desktop.Common.Services;

public sealed class StartupService(
    IEventHandlerService eventHandlerService,
    IGalleryStore galleryStore,
    IImageStore imageStore,
    IHomeStore homeStore,
    IWallpaperService wallpaperService)
        : IStartupService
{
    private readonly IEventHandlerService _eventHandlerService = eventHandlerService;
    private readonly IGalleryStore _galleryStore = galleryStore;
    private readonly IHomeStore _homeStore = homeStore;
    private readonly IImageStore _imageStore = imageStore;
    private readonly IWallpaperService _wallpaperService = wallpaperService;

    public async Task StartupBackgroundAsync()
    {
        await _homeStore.InitializeAsync();
        await _wallpaperService.StartupWallpaperAsync();
        await _galleryStore.InitializeAsync();
        _eventHandlerService.Initialize();
    }

    public async Task StartupAsync()
    {
        await _imageStore.InitializeAsync();
    }
}

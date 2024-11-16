using KabeGami.Application.Wallpapers.Commands.ChangeWallpaper;
using KabeGami.Application.Wallpapers.Commands.SetNextWallpaper;
using KabeGami.Application.Wallpapers.Commands.StartupWallpaper;
using KabeGami.Desktop.Common.Events.Galleries;
using KabeGami.Desktop.Common.Events.Wallpapers;
using KabeGami.Desktop.Common.Interfaces.Services;
using KabeGami.Desktop.Common.Interfaces.Stores;
using KabeGami.Desktop.ViewModels.Common.Models;
using MediatR;

namespace KabeGami.Desktop.Common.Services;

public sealed class WallpaperService
    : IWallpaperService
{
    private readonly IErrorHandlingService _errorHandlingService;
    private readonly IEventAggregator _eventAggregator;
    private readonly IHomeStore _homeStore;
    private readonly ISender _sender;

    public WallpaperService(
        IErrorHandlingService errorHandlingService,
        IEventAggregator eventAggregator, 
        IHomeStore homeStore,
        ISender sender)
    {
        _errorHandlingService = errorHandlingService;
        _eventAggregator = eventAggregator;
        _homeStore = homeStore;
        _sender = sender;
        InitializeEvents();
    }

    private void InitializeEvents()
    {
        _eventAggregator.GetEvent<WallpaperChangeEvent>().Subscribe(OnWallpaperChanged);
        _eventAggregator.GetEvent<GalleryImagesUpdatedEvent>().Subscribe(OnGalleryImagesChanged);
    }

    private async void OnGalleryImagesChanged(GalleryDisplayModel gallery)
    {
        var kabe = _homeStore.Home.DefaultKabe;
        if (kabe != null && gallery.GalleryGuid == kabe.GalleryGuid)
        {
            await ChangeWallpaper(kabe);
        }
    }

    public async Task SetNextWallpaper(KabeDisplayModel kabe)
    {
        var command = new SetNextWallpaperCommand();
        var res = await _sender.Send(command);
        if (res.IsError)
        {
            _errorHandlingService.HandlerErrors(res.Errors);
            return;
        }
    }

    public async Task StartupWallpaperAsync()
    {
        var command = new StartupWallpaperCommand();
        var res = await _sender.Send(command);
        if (res.IsError)
        {
            _errorHandlingService.HandlerErrors(res.Errors);
            return;
        }
    }

    private async void OnWallpaperChanged(KabeDisplayModel kabe)
    {
        await ChangeWallpaper(kabe);
    }

    public async Task ChangeWallpaper(KabeDisplayModel kabe)
    {
        var command = new ChangeWallpaperCommand(new(kabe.GalleryGuid, kabe.CronSchedule));
        var res = await _sender.Send(command);
        if (res.IsError)
        {
            throw new Exception(res.FirstError.Description);
        }
    }
}

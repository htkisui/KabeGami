using KabeGami.Application.Galleries.Queries.GetGallery;
using KabeGami.Application.KabeGamiCores.Commands.SetWallpaper;
using KabeGami.Application.KabeGamiCores.Commands.SetWallpaperToDesktop;
using KabeGami.Application.KabeGamiCores.Commands.StartupWallpaper;
using KabeGami.Application.KabeGamiCores.Queries.GetKabeGamiCore;
using KabeGami.Desktop.ViewModels.Common.Primitives;
using MediatR;
using Microsoft.Extensions.Options;

namespace KabeGami.Desktop.ViewModels.KabeGamiCores;
internal sealed class KabeGamiCoreViewModel : ViewModelBase
{
    private readonly ISender _sender;
    private readonly string _nextWallpaperCombination = String.Empty;
    public Guid GalleryGuid { get; private set; } = Guid.Empty;
    public Guid SubGalleryGuid { get; private set; } = Guid.Empty;
    public Dictionary<string, Guid> SubGalleryCombinations { get; private set; } = [];


    public KabeGamiCoreViewModel(ISender sender, IOptions<KabeGamiGlobalEvents> kabeGamiGlobalEvents)
    {
        _sender = sender;
        _nextWallpaperCombination = kabeGamiGlobalEvents.Value.NextWallpaperCombination;
        InitializeKabeGamiCore();
    }

    private async void InitializeKabeGamiCore()
    {
        var query = new GetKabeGamiCoreQuery();
        var res = await _sender.Send(query);
        if (res.IsError)
        {
            Problem(res.Errors);
            return;
        }
        GalleryGuid = res.Value.GalleryGuidResult.GalleryGuid;

        await StartupWallpaper();
    }

    private async Task SetSubGalleryCombinations()
    {
        var query = new GetGalleryQuery(GalleryGuid);
        var res = await _sender.Send(query);
        if (res.IsError)
        {
            Problem(res.Errors);
            return;
        }

        SubGalleryCombinations.Clear();
        foreach (var subGallery in res.Value.SubGalleriesResult)
        {
            SubGalleryCombinations[subGallery.Combination] = subGallery.SubGalleryGuid;
        }
    }

    private async Task StartupWallpaper()
    {
        var command = new StartupWallpaperCommand();
        var res = await _sender.Send(command);
        if (res.IsError)
        {
            Problem(res.Errors);
            return;
        }
        await SetSubGalleryCombinations();
    }

    public async Task ChangeWallpaper(string combination)
    {
        if (combination == _nextWallpaperCombination)
        {
            var command = new SetWallpaperToDesktopCommand();
            var res = await _sender.Send(command);
            if (res.IsError)
            {
                Problem(res.Errors);
                return;
            }
        }
        else if (SubGalleryCombinations.TryGetValue(combination, out Guid subGalleryGuid)
            && subGalleryGuid != SubGalleryGuid)
        {
            var command = new ChangeWallpaperCommand(subGalleryGuid);
            var res = await _sender.Send(command);
            if (res.IsError)
            {
                Problem(res.Errors);
                return;
            }
            SubGalleryGuid = subGalleryGuid;
        }
    }
}

using Gma.System.MouseKeyHook;
using KabeGami.Desktop.Common.Events.Homes;
using KabeGami.Desktop.Common.Events.Wallpapers;
using KabeGami.Desktop.Common.Interfaces.Services;
using KabeGami.Desktop.Common.Interfaces.Stores;
using KabeGami.Desktop.ViewModels.Common.Models;

namespace KabeGami.Desktop.Common.Services;

public sealed class EventHandlerService
    : IEventHandlerService
{
    private readonly IEventAggregator _eventAggregator;
    private readonly IHomeStore _homeService;

    public EventHandlerService(IEventAggregator eventAggregator, IHomeStore homeService)
    {
        _eventAggregator = eventAggregator;
        _homeService = homeService;
        InitializeEvents();
    }

    private void InitializeEvents()
    {
        _eventAggregator.GetEvent<KabeCreatedEvent>().Subscribe(OnKabeChanged);
        _eventAggregator.GetEvent<KabeDeletedEvent>().Subscribe(OnKabeChanged);
    }

    public void ChangeWallpaper(KabeDisplayModel kabe)
    {
        _eventAggregator.GetEvent<WallpaperChangeEvent>().Publish(kabe);
    }

    public void Initialize()
    {
        OnKabeChanged(_homeService.Home.Kabes);
    }


    private void OnKabeChanged(IList<KabeDisplayModel> kabes)
    {
        Hook.GlobalEvents().Dispose();

        var assignment = new Dictionary<Combination, Action>();

        foreach (var kabe in kabes)
        {
            var combination = Combination.FromString(kabe.Combination);
            assignment[combination] = () => ChangeWallpaper(kabe);
        }

        Hook.GlobalEvents().OnCombination(assignment);
    }
}

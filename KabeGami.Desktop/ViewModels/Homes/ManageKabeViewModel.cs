using KabeGami.Desktop.Common.Events.Galleries;
using KabeGami.Desktop.Common.Events.Homes;
using KabeGami.Desktop.Common.Interfaces.Stores;
using KabeGami.Desktop.ViewModels.Common.Models;
using KabeGami.Desktop.ViewModels.Common.Primitives;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace KabeGami.Desktop.ViewModels.Homes;
internal sealed class ManageKabeViewModel
    : ViewModelBase
{
    private readonly IEventAggregator _eventAggregator;
    private readonly IGalleryStore _galleryStore;
    private readonly IHomeStore _homeStore;

    private Guid defaultKabeGuid = Guid.Empty;
    public Guid DefaultKabeGuid
    {
        get => defaultKabeGuid;
        set
        {
            defaultKabeGuid = value;
            OnPropertyChanged(nameof(DefaultKabeGuid));
        }
    }

    private ObservableCollection<KabeDisplayModel> kabes = [];
    public ObservableCollection<KabeDisplayModel> Kabes
    {
        get => kabes;
        set
        {
            kabes = value;
            OnPropertyChanged(nameof(Kabes));
        }
    }

    private Dictionary<Guid, string> galleryDictionary = [];
    public Dictionary<Guid, string> GalleryDictionary
    {
        get => galleryDictionary;
        set
        {
            galleryDictionary = value;
            OnPropertyChanged(nameof(GalleryDictionary));
        }
    }

    public ManageKabeViewModel(
        IEventAggregator eventAggregator,
        IGalleryStore galleryStore,
        IHomeStore homeStore)
    {
        _eventAggregator = eventAggregator;
        _galleryStore = galleryStore;
        _homeStore = homeStore;
        InitializeEvents();
        Initialize();
    }
    public void Initialize()
    {
        DefaultKabeGuid = (_homeStore.Home.DefaultKabe is null) ? Guid.Empty : _homeStore.Home.DefaultKabe.KabeGuid;
        Kabes = new(_homeStore.Home.Kabes);
        GalleryDictionary = new(_galleryStore.Galleries.ToDictionary(g => g.GalleryGuid, g => g.Name));
    }

    private void InitializeEvents()
    {
        _eventAggregator.GetEvent<GalleryCreatedEvent>().Subscribe(OnGalleryChanged);
        _eventAggregator.GetEvent<GalleryDeletedEvent>().Subscribe(OnGalleryChanged);

        _eventAggregator.GetEvent<KabeCreatedEvent>().Subscribe(OnKabeChanged);
        _eventAggregator.GetEvent<KabeDeletedEvent>().Subscribe(OnKabeChanged);

        _eventAggregator.GetEvent<DefaultKabeSetEvent>().Subscribe(OnDefaultKabeChanged);
    }

    private void OnDefaultKabeChanged(Guid defaultKabeGuid)
    {
        DefaultKabeGuid = defaultKabeGuid;
    }

    private void OnGalleryChanged(IList<GalleryDisplayModel> galleries)
    {
        GalleryDictionary = galleries.ToDictionary(g => g.GalleryGuid, g => g.Name);
    }

    private void OnKabeChanged(IList<KabeDisplayModel> kabes)
    {
        Kabes = new(kabes);
    }

    public ICommand SetDefaultKabeCommand => new RelayCommand(async execute =>
    {
        if (execute is Guid kabeGuid)
        {
            await _homeStore.SetDefaultKabeAsync(kabeGuid);
        }
    });
}

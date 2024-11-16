using KabeGami.Desktop.Common.Events.Galleries;
using KabeGami.Desktop.Common.Events.Homes;
using KabeGami.Desktop.Common.Interfaces.Stores;
using KabeGami.Desktop.ViewModels.Common.Models;
using KabeGami.Desktop.ViewModels.Common.Primitives;
using System.Collections.ObjectModel;
namespace KabeGami.Desktop.ViewModels.Galleries;

internal sealed class ManageGalleryViewModel
    : ViewModelBase
{
    private readonly IEventAggregator _eventAggregator;
    private readonly IGalleryStore _galleryStore;
    private readonly IHomeStore _homeStore;

    private ObservableCollection<GalleryDisplayModel> galleries = [];
    public ObservableCollection<GalleryDisplayModel> Galleries
    {
        get => galleries;
        set
        {
            galleries = value;
            OnPropertyChanged(nameof(Galleries));
        }
    }

    private ObservableCollection<Guid> galleryGuidsUsed = [];
    public ObservableCollection<Guid> GalleryGuidsUsed
    {
        get => galleryGuidsUsed;
        set
        {
            galleryGuidsUsed = value;
            OnPropertyChanged(nameof(GalleryGuidsUsed));
        }
    }

    public ManageGalleryViewModel(
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

    private void Initialize()
    {
        Galleries = new(_galleryStore.Galleries);
        GalleryGuidsUsed = new(Galleries.Select(g => g.GalleryGuid)
            .ToList()
            .Intersect(_homeStore.Home.Kabes.Select(k => k.GalleryGuid)
                                        .ToList()));
    }

    private void InitializeEvents()
    {
        _eventAggregator.GetEvent<GalleryCreatedEvent>().Subscribe(OnGalleryChanged);
        _eventAggregator.GetEvent<GalleryDeletedEvent>().Subscribe(OnGalleryChanged);

        _eventAggregator.GetEvent<KabeCreatedEvent>().Subscribe(OnGalleryUsedChanged);
        _eventAggregator.GetEvent<KabeDeletedEvent>().Subscribe(OnGalleryUsedChanged);
    }

    private void OnGalleryUsedChanged(IList<KabeDisplayModel> _)
    {
        GalleryGuidsUsed = new(Galleries.Select(g => g.GalleryGuid)
            .ToList()
            .Intersect(_homeStore.Home.Kabes.Select(k => k.GalleryGuid)
                                        .ToList()));
    }

    private void OnGalleryChanged(IList<GalleryDisplayModel> galleries)
    {
        Galleries = new(galleries);
    }
}

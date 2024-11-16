using KabeGami.Desktop.Common.Events.Galleries;
using KabeGami.Desktop.Common.Events.Homes;
using KabeGami.Desktop.Common.Interfaces.Stores;
using KabeGami.Desktop.ViewModels.Common.Models;
using KabeGami.Desktop.ViewModels.Common.Primitives;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace KabeGami.Desktop.ViewModels.Homes;
internal sealed class CreateKabeViewModel
    : ViewModelBase
{
    private readonly IEventAggregator _eventAggregator;
    private readonly IGalleryStore _galleryStore;
    private readonly IHomeStore _homeStore;


    private string combinationField = string.Empty;
    public string CombinationField
    {
        get => combinationField;
        set
        {
            combinationField = value;
            OnPropertyChanged(nameof(CombinationField));
        }
    }

    private string cronScheduleField = string.Empty;
    public string CronScheduleField
    {
        get => cronScheduleField;
        set
        {
            cronScheduleField = value;
            OnPropertyChanged(nameof(CronScheduleField));
        }
    }

    private ObservableCollection<string> galleryNames = [];
    public ObservableCollection<string> GalleryNames
    {
        get => galleryNames;
        set
        {
            galleryNames = value;
            OnPropertyChanged(nameof(GalleryNames));
        }
    }

    private string? galleryNameField = null;
    public string? GalleryNameField
    {
        get => galleryNameField;
        set
        {
            galleryNameField = value;
            OnPropertyChanged(nameof(GalleryNameField));
        }
    }

    private string nameField = string.Empty;
    public string NameField
    {
        get => nameField;
        set
        {
            nameField = value;
            OnPropertyChanged(nameof(NameField));
        }
    }

    public CreateKabeViewModel(
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

    private void InitializeEvents()
    {
        _eventAggregator.GetEvent<GalleryCreatedEvent>().Subscribe(OnGalleryChanged);
        _eventAggregator.GetEvent<GalleryDeletedEvent>().Subscribe(OnGalleryChanged);

        _eventAggregator.GetEvent<KabeCreatedEvent>().Subscribe(ClearFields);
    }


    private void Initialize()
    {
        GalleryNames = new(_galleryStore.Galleries.Select(g => g.Name));
    }

    public ICommand CreateKabeCommand => new RelayCommand(async CommandExecutedEventData =>
    {
        await _homeStore.CreateKabeAsync(
            nameField,
            combinationField,
            cronScheduleField,
            galleryNameField!);
    });

    private void ClearFields(IList<KabeDisplayModel> _)
    {
        NameField = string.Empty;
        CombinationField = string.Empty;
        CronScheduleField = string.Empty;
        GalleryNameField = null;
    }

    private void OnGalleryChanged(IList<GalleryDisplayModel> galleries)
    {
        GalleryNames = new(galleries.Select(g => g.Name).ToList());
    }

}

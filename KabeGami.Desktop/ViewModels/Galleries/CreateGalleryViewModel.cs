using KabeGami.Desktop.Common.Events.Galleries;
using KabeGami.Desktop.Common.Interfaces.Stores;
using KabeGami.Desktop.ViewModels.Common.Models;
using KabeGami.Desktop.ViewModels.Common.Primitives;
using System.Windows.Input;
namespace KabeGami.Desktop.ViewModels.Galleries;

internal sealed class CreateGalleryViewModel
    : ViewModelBase
{
    private readonly IEventAggregator _eventAggregator;
    private readonly IGalleryStore _galleryStore;

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

    public CreateGalleryViewModel(
        IEventAggregator eventAggregator,
        IGalleryStore galleryStore)
    {
        _eventAggregator = eventAggregator;
        _galleryStore = galleryStore;
        InitializeEvents();
    }

    private void InitializeEvents()
    {
        _eventAggregator.GetEvent<GalleryCreatedEvent>().Subscribe(ClearFields);
    }

    public ICommand CreateGalleryCommand => new RelayCommand(async CommandExecutedEventData =>
    {
        await _galleryStore.CreateGalleryAsync(NameField);
    });

    private void ClearFields(IList<GalleryDisplayModel> _)
    {
        NameField = string.Empty;
    }
}

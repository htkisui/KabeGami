using KabeGami.Desktop.Common.Events.Images;
using KabeGami.Desktop.Common.Interfaces.Stores;
using KabeGami.Desktop.ViewModels.Common.Models;
using KabeGami.Desktop.ViewModels.Common.Primitives;
using System.Collections.ObjectModel;
namespace KabeGami.Desktop.ViewModels.Images;

internal sealed class ImageViewerViewModel
    : ViewModelBase
{
    private readonly IEventAggregator _eventAggregator;

    private Guid imageViewerGuid = Guid.Empty;
    public Guid ImageViewerGuid 
    {
        get => imageViewerGuid;
        set
        {
            imageViewerGuid = value;
            OnPropertyChanged(nameof(ImageViewerGuid));
        }
    }

    private ObservableCollection<ImageDisplayModel> images = [];
    public ObservableCollection<ImageDisplayModel> Images
    {
        get => images;
        set
        {
            images = value;
            OnPropertyChanged(nameof(Images));
        }
    }

    private int currentValue = 0;
    public int CurrentValue
    {
        get => currentValue;
        set
        {
            currentValue = value;
            OnPropertyChanged(nameof(CurrentValue));
        }
    }

    private int maximum = 0;
    public int Maximum
    {
        get => maximum;
        set
        {
            maximum = value;
            OnPropertyChanged(nameof(Maximum));
        }
    }

    public ImageViewerViewModel(
        IEventAggregator eventAggregator)
    {
        _eventAggregator = eventAggregator;
        InitializeEvents();
    }

    private void InitializeEvents()
    {
        _eventAggregator.GetEvent<ImageViewerInitializedEvent>().Subscribe(OnImageViewerChanged);
        _eventAggregator.GetEvent<ImageViewerUpdatedEvent>().Subscribe(OnImageViewerChanged);
        _eventAggregator.GetEvent<ImageMovedEvent>().Subscribe(OnImageMoved);
    }

    private void OnImageMoved(ImageListViewEvent imageListViewEvent)
    {
        if (imageListViewEvent.ImageViewerGuid == ImageViewerGuid)
        {
            foreach (var image in imageListViewEvent.Images)
            {
                Images.Add(image);
            }
        }
    }

    private void OnImageViewerChanged(ImageViewerEvent imageViewerEvent)
    {
        if (imageViewerEvent.ImageViewerGuid == ImageViewerGuid)
        {
            Images = new(imageViewerEvent.Images);
            CurrentValue = imageViewerEvent.Value;
            Maximum = imageViewerEvent.Maximum;
        }
    }

    public void InitializeImageViewerGuid(Guid imageViewerGuid)
    {
        ImageViewerGuid = imageViewerGuid;
    }
}

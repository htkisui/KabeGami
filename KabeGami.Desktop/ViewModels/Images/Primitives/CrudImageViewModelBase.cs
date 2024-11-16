using KabeGami.Desktop.Common.Events.Images;
using KabeGami.Desktop.Common.Interfaces.Stores;
using KabeGami.Desktop.ViewModels.Common.Models;
using KabeGami.Desktop.ViewModels.Common.Primitives;

namespace KabeGami.Desktop.ViewModels.Images.Primitives;
internal abstract class CrudImageViewModelBase
    : ViewModelBase
{
    protected readonly IEventAggregator _eventAggregator;
    protected readonly IImageStore _imageStore;

    private Guid inputImageViewerGuid = Guid.NewGuid();
    public Guid InputImageViewerGuid
    {
        get => inputImageViewerGuid;
        set
        {
            inputImageViewerGuid = value;
            OnPropertyChanged(nameof(InputImageViewerGuid));
        }
    }

    private Guid outputImageViewerGuid = Guid.NewGuid();
    public Guid OutputImageViewerGuid
    {
        get => outputImageViewerGuid;
        set
        {
            outputImageViewerGuid = value;
            OnPropertyChanged(nameof(OutputImageViewerGuid));
        }
    }

    protected List<ImageDisplayModel> OutputImages { get; set; } = [];

    protected CrudImageViewModelBase(IEventAggregator eventAggregator, IImageStore imageStore)
    {
        _eventAggregator = eventAggregator;
        _imageStore = imageStore;
        InitializeEvents();
    }

    private void InitializeEvents()
    {
        _eventAggregator.GetEvent<ImageSentOnRightClickEvent>().Subscribe(OnRightClick);
    }

    private void OnRightClick(ImageListViewEvent imageListViewEvent)
    {
        if (imageListViewEvent.ImageViewerGuid == InputImageViewerGuid)
        {
            foreach (var image in imageListViewEvent.Images)
            {
                OutputImages.Add(image);
            }
            _eventAggregator.GetEvent<ImageMovedEvent>().Publish(new(OutputImageViewerGuid, imageListViewEvent.Images));
        }

        if (imageListViewEvent.ImageViewerGuid == OutputImageViewerGuid)
        {
            foreach (var image in imageListViewEvent.Images)
            {
                OutputImages.Remove(image);
            }
            _eventAggregator.GetEvent<ImageMovedEvent>().Publish(new(InputImageViewerGuid, imageListViewEvent.Images));
        }
    }
}

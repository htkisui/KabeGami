using KabeGami.Desktop.Common.Events.Images;
using KabeGami.Desktop.Common.Interfaces.Stores;
using KabeGami.Desktop.ViewModels.Common.Primitives;

namespace KabeGami.Desktop.ViewModels.Images;

internal sealed class ImageViewModel(
    IEventAggregator eventAggregator, 
    IImageStore imageService)
        : ViewModelBase
{
    private readonly IEventAggregator _eventAggregator = eventAggregator;
    private readonly IImageStore _imageStore = imageService;

    private Guid localImageViewerGuid = Guid.Empty;
    public Guid LocalImageViewerGuid
    {
        get => localImageViewerGuid;
        set
        {
            localImageViewerGuid = value;
            OnPropertyChanged(nameof(LocalImageViewerGuid));
        }
    }

    public void InitializeImageViewer()
    {
        LocalImageViewerGuid = _imageStore.ImageServiceGuid;
        _eventAggregator.GetEvent<ImageViewerInitializedEvent>().Publish(new(
            LocalImageViewerGuid,
            _imageStore.Images,
            _imageStore.Images.Count,
            _imageStore.Images.Count));
    }
}

using KabeGami.Desktop.Common.Events.Images;
using KabeGami.Desktop.Common.Interfaces.Stores;
using KabeGami.Desktop.ViewModels.Images.Primitives;

namespace KabeGami.Desktop.ViewModels.Images;
internal sealed class DeleteImageViewModel(
    IEventAggregator eventAggregator,
    IImageStore imageStore)
        : CrudImageViewModelBase(eventAggregator, imageStore)
{
    public void InitializeInputImageViewer()
    {
        _eventAggregator.GetEvent<ImageViewerInitializedEvent>().Publish(new(
            InputImageViewerGuid,
            _imageStore.Images,
            _imageStore.Images.Count,
            _imageStore.Images.Count));
    }

    public async Task DeleteImagesAsync()
    {
        await _imageStore.DeleteImagesAsync(OutputImages);
    }
}

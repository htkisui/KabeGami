using KabeGami.Desktop.Common.Errors.ViewModels;
using KabeGami.Desktop.Common.Events.Images;
using KabeGami.Desktop.Common.Interfaces.Stores;
using KabeGami.Desktop.ViewModels.Common.Models;
using KabeGami.Desktop.ViewModels.Images.Primitives;

namespace KabeGami.Desktop.ViewModels.Galleries;
internal sealed class UpdateGalleryImagesViewModel(
    IEventAggregator eventAggregator,
    IGalleryStore galleryStore,
    IImageStore imageStore)
        : CrudImageViewModelBase(eventAggregator, imageStore)
{
    private readonly IGalleryStore _galleryStore = galleryStore;

    public GalleryDisplayModel Gallery { get; private set; } = null!;

    public void InitializeImageViewers(Guid galleryGuid)
    {
        Gallery = _galleryStore.Galleries.SingleOrDefault(g => g.GalleryGuid == galleryGuid)!;
        if (Gallery == null)
        {
            _errorHandlingService.HandlerErrors([Errors.UpdateGalleryImagesViewModel.GalleryNotFound]);
            return;
        }

        var inputImages = _imageStore.Images.Where(i => !Gallery.ImageGuids.Contains(i.ImageGuid)).ToList();
        OutputImages = _imageStore.Images.Except(inputImages).ToList();

        _eventAggregator.GetEvent<ImageViewerInitializedEvent>().Publish(new(
            InputImageViewerGuid,
            inputImages,
            inputImages.Count,
            inputImages.Count));

        _eventAggregator.GetEvent<ImageViewerInitializedEvent>().Publish(new(
            OutputImageViewerGuid,
            OutputImages,
            OutputImages.Count,
            OutputImages.Count));
    }

    public async Task UpdateGalleryImagesAsync()
    {
        if (Gallery != null)
        {
            await _galleryStore.UpdateGalleryImagesAsync(Gallery.GalleryGuid, OutputImages);
        }
    }
}

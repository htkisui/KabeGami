using KabeGami.Desktop.Common.Interfaces.Stores;
using KabeGami.Desktop.ViewModels.Common.Primitives;

namespace KabeGami.Desktop.ViewModels.Galleries;

internal sealed class DeleteGalleryViewModel(
    IGalleryStore galleryStore)
        : ViewModelBase
{
    private readonly IGalleryStore _galleryStore = galleryStore;

    public async Task DeleteGallery(Guid galleryGuid)
    {
        await _galleryStore.DeleteGalleryAsync(galleryGuid);
    }
}

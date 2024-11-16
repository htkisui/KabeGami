using KabeGami.Desktop.ViewModels.Common.Models;

namespace KabeGami.Desktop.Common.Interfaces.Stores;

public interface IGalleryStore
{
    List<GalleryDisplayModel> Galleries { get; }
    Task CreateGalleryAsync(string name);
    Task DeleteGalleryAsync(Guid galleryGuid);
    Task InitializeAsync();
    Task UpdateGalleryImagesAsync(Guid galleryGuid, List<ImageDisplayModel> images);
}

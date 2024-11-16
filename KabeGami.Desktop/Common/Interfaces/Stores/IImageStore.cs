using KabeGami.Desktop.ViewModels.Common.Models;

namespace KabeGami.Desktop.Common.Interfaces.Stores;

public interface IImageStore
{
    List<ImageDisplayModel> Images { get; }
    Guid ImageServiceGuid { get; }
    Dictionary<Guid, CancellationTokenSource> ImageViewerDictionary { get; }

    Task CreateImagesAsync(List<ImageDisplayModel> images);
    Task DeleteImagesAsync(List<ImageDisplayModel> images);
    Task GenerateBitmapImagesAsync(List<ImageDisplayModel> images, Guid guid);
    Task<List<string>> GetImagePathsFromDirectoryPathAsync(string directoryPath);
    Task InitializeAsync();
}

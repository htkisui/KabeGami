using KabeGami.Desktop.Common.Interfaces.Stores;
using KabeGami.Desktop.ViewModels.Common.Models;
using KabeGami.Desktop.ViewModels.Images.Primitives;

namespace KabeGami.Desktop.ViewModels.Images;
internal sealed class CreateImageViewModel(
    IEventAggregator eventAggregator,
    IImageStore imageStore)
        : CrudImageViewModelBase(eventAggregator, imageStore)
{
    private string directoryPath = string.Empty;
    public string DirectoryPath
    {
        get => directoryPath;
        set
        {
            directoryPath = value;
            OnPropertyChanged(nameof(DirectoryPath));
        }
    }

    public async Task GetImagesPathFromDirectoryPathAsync(string directoryPath)
    {
        var imagesPath = await _imageStore.GetImagePathsFromDirectoryPathAsync(directoryPath);
        List<ImageDisplayModel> images = [];
        foreach (var imagePath in imagesPath)
        {
            images.Add(ImageDisplayModel.CreateTemp(imagePath));
        }
        await _imageStore.GenerateBitmapImagesAsync(images, InputImageViewerGuid);
    }

    public async Task CreateImagesAsync()
    {
        await _imageStore.CreateImagesAsync(OutputImages);
    }
}

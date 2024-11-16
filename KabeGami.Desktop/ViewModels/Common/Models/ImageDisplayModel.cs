using System.IO;
using System.Windows.Media.Imaging;

namespace KabeGami.Desktop.ViewModels.Common.Models;
public sealed class ImageDisplayModel(
    Guid imageGuid,
    string imagePath,
    BitmapImage bitmapImage)
{
    public Guid ImageGuid { get; set; } = imageGuid;
    public string ImagePath { get; set; } = imagePath;
    public BitmapImage BitmapImage { get; set; } = bitmapImage;

    public static ImageDisplayModel CreateTemp(string imagePath)
    {
        return new(
            Guid.Empty,
            imagePath,
            null!);
    }
}

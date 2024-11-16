using ErrorOr;

namespace KabeGami.Desktop.Common.Errors.ViewModels;
public static partial class Errors
{
    public static class UpdateGalleryImagesViewModel
    {
        public static Error GalleryNotFound => Error.NotFound(       
            code: "UpdateGalleryImagesViewModel.GalleryNotFound",
            description: "Gallery not found.");
    }
}

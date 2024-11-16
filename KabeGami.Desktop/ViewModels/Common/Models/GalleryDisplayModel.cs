namespace KabeGami.Desktop.ViewModels.Common.Models;
public sealed class GalleryDisplayModel(
    Guid galleryGuid, 
    string name, 
    List<Guid> imageGuids)
{
    public Guid GalleryGuid { get; set; } = galleryGuid;
    public string Name { get; set; } = name;
    public List<Guid> ImageGuids { get; set; } = imageGuids;
}

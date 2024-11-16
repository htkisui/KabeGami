namespace KabeGami.Desktop.ViewModels.Common.Models;
public sealed class KabeDisplayModel(
    Guid kabeGuid, 
    string name, 
    string combination, 
    string cronSchedule, 
    Guid galleryGuid)
{
    public Guid KabeGuid { get; set; } = kabeGuid;
    public string Name { get; set; } = name;
    public string Combination { get; set; } = combination;
    public string CronSchedule { get; set; } = cronSchedule;
    public Guid GalleryGuid { get; set; } = galleryGuid;
}

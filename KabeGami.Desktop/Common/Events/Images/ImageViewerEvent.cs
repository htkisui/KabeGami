using KabeGami.Desktop.ViewModels.Common.Models;

namespace KabeGami.Desktop.Common.Events.Images;
internal sealed record ImageViewerEvent(
    Guid ImageViewerGuid,
    List<ImageDisplayModel> Images,
    int Value,
    int Maximum);
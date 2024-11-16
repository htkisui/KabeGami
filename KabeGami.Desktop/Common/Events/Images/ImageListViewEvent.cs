using KabeGami.Desktop.ViewModels.Common.Models;

namespace KabeGami.Desktop.Common.Events.Images;
internal sealed record ImageListViewEvent(
    Guid ImageViewerGuid,
    List<ImageDisplayModel> Images);

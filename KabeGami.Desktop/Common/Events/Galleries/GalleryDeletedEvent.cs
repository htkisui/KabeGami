using KabeGami.Desktop.ViewModels.Common.Models;

namespace KabeGami.Desktop.Common.Events.Galleries;

internal sealed class GalleryDeletedEvent
    : PubSubEvent<IList<GalleryDisplayModel>>
{
}

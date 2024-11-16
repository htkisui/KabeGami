using KabeGami.Desktop.ViewModels.Common.Models;

namespace KabeGami.Desktop.Common.Events.Galleries;
internal sealed class GalleryCreatedEvent
    : PubSubEvent<IList<GalleryDisplayModel>>
{
}

using KabeGami.Desktop.Common.Events.Images;
using KabeGami.Desktop.ViewModels.Common.Models;
using KabeGami.Desktop.ViewModels.Common.Primitives;
using System.Windows.Media.Imaging;

namespace KabeGami.Desktop.ViewModels.Images;
internal sealed class ImageListViewViewModel(
    IEventAggregator eventAggregator)
        : ViewModelBase
{
    private readonly IEventAggregator _eventAggregator = eventAggregator;

    public void ImageSelectedOnLeftClick(Guid guid, ImageDisplayModel image)
    {
        _eventAggregator.GetEvent<ImageSentOnLeftClickEvent>().Publish(new(guid, [image]));
    }

    public void ImageSelectedOnRightClick(Guid guid, ImageDisplayModel image) 
    {
        _eventAggregator.GetEvent<ImageSentOnRightClickEvent>().Publish(new(guid, [image]));
    }
}

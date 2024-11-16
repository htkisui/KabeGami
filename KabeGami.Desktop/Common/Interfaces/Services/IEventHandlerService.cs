using KabeGami.Desktop.ViewModels.Common.Models;

namespace KabeGami.Desktop.Common.Interfaces.Services;

public interface IEventHandlerService
{
    void ChangeWallpaper(KabeDisplayModel kabe);
    void Initialize();
}

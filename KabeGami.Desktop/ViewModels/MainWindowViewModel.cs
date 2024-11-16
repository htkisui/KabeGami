using KabeGami.Desktop.Common.Interfaces.Services;
using KabeGami.Desktop.ViewModels.Common.Primitives;

namespace KabeGami.Desktop.ViewModels;
internal sealed class MainWindowViewModel(
    IStartupService startupService)
        : ViewModelBase
{
    private readonly IStartupService _startupService = startupService;

    public async void Startup()
    {
        await _startupService.StartupAsync();
    }
}

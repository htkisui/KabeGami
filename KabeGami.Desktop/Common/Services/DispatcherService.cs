using KabeGami.Desktop.Common.Interfaces.Services;

namespace KabeGami.Desktop.Common.Services;
internal class DispatcherService : IDispatcherService
{
    public void Invoke(Action action)
    {
        System.Windows.Application.Current.Dispatcher.Invoke(action);
    }
}

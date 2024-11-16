namespace KabeGami.Desktop.Common.Interfaces.Services;

public interface IDispatcherService
{
    void Invoke(Action action);
}

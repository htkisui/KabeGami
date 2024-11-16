namespace KabeGami.Desktop.Common.Interfaces.Services;

public interface IStartupService
{
    Task StartupBackgroundAsync();
    Task StartupAsync();
}

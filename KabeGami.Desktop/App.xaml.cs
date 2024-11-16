using KabeGami.Application;
using KabeGami.Desktop.Common.Interfaces.Services;
using KabeGami.Desktop.Views;
using KabeGami.Desktop.Views.Common.Widgets;
using KabeGami.Infrastucture;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.IO;
using System.Windows;

namespace KabeGami.Desktop;
/// <summary>
/// Interaction logic for App.xaml
/// </summary>
public partial class App : System.Windows.Application
{
    public static IServiceProvider Container { get; private set; } = null!;
    public static IConfiguration Configuration { get; private set; } = null!;
    private Mutex mutex = null!;

    private const string appSettings = "appsettings.json";
    private const string mutexName = "MutexName";
    private const string mutexErrorMessage = "KabeGami is already running.";
    private const string mutexErrorTitle = "Error";

    protected override async void OnStartup(StartupEventArgs e)
    {
        base.OnStartup(e);

        var configurationBuilder = new ConfigurationBuilder()
           .SetBasePath(Directory.GetCurrentDirectory())
           .AddJsonFile(appSettings, optional: false, reloadOnChange: true);
        Configuration = configurationBuilder.Build();

        mutex = new Mutex(true, Configuration[mutexName], out bool createdNew);
        if (createdNew is false)
        {
            MessageBox.Show(mutexErrorMessage, mutexErrorTitle, MessageBoxButton.OK, MessageBoxImage.Error);
            Shutdown();
            return;
        }

        var services = new ServiceCollection();
        {
            services
                .AddPresentation()
                .AddApplication()
                .AddInfrastructure(Configuration);
        }


        Container = services.BuildServiceProvider();
        {
            var startupService = Container.GetRequiredService<IStartupService>();
            await startupService.StartupBackgroundAsync();

            Container.GetRequiredService<KabeGamiSysTrayIconUserControl>();
        }
    }

    protected override void OnExit(ExitEventArgs e)
    {
        base.OnExit(e);
        mutex.Dispose();
    }
}


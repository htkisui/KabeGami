using KabeGami.Application;
using KabeGami.Desktop.Views.KabeGamiCores;
using KabeGami.Infrastructure;
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

    protected override void OnStartup(StartupEventArgs e)
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
                .AddPresentation(Configuration)
                .AddApplication()
                .AddInfrastructure(Configuration);
        }

        Container = services.BuildServiceProvider();
        {
            Container.GetRequiredService<KabeGamiCoreUserControl>();
        }
    }

    protected override void OnExit(ExitEventArgs e)
    {
        base.OnExit(e);
        var mainMenuUserControl = Container.GetRequiredService<MainMenuUserControl>();
        mainMenuUserControl.Dispose();
        mutex.Dispose();
    }
}


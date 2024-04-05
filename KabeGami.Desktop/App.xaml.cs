using KabeGami.Application;
using KabeGami.Desktop.Views.Common.MainWindow;
using KabeGami.Desktop.Views.MainMenu;
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

    protected override void OnStartup(StartupEventArgs e)
    {
        base.OnStartup(e);

        var configurationBuilder = new ConfigurationBuilder()
           .SetBasePath(Directory.GetCurrentDirectory())
           .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);
        Configuration = configurationBuilder.Build();

        var services = new ServiceCollection();
        {
            services
                .AddPresentation()
                .AddApplication()
                .AddInfrastructure(Configuration);
        }

        Container = services.BuildServiceProvider();
        {
            var mainWindow = Container.GetRequiredService<MainWindow>();
            mainWindow.Show();
            Container.GetRequiredService<MainMenuUserControl>();
        }
    }

    protected override void OnExit(ExitEventArgs e)
    {
        base.OnExit(e);
        var mainMenuUserControl = Container.GetRequiredService<MainMenuUserControl>();
        mainMenuUserControl.Dispose();
    }
}


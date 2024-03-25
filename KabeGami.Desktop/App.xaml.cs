using Autofac;
using Autofac.Extensions.DependencyInjection;
using KabeGami.Application;
using KabeGami.Desktop.Views.Common.MainWindow;
using KabeGami.Desktop.Views.MainMenu;
using KabeGami.Infrastructure;
using Microsoft.Extensions.DependencyInjection;
using System.Windows;

namespace KabeGami.Desktop;
/// <summary>
/// Interaction logic for App.xaml
/// </summary>
public partial class App : System.Windows.Application
{
    public static IContainer Container { get; private set; } = null!;

    protected override void OnStartup(StartupEventArgs e)
    {
        base.OnStartup(e);

        var services = new ServiceCollection();
        {
            services
                .AddPresentation()
                .AddInfrastructure()
                .AddApplication();
        }

        var builder = new ContainerBuilder();
        {
            builder.Populate(services);
        }

        Container = builder.Build();
        {
            Container.Resolve<MainWindow>();
            Container.Resolve<MainMenuUserControl>();
        }
    }

    protected override void OnExit(ExitEventArgs e)
    {
        base.OnExit(e);
        var mainMenuUserControl = Container.Resolve<MainMenuUserControl>();
        mainMenuUserControl.Dispose();
    }
}


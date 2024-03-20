using Autofac;
using Autofac.Extensions.DependencyInjection;
using KabeGami.Application;
using KabeGami.Infrastructure;
using Microsoft.Extensions.DependencyInjection;
using System.Windows;


namespace KabeGami.Desktop;

public partial class App : System.Windows.Application
{
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
            builder.RegisterType<MainWindow>().AsSelf();
        }

        var container = builder.Build();
        {
            var mainWindow = container.Resolve<MainWindow>();
            mainWindow.Show();
        }
    }
}


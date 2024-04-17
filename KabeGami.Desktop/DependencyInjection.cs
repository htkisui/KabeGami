using KabeGami.Desktop.ViewModels.Common.Primitives;
using KabeGami.Desktop.ViewModels.KabeGamiCores;
using KabeGami.Desktop.ViewModels.Test;
using KabeGami.Desktop.Views.Common.MainWindow;
using KabeGami.Desktop.Views.KabeGamiCores;
using KabeGami.Desktop.Views.Test;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Windows.Forms;

namespace KabeGami.Desktop;

public static class DependencyInjection
{
    public static IServiceCollection AddPresentation(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        services.AddSingleton<MainWindow>();
        services
            .AddUserControls()
            .AddViewModels()
            .AddCommons(configuration);
        return services;
    }

    public static IServiceCollection AddUserControls(this IServiceCollection services)
    {
        services.AddSingleton<KabeGamiCoreUserControl>();
        services.AddSingleton<MainMenuUserControl>();

        services.AddSingleton<TestUserControl>();

        return services;
    }

    public static IServiceCollection AddViewModels(this IServiceCollection services)
    {
        services.AddSingleton<KabeGamiCoreViewModel>();
        services.AddSingleton<TestViewModel>();
        return services;
    }

    public static IServiceCollection AddCommons(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        services.AddSingleton<NotifyIcon>();
        services.Configure<KabeGamiGlobalEvents>(configuration.GetSection("KabeGamiGlobalEvents"));
        return services;
    }
}

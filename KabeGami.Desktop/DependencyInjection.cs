using KabeGami.Desktop.ViewModels.MainMenu;
using KabeGami.Desktop.Views.Common.MainWindow;
using KabeGami.Desktop.Views.MainMenu;
using Microsoft.Extensions.DependencyInjection;
using System.Windows.Forms;

namespace KabeGami.Desktop; 

public static class DependencyInjection
{
    public static IServiceCollection AddPresentation(this IServiceCollection services)
    {
        services.AddSingleton<MainWindow>();
        services
            .AddUserControls()
            .AddViewModels()
            .AddCommons();
        return services;
    }

    public static IServiceCollection AddUserControls(this IServiceCollection services)
    {
        services.AddSingleton<MainMenuUserControl>();
        return services;
    }

    public static IServiceCollection AddViewModels(this IServiceCollection services)
    {
        services.AddSingleton<MainMenuViewModel>();
        return services;
    }

    public static IServiceCollection AddCommons(this IServiceCollection services)
    {
        services.AddSingleton<NotifyIcon>();
        return services;
    }
}

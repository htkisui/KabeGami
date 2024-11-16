using KabeGami.Desktop.Common.Interfaces.Services;
using KabeGami.Desktop.Common.Interfaces.Stores;
using KabeGami.Desktop.Common.Mappings;
using KabeGami.Desktop.Common.Services;
using KabeGami.Desktop.Common.Stores;
using KabeGami.Desktop.ViewModels;
using KabeGami.Desktop.ViewModels.Galleries;
using KabeGami.Desktop.ViewModels.Homes;
using KabeGami.Desktop.ViewModels.Images;
using KabeGami.Desktop.Views;
using KabeGami.Desktop.Views.Common.Widgets;
using KabeGami.Desktop.Views.Galleries;
using KabeGami.Desktop.Views.Galleries.Common;
using KabeGami.Desktop.Views.Homes;
using KabeGami.Desktop.Views.Images;
using KabeGami.Desktop.Views.Images.Common;
using Microsoft.Extensions.DependencyInjection;
using System.Windows.Forms;

namespace KabeGami.Desktop;
public static class DependencyInjection
{
    public static IServiceCollection AddPresentation(
        this IServiceCollection services)
    {
        services.AddSingleton<MainWindow>();
        services
            .AddCommon()
            .AddServices()
            .AddUserControls()
            .AddViewModels();

        return services;
    }

    public static IServiceCollection AddCommon(
        this IServiceCollection services)
    {
        services.AddMappings();
        services.AddSingleton<IEventAggregator, EventAggregator>();
        services.AddSingleton<NotifyIcon>();
        return services;
    }

    public static IServiceCollection AddUserControls(
        this IServiceCollection services)
    {
        services.AddSingleton<KabeGamiSysTrayIconUserControl>();

        services.AddSingleton<GalleryUserControl>();
        services.AddSingleton<HomeUserControl>();
        services.AddSingleton<ImageUserControl>();

        services.AddTransient<UpdateGalleryImagesUserControl>();

        services.AddTransient<CreateImageUserControl>();
        services.AddTransient<DeleteImageUserControl>();

        return services;
    }

    public static IServiceCollection AddServices(
        this IServiceCollection services)
    {
        services.AddTransient<IDispatcherService, DispatcherService>();

        services.AddSingleton<IErrorHandlingService, ErrorHandlingService>();
        services.AddSingleton<IEventHandlerService, EventHandlerService>();
        services.AddSingleton<IGalleryStore, GalleryStore>();
        services.AddSingleton<IHomeStore, HomeStore>();
        services.AddSingleton<IImageStore, ImageStore>();
        services.AddSingleton<IWallpaperService, WallpaperService>();

        services.AddSingleton<IStartupService, StartupService>();

        return services;
    }

    public static IServiceCollection AddViewModels(
        this IServiceCollection services)
    {
        services.AddSingleton<CreateGalleryViewModel>();
        services.AddSingleton<DeleteGalleryViewModel>();
        services.AddSingleton<ManageGalleryViewModel>();
        services.AddTransient<UpdateGalleryImagesViewModel>();

        services.AddSingleton<CreateImageViewModel>();
        services.AddSingleton<DeleteImageViewModel>();
        services.AddTransient<ImageListViewViewModel>();
        services.AddTransient<ImageViewModel>();
        services.AddTransient<ImageViewerViewModel>();

        services.AddSingleton<CreateKabeViewModel>();
        services.AddSingleton<DeleteKabeViewModel>();
        services.AddSingleton<ManageKabeViewModel>();

        services.AddSingleton<MainWindowViewModel>();

        return services;
    }
}

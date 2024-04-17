using KabeGami.Application.Common.Interfaces.Persistence;
using KabeGami.Application.Common.Interfaces.Services;
using KabeGami.Infrastructure.Persistence;
using KabeGami.Infrastructure.Persistence.Repositories;
using KabeGami.Infrastructure.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Quartz;
using Quartz.Impl;

namespace KabeGami.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        services
            .AddServices()
            .AddPersistence(configuration);

        return services;
    }

    public static IServiceCollection AddServices(this IServiceCollection services)
    {
        services.AddSingleton<IOperatingSystemService, OperatingSystemService>();

        services.AddSingleton<IWallpaperSystemService, WallpaperSystemService>();
        services.AddSingleton<IScheduler>(provider =>
        {
            // Créer une instance de SchedulerFactory
            var schedulerFactory = new StdSchedulerFactory();

            // Retourner une instance de Scheduler
            return schedulerFactory.GetScheduler().Result;
        });

        return services;
    }

    public static IServiceCollection AddPersistence(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        services.AddDbContext<ApplicationDbContext>(options =>
            options.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));

        services.AddScoped<IUnitOfWork, UnitOfWork>();

        services.AddScoped<IGalleryRepository, GalleryRepository>();
        services.AddScoped<IImageRepository, ImageRepository>();
        services.AddScoped<IKabeGamiCoreRepository, KabeGamiCoreRepository>();

        return services;
    }
}

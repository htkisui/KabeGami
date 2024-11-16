using KabeGami.Application.Common.Interfaces.Persistence;
using KabeGami.Application.Common.Interfaces.Services;
using KabeGami.Infrastucture.Persistence;
using KabeGami.Infrastucture.Persistence.Repositories;
using KabeGami.Infrastucture.Services;
using Microsoft.Extensions.DependencyInjection;
using Quartz.Impl;
using Quartz;
using Microsoft.Extensions.Configuration;
using Microsoft.EntityFrameworkCore;

namespace KabeGami.Infrastucture;
public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        services
            .AddPersistence(configuration)
            .AddServices();
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
        services.AddScoped<IHomeRepository, HomeRepository>();
        services.AddScoped<IImageRepository, ImageRepository>();

        return services;
    }

    public static IServiceCollection AddServices(this IServiceCollection services)
    {
        services.AddSingleton<IOperatingSystemService, OperatingSystemService>();

        services.AddSingleton<IWallpaperSystemService, WallpaperSystemService>();
        services.AddSingleton<IScheduler>(provider =>
        {
            var schedulerFactory = new StdSchedulerFactory();
            return schedulerFactory.GetScheduler().Result;
        });

        return services;
    }
}

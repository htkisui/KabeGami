using KabeGami.Application.Common.Interfaces.Persistence;
using KabeGami.Application.Common.Interfaces.Services;
using KabeGami.Infrastructure.Persistence;
using KabeGami.Infrastructure.Persistence.Interceptors;
using KabeGami.Infrastructure.Persistence.Repositories;
using KabeGami.Infrastructure.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

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
        return services;
    }

    public static IServiceCollection AddPersistence(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        services.AddScoped<PublishDomainEventsInterceptor>();

        services.AddDbContext<ApplicationDbContext>(options =>
            options.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));

        services.AddScoped<IUnitOfWork, UnitOfWork>();

        services.AddScoped<IImageRepository, ImageRepository>();
        return services;
    }
}

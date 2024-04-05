using KabeGami.Application.Common.Mappings;
using MediatR;
using Microsoft.Extensions.DependencyInjection;

namespace KabeGami.Application;

public static class DependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(DependencyInjection).Assembly));
        services.AddMappings();
        return services;
    }
}

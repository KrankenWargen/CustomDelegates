using Microsoft.Extensions.DependencyInjection;

namespace FGW.Infrastructure;

public static class ConfigureServices
{
    public static IServiceCollection AddInfrastructureServices(this IServiceCollection services)
    {
        services.AddHostedService<BindAndDispatch>();

        return services;
    }
}
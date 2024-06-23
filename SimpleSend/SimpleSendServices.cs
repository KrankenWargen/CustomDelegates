using Microsoft.Extensions.DependencyInjection;

namespace SimpleSend;

public static class SimpleSendServices
{
    public static IServiceCollection AddSimpleSend(this IServiceCollection services)
    {
        services.AddHostedService<SimpleRegistrationService>();
        services.AddSingleton<ISender, SimpleManager>();
        return services;
    }
}
using Microsoft.Extensions.DependencyInjection;

namespace SimpleSend.Extensions;

public static class ConfigureServicesExtensions
{
    public static IServiceCollection AddSubscribers<TImplementation>(
        this IServiceCollection services
    ) where TImplementation : class, ISubscribe =>
        services.AddScoped<ISubscribe, TImplementation>();

    internal static bool Register(this IEnumerable<ISubscribe> entities,
        Func<ISubscribe, bool> subscribeMethod) =>
        entities.Select(subscribeMethod).All(x => x);
}
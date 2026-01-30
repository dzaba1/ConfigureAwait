using Microsoft.Extensions.DependencyInjection;

namespace Dzaba.ConfigureAwait.Lib;

public static class Bootstrapper
{
    public static IServiceCollection RegisterConfigureAwaitLib(this IServiceCollection services)
    {
        ArgumentNullException.ThrowIfNull(services);

        services.AddTransient<IMyService, MyService>();

        return services;
    }
}
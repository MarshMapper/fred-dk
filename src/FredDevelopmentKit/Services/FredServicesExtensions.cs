using FredDevelopmentKit.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace FredDevelopmentKit.Services
{
    public static class FredServicesExtensions
    {
        public static void AddSingletonFredServices(this IHostBuilder builder)
        {
            builder.ConfigureServices((hostContext, services) =>
            {
                services.AddHttpClient<FredHttpClient>()
                    .ConfigurePrimaryHttpMessageHandler(() =>
                    {
                        // set up pooling to allow long-lived HttpClients while avoiding DNS caching issues
                        // follwing recommendations here: https://learn.microsoft.com/en-us/dotnet/fundamentals/networking/http/httpclient-guidelines#pooled-connections
                        return new SocketsHttpHandler()
                        {
                            PooledConnectionLifetime = TimeSpan.FromMinutes(2)
                        };
                    })
                    // disable recycling, handled by pooling configured above
                    .SetHandlerLifetime(Timeout.InfiniteTimeSpan);

                services.AddSingleton<IFredCategoryService, FredCategoryService>();
                services.AddSingleton<IFredReleaseService, FredReleaseService>();
                services.AddSingleton<IFredSeriesService, FredSeriesService>();
                services.AddSingleton<IFredSourceService, FredSourceService>();
                services.Configure<FredClientOptions>(
                    hostContext.Configuration.GetSection($"{FredClientOptions.FredClient}"));
            });
        }
    }
}

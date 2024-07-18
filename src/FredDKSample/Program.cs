using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Configuration;
using FredDevelopmentKit.Services;
using FredDevelopmentKit.Configuration;
using FredDKSample.Examples;

namespace FredDKSample
{
    internal class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = Host.CreateDefaultBuilder(args);

            builder.ConfigureServices(services =>
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
            });

            var services = builder.Build().Services;
            IConfiguration? configuration = services.GetService<IConfiguration>();
            FredClientOptions fredClientOptions = new FredClientOptions();
            if (configuration != null)
            {
                configuration.GetSection(FredClientOptions.FredClient).Bind(fredClientOptions);
            }

            var categoryService = services.GetService<IFredCategoryService>();
            var releaseService = services.GetService<IFredReleaseService>();
            var seriesService = services.GetService<IFredSeriesService>();

            if (categoryService != null && releaseService != null && seriesService != null)
            {
                categoryService.SetApiKey(fredClientOptions.ApiKey);
                releaseService.SetApiKey(fredClientOptions.ApiKey);
                seriesService.SetApiKey(fredClientOptions.ApiKey);
            }

            await GetSampleData(categoryService, releaseService, seriesService);
        }
        public static async Task GetSampleData(IFredCategoryService? categoryService,
            IFredReleaseService? releaseService,
            IFredSeriesService? seriesService)
        {
            await CategoryExample.GetSampleCategoryData(categoryService);
            await ReleasesExample.GetSampleReleasesData(releaseService);
            await SeriesExample.GetSampleSeriesData(seriesService);
        }
    }
}

using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using FredDevelopmentKit.Services;
using FredDKSample.Examples;

namespace FredDKSample
{
    internal class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = Host.CreateDefaultBuilder(args);

            // for this console example, we'll use singleton services
            builder.AddSingletonFredServices();

            var services = builder.Build().Services;

            var categoryService = services.GetService<IFredCategoryService>();
            var releaseService = services.GetService<IFredReleaseService>();
            var seriesService = services.GetService<IFredSeriesService>();

            if (categoryService != null && releaseService != null && seriesService != null)
            {
                await GetSampleData(categoryService, releaseService, seriesService);
            }
            else
            {
                Console.WriteLine("Failed to get services");
            }
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

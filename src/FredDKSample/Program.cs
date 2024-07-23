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

            // call extension method to add Fred services
            builder.AddFredServices();

            await GetSampleData(builder.Build().Services);
         }
        public static async Task GetSampleData(IServiceProvider services)
        {
            var categoryService = services.GetService<IFredCategoryService>();
            var releaseService = services.GetService<IFredReleaseService>();
            var seriesService = services.GetService<IFredSeriesService>();
            var sourceService = services.GetService<IFredSourceService>();
            var tagsService = services.GetService<IFredTagsService>();
            var mapsService = services.GetService<IFredMapsService>();

            if (categoryService != null && releaseService != null && 
                seriesService != null && sourceService != null && tagsService != null &&
                mapsService != null)
            {
                await CategoryExample.GetSampleCategoryData(categoryService);
                await ReleasesExample.GetSampleReleasesData(releaseService);
                await SeriesExample.GetSampleSeriesData(seriesService);
                await SourceExample.GetSampleSourceData(sourceService);
                await TagsExample.GetSampleTagsData(tagsService);
                await MapsExample.GetSampleMapsData(mapsService);
            }
            else
            {
                Console.WriteLine("Failed to get services");
            }
        }
    }
}

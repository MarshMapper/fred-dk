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

            await GetSampleData(builder.Build().Services);
         }
        public static async Task GetSampleData(IServiceProvider services)
        {
            var categoryService = services.GetService<IFredCategoryService>();
            var releaseService = services.GetService<IFredReleaseService>();
            var seriesService = services.GetService<IFredSeriesService>();
            var sourceService = services.GetService<IFredSourceService>();
            var tagsService = services.GetService<IFredTagsService>();

            if (categoryService != null && releaseService != null && 
                seriesService != null && sourceService != null && tagsService != null)
            {
                await CategoryExample.GetSampleCategoryData(categoryService);
                await ReleasesExample.GetSampleReleasesData(releaseService);
                await SeriesExample.GetSampleSeriesData(seriesService);
                await SourceExample.GetSampleSourceData(sourceService);
                await TagsExample.GetSampleTagsData(tagsService);
            }
            else
            {
                Console.WriteLine("Failed to get services");
            }
        }
    }
}

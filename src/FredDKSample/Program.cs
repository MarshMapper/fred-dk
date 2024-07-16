using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Configuration;
using FredDevelopmentKit.Models;
using FredDevelopmentKit.Services;
using FredDevelopmentKit.Configuration;

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
            });

            var services = builder.Build().Services;
            IConfiguration? configuration = services.GetService<IConfiguration>();

            var categoryService = services.GetService<IFredCategoryService>();
            if (categoryService != null && configuration != null)
            {
                FredClientOptions fredClientOptions = new FredClientOptions();
                configuration.GetSection(FredClientOptions.FredClient).Bind(fredClientOptions);
                categoryService.SetApiKey(fredClientOptions.ApiKey);
            }

            await GetSampleData(categoryService);
        }
        public static async Task GetSampleData(IFredCategoryService? categoryService)
        {
            Category? category = null;

            if (categoryService == null)
            {
                return;
            }
            category = await categoryService.GetCategory(18);

            if (category != null)
            {
                Console.WriteLine($"Category name is {category.Name} id is {category.Id}");
            }
            else
            {
                Console.WriteLine("Category not found");
            }

            List<Category>? categories = null;
            categories = await categoryService.GetChildCategories(32992);

            if (categories != null)
            {
                foreach (Category c in categories)
                {
                    Console.WriteLine($"Child category name is {c.Name} id is {c.Id}");
                }
            }
            else
            {
                Console.WriteLine("No child categories found");
            }
            categories = await categoryService.GetRelatedCategories(32073);
            
            if (categories != null)
            {
                foreach (Category c in categories)
                {
                    Console.WriteLine($"Related category name is {c.Name} id is {c.Id}");
                }
            }
            else
            {
                Console.WriteLine("No related categories found");
            }

            CategorySeriesResponseDto? series = await categoryService.GetSeries(32992);
            if (series != null)
            {
                foreach (CategorySeriesDto s in series.Series)
                {
                    Console.WriteLine($"Series name is {s.Title} id is {s.Id}");
                }
            }
            else
            {
                Console.WriteLine("No series found");
            }
            CategoryTagsResponseDto? tags = await categoryService.GetTags(32992);
            if (tags != null)
            {
                foreach (CategoryTagDto t in tags.Tags)
                {
                    Console.WriteLine($"Tag name is {t.Name} Group id is {t.GroupId}");
                }
            }
            else
            {
                Console.WriteLine("No tags found");
            }
            tags = await categoryService.GetRelatedTags(125, new List<string> { "services", "quarterly" });
            if (tags != null)
            {
                foreach (CategoryTagDto t in tags.Tags)
                {
                    Console.WriteLine($"Tag name is {t.Name} Group id is {t.GroupId}");
                }
            }
            else
            {
                Console.WriteLine("No tags found");
            }
        }
    }
}

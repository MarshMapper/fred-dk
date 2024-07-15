using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using FredDeveloperKit.Models;
using FredDeveloperKit.Services;
using Microsoft.Extensions.Configuration;
using FredDeveloperKit.Configuration;

namespace FredDKSample
{
    internal class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = Host.CreateDefaultBuilder(args);

            builder.ConfigureServices(services =>
            {
                services.AddSingleton<IFredCategoryService, FredCategoryService>();

                services.AddHttpClient<FredCategoryService>()
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
            });

            var services = builder.Build().Services;
            IConfiguration? configuration = services.GetService<IConfiguration>();

            var categoryService = services.GetService<FredCategoryService>();
            if (categoryService != null && configuration != null)
            {
                FredClientOptions fredClientOptions = new FredClientOptions();
                configuration.GetSection(FredClientOptions.FredClient).Bind(fredClientOptions);
                categoryService.SetApiKey(fredClientOptions.ApiKey);
            }

            Category? category = null;
            if (categoryService != null)
            {
                category = await categoryService.GetCategory(18);
            }
            if (category != null)
            {
                Console.WriteLine($"Category name is {category.Name} id is {category.Id}");
            }
            else
            {
                Console.WriteLine("Category not found");
            }

            List<Category>? categories = null;
            if (categoryService != null)
            {
                categories = await categoryService.GetChildCategories(32992);
            }
            if (categories != null)
            {
                foreach (Category c in categories)
                {
                    Console.WriteLine($"Category name is {c.Name} id is {c.Id}");
                }
            }
            else
            {
                Console.WriteLine("No child categories found");
            }
        }
    }
}

using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Configuration;
using Ardalis.Result;
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
            await GetSampleCategoryData(categoryService);
            await GetSampleReleasesData(releaseService);
            await GetSampleSeriesData(seriesService);
        }
        public static async Task GetSampleCategoryData(IFredCategoryService? categoryService)
        {
            Result<Category> categoryResult;

            if (categoryService == null)
            {
                return;
            }
            categoryResult = await categoryService.GetCategory(18);

            if (categoryResult.IsSuccess)
            {
                Console.WriteLine($"Category name is {categoryResult.Value.Name} id is {categoryResult.Value.Id}");
            }
            else
            {
                Console.WriteLine("Category not found");
            }

            Result<List<Category>> categoriesResult;
            categoriesResult = await categoryService.GetChildCategories(32992);

            if (categoriesResult.IsSuccess)
            {
                foreach (Category c in categoriesResult.Value)
                {
                    Console.WriteLine($"Child category name is {c.Name} id is {c.Id}");
                }
            }
            else
            {
                Console.WriteLine("No child categories found");
            }
            categoriesResult = await categoryService.GetRelatedCategories(32073);

            if (categoriesResult.IsSuccess)
            {
                foreach (Category c in categoriesResult.Value)
                {
                    Console.WriteLine($"Related category name is {c.Name} id is {c.Id}");
                }
            }
            else
            {
                Console.WriteLine("No related categories found");
            }

            Result<RelatedSeriesResponseDto> seriesResult = await categoryService.GetSeries(32992);
            if (seriesResult.IsSuccess)
            {
                foreach (SeriesDto s in seriesResult.Value.Series)
                {
                    Console.WriteLine($"Series name is {s.Title} id is {s.Id}");
                }
            }
            else
            {
                Console.WriteLine("No series found");
            }
            Result<TagsResponseDto> tagsResult = await categoryService.GetTags(32992);
            if (tagsResult.IsSuccess)
            {
                foreach (TagDto t in tagsResult.Value.Tags)
                {
                    Console.WriteLine($"Tag name is {t.Name} Group id is {t.GroupId}");
                }
            }
            else
            {
                Console.WriteLine("No tags found");
            }
            tagsResult = await categoryService.GetRelatedTags(125, new List<string> { "services", "quarterly" });
            if (tagsResult.IsSuccess)
            {
                foreach (TagDto t in tagsResult.Value.Tags)
                {
                    Console.WriteLine($"Tag name is {t.Name} Group id is {t.GroupId}");
                }
            }
            else
            {
                Console.WriteLine("No tags found");
            }
        }
        public static async Task GetSampleReleasesData(IFredReleaseService? releaseService)
        {
            if (releaseService == null)
            {
                return;
            }
            Result<ReleaseResponseDto> releasesResult = await releaseService.GetReleases();
            if (releasesResult.IsSuccess)
            {
                foreach (Release release in releasesResult.Value.Releases)
                {
                    Console.WriteLine($"Release name is {release.Name} id is {release.Id}");
                }
            }
            else
            {
                Console.WriteLine("Releases not found");
            }
            Result<ReleasesDatesResponseDto> releasesDatesResult = await releaseService.GetReleasesDates();
            if (releasesDatesResult.IsSuccess)
            {
                foreach (ReleasesDate releaseDate in releasesDatesResult.Value.ReleaseDates)
                {
                    Console.WriteLine($"Release date is {releaseDate.Date} id is {releaseDate.ReleaseId}");
                }
            }
            else
            {
                Console.WriteLine("Releases dates not found");
            }
            Result<Release> releaseResult = await releaseService.GetRelease(19);
            if (releaseResult.IsSuccess)
            {
                Console.WriteLine($"Release name is {releaseResult.Value.Name} id is {releaseResult.Value.Id}");
            }
            else
            {
                Console.WriteLine("Release not found");
            }
            Result<ReleaseDateResponseDto> releaseDatesResult = await releaseService.GetReleaseDates(19);
            if (releaseDatesResult.IsSuccess)
            {
                foreach (ReleaseDate releaseDate in releaseDatesResult.Value.ReleaseDates)
                {
                    Console.WriteLine($"Release date is {releaseDate.Date} id is {releaseDate.ReleaseId}");
                }
            }
            else
            {
                Console.WriteLine("Release dates not found");
            }
            Result<RelatedSeriesResponseDto> seriesResult = await releaseService.GetSeries(19);
            if (seriesResult.IsSuccess)
            {
                foreach (SeriesDto series in seriesResult.Value.Series)
                {
                    Console.WriteLine($"Series name is {series.Title} id is {series.Id}");
                }
            }
            else
            {
                Console.WriteLine("No series found");
            }
            Result<SourcesResponseDto> sourcesResult = await releaseService.GetSources(19);
            if (sourcesResult.IsSuccess)
            {
                foreach (SourceDto source in sourcesResult.Value.Sources)
                {
                    Console.WriteLine($"Source name is {source.Name} id is {source.Id}");
                }
            }
            else
            {
                Console.WriteLine("No sources found");
            }
            Result<TagsResponseDto> tagsResult = await releaseService.GetTags(86);
            if (tagsResult.IsSuccess)
            {
                foreach (TagDto tag in tagsResult.Value.Tags)
                {
                    Console.WriteLine($"Tag name is {tag.Name} Group id is {tag.GroupId}");
                }
            }
            else
            {
                Console.WriteLine("No tags found");
            }
            tagsResult = await releaseService.GetRelatedTags(125, new List<string> { "services", "quarterly" });
            if (tagsResult.IsSuccess)
            {
                foreach (TagDto tag in tagsResult.Value.Tags)
                {
                    Console.WriteLine($"Tag name is {tag.Name} Group id is {tag.GroupId}");
                }
            }
            else
            {
                Console.WriteLine("No tags found");
            }
        }
        public static async Task GetSampleSeriesData(IFredSeriesService? seriesService)
        {
            if (seriesService == null)
            {
                return;
            }
            Result<SeriesDto> seriesResult = await seriesService.GetSeries("GNPCA");
            if (seriesResult.IsSuccess)
            {
                Console.WriteLine($"Series name is {seriesResult.Value.Title} id is {seriesResult.Value.Id}");
            }
            else
            {
                Console.WriteLine("Series not found");
            }
            Result<CategoryResponseDto> categoriesResult = await seriesService.GetCategories("GNPCA");
            if (categoriesResult.IsSuccess)
            {
                foreach (Category category in categoriesResult.Value.Categories)
                {
                    Console.WriteLine($"Category name is {category.Name} id is {category.Id}");
                }
            }
            else
            {
                Console.WriteLine("No categories found");
            }
            Result<ObservationsResponseDto> observationsResult = await seriesService.GetObservations("GNPCA");
            if (observationsResult.IsSuccess)
            {
                foreach (ObservationDto observation in observationsResult.Value.Observations)
                {
                    Console.WriteLine($"Observation date is {observation.Date} value is {observation.Value}");
                }
            }
            else
            {
                Console.WriteLine("No observations found");
            }
            Result<Release> releaseResult = await seriesService.GetRelease("GNPCA");
            if (releaseResult.IsSuccess)
            {
                Console.WriteLine($"Release name is {releaseResult.Value.Name} id is {releaseResult.Value.Id}");
            }
            else
            {
                Console.WriteLine("Release not found");
            }
            Result<SeriesResponseDto> searchResult = await seriesService.SearchSeries("monetary+service+index");
            if (searchResult.IsSuccess)
            {
                foreach (SeriesDto series in searchResult.Value.Series)
                {
                    Console.WriteLine($"Series name is {series.Title} id is {series.Id}");
                }
            }
            else
            {
                Console.WriteLine("No series found");
            }
            Result<TagsResponseDto> searchTagsResult = await seriesService.SearchSeriesTags("monetary+service+index");
            if (searchTagsResult.IsSuccess)
            {
                foreach (TagDto tag in searchTagsResult.Value.Tags)
                {
                    Console.WriteLine($"Tag name is {tag.Name} Group id is {tag.GroupId}");
                }
            }
            else
            {
                Console.WriteLine("No tags found");
            }
            Result<TagsResponseDto> searchRelatedTagsResult = await seriesService.SearchSeriesRelatedTags("monetary+service+index");
            if (searchRelatedTagsResult.IsSuccess)
            {
                foreach (TagDto tag in searchRelatedTagsResult.Value.Tags)
                {
                    Console.WriteLine($"Tag name is {tag.Name} Group id is {tag.GroupId}");
                }
            }
            else
            {
                Console.WriteLine("No tags found");
            }
            Result<TagsResponseDto> tagsResult = await seriesService.GetTags("GNPCA");
            if (tagsResult.IsSuccess)
            {
                foreach (TagDto tag in tagsResult.Value.Tags)
                {
                    Console.WriteLine($"Tag name is {tag.Name} Group id is {tag.GroupId}");
                }
            }
            else
            {
                Console.WriteLine("No tags found");
            }
            Result<UpdateResponseDto> updatesResult = await seriesService.GetSeriesUpdates();
            if (updatesResult.IsSuccess)
            {
                foreach (SeriesDto series in updatesResult.Value.Series)
                {
                    Console.WriteLine($"Update name is {series.Title} id is {series.Id}");
                }
            }
            else
            {
                Console.WriteLine("No updates found");
            }
            Result<VintageDatesResponseDto> vintageDatesResult = await seriesService.GetVintageDates("GNPCA");
            if (vintageDatesResult.IsSuccess)
            {
                foreach (string vintageDate in vintageDatesResult.Value.VintageDates)
                {
                    Console.WriteLine($"Vintage date is {vintageDate}");
                }
            }
            else
            {
                Console.WriteLine("No vintage dates found");
            }
        }
    }
}

using Ardalis.Result;
using FredDevelopmentKit.Models;
using FredDevelopmentKit.Services;

namespace FredDKSample.Examples
{
    public static class SeriesExample
    {
        public static async Task GetSampleSeriesData(IFredSeriesService? seriesService)
        {
            if (seriesService == null)
            {
                return;
            }
            ShowSeriesResult(await seriesService.GetSeries("GNPCA"));
            ShowCategoriesResult(await seriesService.GetCategories("GNPCA"));
            ShowObservationsResult(await seriesService.GetObservations("GNPCA"));
            ReleasesExample.ShowReleaseResult(await seriesService.GetRelease("GNPCA"));
            ShowSeriesResult(await seriesService.SearchSeries("monetary+service+index"));
            ReleasesExample.ShowTagsResult(await seriesService.SearchSeriesTags("monetary+service+index"));
            ReleasesExample.ShowTagsResult(await seriesService.SearchSeriesRelatedTags("monetary+service+index"));
            ReleasesExample.ShowTagsResult(await seriesService.GetTags("GNPCA"));
            ShowUpdateResult(await seriesService.GetSeriesUpdates());
            ShowVintageDatesResult(await seriesService.GetVintageDates("GNPCA"));
        }
        public static void ShowSeriesResult(Result<SeriesDto> seriesResult)
        {
            if (seriesResult.IsSuccess)
            {
                Console.WriteLine($"Series name is {seriesResult.Value.Title} id is {seriesResult.Value.Id}");
            }
            else
            {
                Console.WriteLine("Series not found");
            }
        }
        public static void ShowCategoriesResult(Result<CategoryResponseDto> categoriesResult)
        {
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
        }
        public static void ShowObservationsResult(Result<ObservationsResponseDto> observationsResult)
        {
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
        }
        public static void ShowSeriesResult(Result<SeriesResponseDto> seriesResult)
        {
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
        }
        public static void ShowUpdateResult(Result<UpdateResponseDto> updatesResult)
        {
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
        }
        public static void ShowVintageDatesResult(Result<VintageDatesResponseDto> vintageDatesResult)
        {
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

using Ardalis.Result;
using FredDevelopmentKit.Models;
using FredDevelopmentKit.Services;

namespace FredDKSample.Examples
{
    public static class MapsExample
    {
        public static async Task GetSampleMapsData(IFredMapsService? mapsService)
        {
            if (mapsService == null)
            {
                return;
            }
            ShowSeriesGroupResult(await mapsService.GetSeriesGroup("SMU56000000500000001a"));
            ShowSeriesByRegionResult(await mapsService.GetSeriesByRegion("WIPCPI"));
        }
        public static void ShowSeriesGroupResult(Result<SeriesGroup> result)
        {
            if (result.IsSuccess)
            {
                var seriesGroup = result.Value;
                Console.WriteLine($"Title: {seriesGroup.Title}, Region Type: {seriesGroup.RegionType}, Series Group: {seriesGroup.SeriesGroupId}");
            }
            else
            {
                Console.WriteLine("Series Group not found");
            }
        }
        public static void ShowSeriesByRegionResult(Result<SeriesByRegion> result)
        {
            if (result.IsSuccess)
            {
                var seriesByRegion = result.Value;
                Console.WriteLine($"Title: {seriesByRegion.Title}, Region Type: {seriesByRegion.RegionType}, Frequency: {seriesByRegion.Frequency}");
                foreach (var item in seriesByRegion.SeriesRegionValues)
                {
                    Console.WriteLine($"Region: {item.Region}, Value: {item.Value}");
                }
            }
            else
            {
                Console.WriteLine("Series By Region not found");
            }
        }
    }
}

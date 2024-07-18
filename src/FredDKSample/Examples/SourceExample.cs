using Ardalis.Result;
using FredDevelopmentKit.Models;
using FredDevelopmentKit.Services;

namespace FredDKSample.Examples
{
    public class SourceExample
    {
        public static async Task GetSampleSourceData(IFredSourceService sourceService)
        {
            if (sourceService == null)
            {
                return;
            }
            ReleasesExample.ShowSourcesResult(await sourceService.GetSources());
            ShowSourceResult(await sourceService.GetSource(1));
            ShowReleasesResults(await sourceService.GetReleases(1));
        }
        public static void ShowSourceResult(Result<SourceDto> sourceResult)
        {
            if (sourceResult.IsSuccess)
            {
                Console.WriteLine($"Source name is {sourceResult.Value.Name} id is {sourceResult.Value.Id}");
            }
            else
            {
                Console.WriteLine("Source not found");
            }
        }
        public static void ShowReleasesResults(Result<ReleasesResponseDto> releasesResult)
        {
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
        }
    }
}

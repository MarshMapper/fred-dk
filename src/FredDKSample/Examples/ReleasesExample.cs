using Ardalis.Result;
using FredDevelopmentKit.Models;
using FredDevelopmentKit.Services;

namespace FredDKSample.Examples
{
    public static class ReleasesExample
    {
        public static async Task GetSampleReleasesData(IFredReleaseService? releaseService)
        {
            if (releaseService == null)
            {
                return;
            }
            ShowReleasesResults(await releaseService.GetReleases());
            ShowReleasesDatesResults(await releaseService.GetReleasesDates());
            ShowReleaseResult(await releaseService.GetRelease(19));
            ShowReleaseDateResult(await releaseService.GetReleaseDates(19));
            ShowRelatedSeriesResult(await releaseService.GetSeries(19));
            ShowReleasesDatesResults(await releaseService.GetReleasesDates());
            ShowSourcesResult(await releaseService.GetSources(19));
            ShowTagsResult(await releaseService.GetTags(19));
            ShowTagsResult(await releaseService.GetRelatedTags(19, new List<string> { "services", "quarterly" }));
        }
        public static void ShowReleasesResults(Result<ReleaseResponseDto> releasesResult)
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
        public static void ShowReleasesDatesResults(Result<ReleasesDatesResponseDto> releasesDatesResult)
        {
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
        }
        public static void ShowReleaseResult(Result<Release> releaseResult)
        {
            if (releaseResult.IsSuccess)
            {
                Console.WriteLine($"Release name is {releaseResult.Value.Name} id is {releaseResult.Value.Id}");
            }
            else
            {
                Console.WriteLine("Release not found");
            }
        }
        public static void ShowReleaseDateResult(Result<ReleaseDateResponseDto> releaseDatesResult)
        {
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
        }
        public static void ShowRelatedSeriesResult(Result<RelatedSeriesResponseDto> seriesResult)
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
        public static void ShowSourcesResult(Result<SourcesResponseDto> sourcesResult)
        {
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
        }
        public static void ShowTagsResult(Result<TagsResponseDto> tagsResult)
        {
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
    }
}

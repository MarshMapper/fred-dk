using FredDevelopmentKit.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FredDKSample.Examples
{
    public class TagsExample
    {
        public static async Task GetSampleTagsData(IFredTagsService? tagsService)
        {
            if (tagsService == null)
            {
                return;
            }
            ReleasesExample.ShowTagsResult(await tagsService.GetTags());
            ReleasesExample.ShowTagsResult(await tagsService.GetRelatedTags(new List<string> { "monetary+aggregates", "weekly" }));
            ReleasesExample.ShowRelatedSeriesResult(await tagsService.GetSeries(new List<string> { "slovenia", "food", "oecd" }));
        }
    }
}

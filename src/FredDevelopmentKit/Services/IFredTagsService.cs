using Ardalis.Result;
using FredDevelopmentKit.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FredDevelopmentKit.Services
{
    public interface IFredTagsService
    {
        Task<Result<TagsResponseDto>> GetTags();
        Task<Result<TagsResponseDto>> GetRelatedTags(List<string> tagNames);
        Task<Result<RelatedSeriesResponseDto>> GetSeries(List<string> tagNames);
    }
}

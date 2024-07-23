using Ardalis.Result;
using FredDevelopmentKit.Configuration;
using FredDevelopmentKit.Models;
using Microsoft.Extensions.Options;

namespace FredDevelopmentKit.Services
{
    public class FredCategoryService : FredService, IFredCategoryService
    {
        public FredCategoryService(IFredHttpClient fredClient, IOptions<FredClientOptions> options) : 
            base(fredClient, options)
        {
        }
        public async Task<Result<Category>> GetCategory(int categoryId)
        {
            string categoryUrl = $"category?category_id={categoryId}&{GetCommonUrlSegments()}";
            Result<CategoryResponseDto> result = await _fredClient.GetFromJsonAsync<CategoryResponseDto>(categoryUrl);
            if (result.IsSuccess)
            {
                CategoryResponseDto? categoryResponse = result.Value;
                if (categoryResponse == null || categoryResponse.Categories == null || categoryResponse.Categories.Count == 0)
                {
                    return Result.NotFound();
                }
                else
                {
                    return Result.Success<Category>(categoryResponse.Categories[0]);
                }
            }
            else
            {
                return Result.NotFound();
            }
        }
        public async Task<Result<List<Category>>> GetChildCategories(int categoryId)
        {
            string childCategoriesUrl = $"category/children?category_id={categoryId}&{GetCommonUrlSegments()}";
            Result<CategoryResponseDto> result = await _fredClient.GetFromJsonAsync<CategoryResponseDto>(childCategoriesUrl);
            if (result.IsSuccess)
            {
                CategoryResponseDto? categoryResponse = result.Value;
                if (categoryResponse == null || categoryResponse.Categories == null || categoryResponse.Categories.Count == 0)
                {
                    return Result.NotFound();
                }
                else
                {
                    return Result.Success<List<Category>>(categoryResponse.Categories);
                }
            }
            else
            {
                return Result.NotFound();
            }
        }
        public async Task<Result<List<Category>>> GetRelatedCategories(int categoryId)
        {
            string relatedCategoriesUrl = $"category/related?category_id={categoryId}&{GetCommonUrlSegments()}";
            Result<CategoryResponseDto> result = await _fredClient.GetFromJsonAsync<CategoryResponseDto>(relatedCategoriesUrl);
            if (result.IsSuccess)
            {
                CategoryResponseDto? categoryResponse = result.Value;
                if (categoryResponse == null || categoryResponse.Categories == null || categoryResponse.Categories.Count == 0)
                {
                    return Result.NotFound();
                }
                else
                {
                    return Result.Success<List<Category>>(categoryResponse.Categories);
                }
            }
            else
            {
                return Result.NotFound();
            }
        }
        public async Task<Result<RelatedSeriesResponseDto>> GetSeries(int categoryId)
        {
            string seriesUrl = $"category/series?category_id={categoryId}&{GetCommonUrlSegments()}";
            return await _fredClient.GetFromJsonAsync<RelatedSeriesResponseDto>(seriesUrl);
        }
        public async Task<Result<TagsResponseDto>> GetTags(int categoryId)
        {
            string tagsUrl = $"category/tags?category_id={categoryId}&{GetCommonUrlSegments()}";
            return await _fredClient.GetFromJsonAsync<TagsResponseDto>(tagsUrl);
        }
        public async Task<Result<TagsResponseDto>> GetRelatedTags(int categoryId, List<string> searchTagNames)
        {
            string tagsUrl = $"category/related_tags?category_id={categoryId}&{GetCommonUrlSegments()}";
            if (searchTagNames != null && searchTagNames.Count > 0)
            {
                tagsUrl += "&tag_names=" + String.Join(';', searchTagNames);
            }

            return await _fredClient.GetFromJsonAsync<TagsResponseDto>(tagsUrl);
        }
    }
}

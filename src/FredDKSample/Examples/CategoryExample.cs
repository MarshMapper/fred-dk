using Ardalis.Result;
using FredDevelopmentKit.Models;
using FredDevelopmentKit.Services;

namespace FredDKSample.Examples
{
    public static class CategoryExample
    {
        public static async Task GetSampleCategoryData(IFredCategoryService? categoryService)
        {
            if (categoryService == null)
            {
                return;
            }
            ShowCategoryResult(await categoryService.GetCategory(18));
            ShowCategoriesResults(await categoryService.GetChildCategories(32992));
            ShowCategoriesResults(await categoryService.GetRelatedCategories(32073));
            ReleasesExample.ShowRelatedSeriesResult(await categoryService.GetSeries(32992));
            ReleasesExample.ShowTagsResult(await categoryService.GetTags(32992));
            ReleasesExample.ShowTagsResult(await categoryService.GetRelatedTags(125, new List<string> { "services", "quarterly" }));
        }
        public static void ShowCategoryResult(Result<Category> categoryResult)
        {
            if (categoryResult.IsSuccess)
            {
                Console.WriteLine($"Category name is {categoryResult.Value.Name} id is {categoryResult.Value.Id}");
            }
            else
            {
                Console.WriteLine("Category not found");
            }
        }
        public static void ShowCategoriesResults(Result<List<Category>> categoriesResult)
        {
            if (categoriesResult.IsSuccess)
            {
                foreach (Category c in categoriesResult.Value)
                {
                    Console.WriteLine($"Category name is {c.Name} id is {c.Id}");
                }
            }
            else
            {
                Console.WriteLine("No categories found");
            }
        }
    }
}

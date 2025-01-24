using Blogs.Application.Models;
using Blogs.Contracts.Requests;
using Blogs.Contracts.Responses;
using System.Reflection.Metadata.Ecma335;
using System.Runtime.CompilerServices;

namespace Blogs.API.Mapping
{
    public static class CategoryContractMapping
    {
        public static Category MapToCategory(this CreateCategoryRequest request)
        {
            var category = new Category
            {
                Id = Guid.NewGuid(),
                Name = request.Name,
                Description = request.Description,
                ParentCategoryId = request.ParentCategoryId
            };

            return category;
        }

        public static Category MapToCategory(this UpdateCategoryRequest request,Guid id)
        {
            var category = new Category
            {
                Id = id,
                Name = request.Name,
                Description = request.Description,
                ParentCategoryId = request.ParentCategoryId
            };

            return category;
        }

        public static CategoryResponse MaptoCategoryResponse(this Category category)
        {
            return new CategoryResponse
            {
                Id = category.Id,
                Name = category.Name,
                Slug = category.Slug,
                Description = category.Description,
                ParentCategoryId = category.ParentCategoryId
            };
        }

        public static CategoriesResponse MaptoCategoriesResponse(this IEnumerable<Category> categories) 
        {
            return new CategoriesResponse
            {
                Categories = categories.Select(x => x.MaptoCategoryResponse())
            };
        
        }
    }
}

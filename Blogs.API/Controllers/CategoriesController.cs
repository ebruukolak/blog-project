using Blogs.API.Mapping;
using Blogs.Application.Repositories;
using Blogs.Application.Services;
using Blogs.Contracts.Requests;
using Microsoft.AspNetCore.Mvc;

namespace Blogs.API.Controllers
{
    [ApiController]
    public class CategoriesController : ControllerBase
    {
        private readonly ICategoryService _categoryService;
        public CategoriesController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        [HttpPost(ApiEndpoints.Category.Create)]
        public async Task<IActionResult> Create([FromBody] CreateCategoryRequest request)
        {
            var category = request.MapToCategory();
            await _categoryService.CreateAsync(category);
            return CreatedAtAction(nameof(Get), new { idOrSlug = category.Id }, category);
        }
        [HttpGet(ApiEndpoints.Category.Get)]
        public async Task<IActionResult> Get([FromRoute] string idOrSlug)
        {
             var category = Guid.TryParse(idOrSlug,out var id) 
                ? await _categoryService.GetByIdAsync(id)
                : await _categoryService.GetBySlugAsync(idOrSlug);

            if (category == null)
            {
                return NotFound();
            }

            var response = category.MaptoCategoryResponse();
            return Ok(response);
        }

        [HttpGet(ApiEndpoints.Category.GetAll)]
        public async Task<IActionResult> GetAll()
        {
            var categories = await _categoryService.GetAllAsync();

            var response = categories.MaptoCategoriesResponse();
            return Ok(response);
        }

        [HttpPut(ApiEndpoints.Category.Update)]
        public async Task<IActionResult> Update([FromRoute] Guid id, [FromBody] UpdateCategoryRequest request)
        {
            var category = request.MapToCategory(id);

            var updatedCategory = await _categoryService.UpdateAsync(category);
            if (updatedCategory is null)
            {
                return NotFound();
            }
            var response = category?.MaptoCategoryResponse();
            return Ok(response);
        }

        [HttpDelete(ApiEndpoints.Category.Delete)]
        public async Task<IActionResult> Delete([FromRoute] Guid id)
        {
            var deleted = await _categoryService.DeleteByIdAsync(id);
            if (!deleted)
            {
                return NotFound();
            }
            return Ok();
        }

    }
}

using Blogs.API.Mapping;
using Blogs.Application.Services.Categories;
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
        public async Task<IActionResult> Create([FromBody] CreateCategoryRequest request,CancellationToken token)
        {
            var category = request.MapToCategory();
            await _categoryService.CreateAsync(category, token);
            return CreatedAtAction(nameof(Get), new { idOrSlug = category.Id }, category);
        }
        [HttpGet(ApiEndpoints.Category.Get)]
        public async Task<IActionResult> Get([FromRoute] string idOrSlug, CancellationToken token)
        {
             var category = Guid.TryParse(idOrSlug,out var id) 
                ? await _categoryService.GetByIdAsync(id, token)
                : await _categoryService.GetBySlugAsync(idOrSlug, token);

            if (category == null)
            {
                return NotFound();
            }

            var response = category.MaptoCategoryResponse();
            return Ok(response);
        }

        [HttpGet(ApiEndpoints.Category.GetAll)]
        public async Task<IActionResult> GetAll( CancellationToken token)
        {
            var categories = await _categoryService.GetAllAsync(token);

            var response = categories.MaptoCategoriesResponse();
            return Ok(response);
        }

        [HttpPut(ApiEndpoints.Category.Update)]
        public async Task<IActionResult> Update([FromRoute] Guid id, [FromBody] UpdateCategoryRequest request, CancellationToken token)
        {
            var category = request.MapToCategory(id);

            var updatedCategory = await _categoryService.UpdateAsync(category, token);
            if (updatedCategory is null)
            {
                return NotFound();
            }
            var response = category?.MaptoCategoryResponse();
            return Ok(response);
        }

        [HttpDelete(ApiEndpoints.Category.Delete)]
        public async Task<IActionResult> Delete([FromRoute] Guid id, CancellationToken token)
        {
            var deleted = await _categoryService.DeleteByIdAsync(id, token);
            if (!deleted)
            {
                return NotFound();
            }
            return Ok();
        }

    }
}

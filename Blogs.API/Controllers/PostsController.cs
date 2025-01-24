using Blogs.API.Mapping;
using Blogs.Application.Repositories;
using Blogs.Application.Services;
using Blogs.Contracts.Requests;
using Microsoft.AspNetCore.Mvc;

namespace Blogs.API.Controllers
{
    [ApiController]
    public class PostsController:ControllerBase
    {
        private readonly IPostService _postService;
        public PostsController(IPostService postService)
        {
            _postService = postService;
        }

        [HttpPost(ApiEndpoints.Post.Create)]
        public async Task<IActionResult> Create([FromBody] CreatePostRequest request)
        {
            var post = request.MapToPost();
            await _postService.CreateAsync(post);
            return CreatedAtAction(nameof(Get), new { idOrSlug = post.Id }, post);
        }
        [HttpGet(ApiEndpoints.Post.Get)]
        public async Task<IActionResult> Get([FromRoute] string idOrSlug)
        {
            var post = Guid.TryParse(idOrSlug,out var id) 
                ? await _postService.GetByIdAsync(id)
                : await _postService.GetBySlugAsync(idOrSlug);

            if (post == null)
            {
                return NotFound();
            }

            var response = post.MaptoPostResponse();
            return Ok(response);
        }

        [HttpGet(ApiEndpoints.Post.GetAll)]
        public async Task<IActionResult> GetAll()
        {
            var posts = await _postService.GetAllAsync();

            var response = posts.MaptoPostsResponse();
            return Ok(response);
        }

        [HttpPut(ApiEndpoints.Post.Update)]
        public async Task<IActionResult> Update([FromRoute] Guid id, [FromBody] UpdatePostRequest request)
        {
            var post = request.MapToPost(id);

            var updatedPost = await _postService.UpdateAsync(post);
            if (updatedPost is null)
            {
                return NotFound();
            }
            var response = post.MaptoPostResponse();
            return Ok(response);
        }

        [HttpDelete(ApiEndpoints.Post.Delete)]
        public async Task<IActionResult> Delete([FromRoute] Guid id)
        {
            var deleted = await _postService.DeleteByIdAsync(id);
            if (!deleted)
            {
                return NotFound();
            }
            return Ok();
        }
    }
}

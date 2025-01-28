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
        public async Task<IActionResult> Create([FromBody] CreatePostRequest request, CancellationToken token)
        {
            var post = request.MapToPost();
            await _postService.CreateAsync(post, token);
            var response = post.MaptoPostResponse();
            return CreatedAtAction(nameof(Get), new { idOrSlug = post.Id }, response);
        }
        [HttpGet(ApiEndpoints.Post.Get)]
        public async Task<IActionResult> Get([FromRoute] string idOrSlug, CancellationToken token)
        {
            var post = Guid.TryParse(idOrSlug,out var id) 
                ? await _postService.GetByIdAsync(id, token)
                : await _postService.GetBySlugAsync(idOrSlug,token);

            if (post == null)
            {
                return NotFound();
            }

            var response = post.MaptoPostResponse();
            return Ok(response);
        }

        [HttpGet(ApiEndpoints.Post.GetAll)]
        public async Task<IActionResult> GetAll(CancellationToken token)
        {
            var posts = await _postService.GetAllAsync(token);

            var response = posts.MaptoPostsResponse();
            return Ok(response);
        }

        [HttpPut(ApiEndpoints.Post.Update)]
        public async Task<IActionResult> Update([FromRoute] Guid id, [FromBody] UpdatePostRequest request, CancellationToken token)
        {
            var post = request.MapToPost(id);

            var updatedPost = await _postService.UpdateAsync(post, token);
            if (updatedPost is null)
            {
                return NotFound();
            }
            var response = post.MaptoPostResponse();
            return Ok(response);
        }

        [HttpDelete(ApiEndpoints.Post.Delete)]
        public async Task<IActionResult> Delete([FromRoute] Guid id, CancellationToken token)
        {
            var deleted = await _postService.DeleteByIdAsync(id, token);
            if (!deleted)
            {
                return NotFound();
            }
            return Ok();
        }
    }
}

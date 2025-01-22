using Blogs.API.Mapping;
using Blogs.Application.Repositories;
using Blogs.Contracts.Requests;
using Microsoft.AspNetCore.Mvc;

namespace Blogs.API.Controllers
{
    [ApiController]
    public class PostsController:ControllerBase
    {
        private readonly IPostRepository _postRepository;
        public PostsController(IPostRepository postRepository)
        {
            _postRepository = postRepository;
        }

        [HttpPost(ApiEndpoints.Post.Create)]
        public async Task<IActionResult> Create([FromBody] CreatePostRequest request)
        {
            var post = request.MapToPost();
            await _postRepository.CreateAsync(post);
            return CreatedAtAction(nameof(Get), new { id = post.Id }, post);
        }
        [HttpGet(ApiEndpoints.Post.Get)]
        public async Task<IActionResult> Get([FromRoute] Guid id)
        {
            var post = await _postRepository.GetByIdAsync(id);
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
            var posts = await _postRepository.GetAllAsync();

            var response = posts.MaptoPostsResponse();
            return Ok(response);
        }

        [HttpPut(ApiEndpoints.Post.Update)]
        public async Task<IActionResult> Update([FromRoute] Guid id, [FromBody] UpdatePostRequest request)
        {
            var post = request.MapToPost(id);

            var updated = await _postRepository.UpdateAsync(post);
            if (!updated)
            {
                return NotFound();
            }
            var response = post.MaptoPostResponse();
            return Ok(response);
        }

        [HttpDelete(ApiEndpoints.Post.Delete)]
        public async Task<IActionResult> Delete([FromRoute] Guid id)
        {
            var deleted = await _postRepository.DeleteByIdAsync(id);
            if (!deleted)
            {
                return NotFound();
            }
            return Ok();
        }
    }
}

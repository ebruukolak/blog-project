using Blogs.Application.Models;
using Blogs.Application.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blogs.Application.Services
{
    public class PostService : IPostService
    {
        private readonly IPostRepository _postRepository;

        public PostService(IPostRepository postRepository)
        {
            _postRepository = postRepository;
        }
        public Task<bool> CreateAsync(Post post)
        {
            return _postRepository.CreateAsync(post);
        }

        public Task<Post?> GetByIdAsync(Guid id)
        {
            return _postRepository.GetByIdAsync(id);
        }

        public Task<Post?> GetBySlugAsync(string slug)
        {
            return _postRepository.GetBySlugAsync(slug);
        }

        public Task<IEnumerable<Post>> GetAllAsync()
        {
            return _postRepository.GetAllAsync();
        }
        public async Task<Post?> UpdateAsync(Post post)
        {
            var postExist = await _postRepository.ExistByIdAsync(post.Id);
            if (!postExist)
            {
                return null;
            }
            await _postRepository.UpdateAsync(post);
            return post;
        }
        public Task<bool> DeleteByIdAsync(Guid id)
        {
            return _postRepository.DeleteByIdAsync(id);
        }
    }
}

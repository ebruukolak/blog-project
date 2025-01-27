using Blogs.Application.Models;
using Blogs.Application.Repositories;
using Blogs.Application.Validators;
using FluentValidation;
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
        private readonly PostValidator _postValidator;

        public PostService(IPostRepository postRepository, PostValidator postValidator)
        {
            _postRepository = postRepository;
            _postValidator = postValidator;
        }
        public async Task<bool> CreateAsync(Post post, CancellationToken token = default)
        {
            await _postValidator.ValidateAndThrowAsync(post, token);
            return await _postRepository.CreateAsync(post, token);
        }

        public Task<Post?> GetByIdAsync(Guid id, CancellationToken token = default)
        {
            return _postRepository.GetByIdAsync(id, token);
        }

        public Task<Post?> GetBySlugAsync(string slug, CancellationToken token = default)
        {
            return _postRepository.GetBySlugAsync(slug, token);
        }

        public Task<IEnumerable<Post>> GetAllAsync(CancellationToken token = default)
        {
            return _postRepository.GetAllAsync( token);
        }
        public async Task<Post?> UpdateAsync(Post post, CancellationToken token = default)
        {
            await _postValidator.ValidateAndThrowAsync(post, token);
            var postExist = await _postRepository.ExistByIdAsync(post.Id, token);
            if (!postExist)
            {
                return null;
            }
            await _postRepository.UpdateAsync(post, token);
            return post;
        }
        public Task<bool> DeleteByIdAsync(Guid id, CancellationToken token = default)
        {
            return _postRepository.DeleteByIdAsync(id, token);
        }
    }
}

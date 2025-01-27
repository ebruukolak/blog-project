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
        public async Task<bool> CreateAsync(Post post)
        {
            await _postValidator.ValidateAndThrowAsync(post);
            return await _postRepository.CreateAsync(post);
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
            await _postValidator.ValidateAndThrowAsync(post);
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

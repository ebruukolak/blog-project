using Blogs.Application.Models;
using Blogs.Application.Repositories;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blogs.Application.Validators
{
    public class PostValidator:AbstractValidator<Post>
    {
        private readonly IPostRepository _postRepository;
        public PostValidator(IPostRepository postRepository)
        {
            _postRepository = postRepository;
            RuleFor(x => x.Id)
            .NotEmpty();

            RuleFor(x=> x.Title)
                .NotEmpty();

            RuleFor(x=>x.Content)
                .NotEmpty();

            RuleFor(x=>x.Tags)
                .NotEmpty();

            RuleFor(x=>x.IsDraft)
                .NotNull();

            RuleFor(x => x)
                .MustAsync(ValidateSlug);
        }

        private async Task<bool> ValidateSlug(Post post, CancellationToken token)
        {
            var existingPost = await _postRepository.GetBySlugAsync(post.Slug);

            if(existingPost is not null)
            {
                return existingPost.Id == post.Id;
            }

            return existingPost is null;
        }
    }
}

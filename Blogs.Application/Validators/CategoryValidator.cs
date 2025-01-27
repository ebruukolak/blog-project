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
    public class CategoryValidator : AbstractValidator<Category>
    {
        private readonly ICategoryRepository _categoryRepository;
        public CategoryValidator(ICategoryRepository categoryRepository)
        {
            _categoryRepository = categoryRepository;
            RuleFor(x => x.Id)
                .NotEmpty();

            RuleFor(x => x.Name)
                .NotEmpty();

            RuleFor(x => x)
                .MustAsync(ValidateSlug)
                .WithMessage("This category already exist in the system");

        }
        private async Task<bool> ValidateSlug(Category category, CancellationToken cancellationToken)
        {
            var existingCategory = await _categoryRepository.GetBySlugAsync(category.Slug,cancellationToken);

            if (existingCategory is not null) 
            {
                return existingCategory.Id == category.Id;
            }

            return existingCategory is null;
        }
    }
}

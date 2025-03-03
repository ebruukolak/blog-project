using Blogs.Application.Models;
using Blogs.Application.Repositories;
using FluentValidation;

namespace Blogs.Application.Services.Categories
{
    public class CategoryService : ICategoryService
    {
        private readonly ICategoryRepository _categoryRepository;
        private readonly IValidator<Category> _validator;
        public CategoryService(ICategoryRepository categoryRepository, IValidator<Category> validator)
        {
            _categoryRepository = categoryRepository;
            _validator = validator;
        }
        public async Task<bool> CreateAsync(Category category, CancellationToken token = default)
        {
            await _validator.ValidateAndThrowAsync(category, token);
            return await _categoryRepository.CreateAsync(category, token);
        }
        public Task<Category?> GetByIdAsync(Guid id, CancellationToken token = default)
        {
            return _categoryRepository.GetByIdAsync(id, token);
        }

        public Task<Category?> GetBySlugAsync(string slug, CancellationToken token = default)
        {
            return _categoryRepository.GetBySlugAsync(slug, token);
        }


        public Task<IEnumerable<Category>> GetAllAsync(CancellationToken token = default)
        {
            return _categoryRepository.GetAllAsync(token);
        }


        public async Task<Category?> UpdateAsync(Category category, CancellationToken token = default)
        {
            await _validator.ValidateAndThrowAsync(category, token);
            var categoryExist = await _categoryRepository.ExistByIdAsync(category.Id, token);
            if (!categoryExist)
            {
                return null;
            }
            await _categoryRepository.UpdateAsync(category, token);
            return category;
        }

        public Task<bool> DeleteByIdAsync(Guid id, CancellationToken token = default)
        {
            return _categoryRepository.DeleteByIdAsync(id, token);
        }
    }
}

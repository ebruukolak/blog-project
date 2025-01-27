using Blogs.Application.Models;
using Blogs.Application.Repositories;
using FluentValidation;

namespace Blogs.Application.Services
{
    public class CategoryService : ICategoryService
    {
        private readonly ICategoryRepository _categoryRepository;
        private readonly IValidator<Category> _validator;
        public CategoryService(ICategoryRepository categoryRepository,IValidator<Category> validator)
        {
            _categoryRepository = categoryRepository;
            _validator = validator;
        }
        public async Task<bool> CreateAsync(Category category)
        {
            await _validator.ValidateAndThrowAsync(category);
            return await _categoryRepository.CreateAsync(category);
        }
        public Task<Category?> GetByIdAsync(Guid id)
        {
            return _categoryRepository.GetByIdAsync(id);
        }

        public Task<Category?> GetBySlugAsync(string slug)
        {
            return _categoryRepository.GetBySlugAsync(slug);
        }


        public Task<IEnumerable<Category>> GetAllAsync()
        {
            return _categoryRepository.GetAllAsync();
        }


        public async Task<Category?> UpdateAsync(Category category)
        {
            await _validator.ValidateAndThrowAsync(category);
            var categoryExist = await _categoryRepository.ExistByIdAsync(category.Id);
            if (!categoryExist)
            {
                return null;
            }
            await _categoryRepository.UpdateAsync(category);
            return category;
        }

        public Task<bool> DeleteByIdAsync(Guid id)
        {
            return _categoryRepository.DeleteByIdAsync(id);
        }
    }
}

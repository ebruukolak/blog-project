using Blogs.Application.Models;
using Blogs.Application.Repositories;

namespace Blogs.Application.Services
{
    public class CategoryService : ICategoryService
    {
        private readonly ICategoryRepository _categoryRepository;

        public CategoryService(ICategoryRepository categoryRepository)
        {
            _categoryRepository = categoryRepository;
        }
        public Task<bool> CreateAsync(Category category)
        {
            return _categoryRepository.CreateAsync(category);
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

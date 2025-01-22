using Blogs.Application.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blogs.Application.Repositories
{
    public class CategoryRepository : ICategoryRepository
    {
        private readonly List<Category> _categories = new();
        public Task<bool> CreateAsync(Category category)
        {
            _categories.Add(category);
            return Task.FromResult(true);
        }
        public Task<Category?> GetByIdAsync(Guid id)
        {
            var category = _categories.SingleOrDefault(x => x.Id == id);
            return Task.FromResult(category);
        }

        public Task<IEnumerable<Category>> GetAllAsync()
        {
            return Task.FromResult(_categories.AsEnumerable());
        }

        public Task<bool> UpdateAsync(Category category)
        {
            var categoryIndex = _categories.FindIndex(x=>x.Id == category.Id);
            if (categoryIndex == -1) 
            {
                return Task.FromResult(false);
            }
            _categories[categoryIndex] = category;
            return Task.FromResult(true);
        }
        public Task<bool> DeleteByIdAsync(Guid id)
        {
            var removeCount = _categories.RemoveAll(x=>x.Id == id);
            var categoryRemoved=removeCount > 0;
            return Task.FromResult(categoryRemoved);
        }

       
    }
}

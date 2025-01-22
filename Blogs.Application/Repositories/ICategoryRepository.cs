using Blogs.Application.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blogs.Application.Repositories
{
    public interface ICategoryRepository
    {
        Task<bool> CreateAsync(Category category);
        Task<Category?> GetByIdAsync(Guid id);
        Task<IEnumerable<Category>> GetAllAsync();
        Task<bool> UpdateAsync(Category category);
        Task<bool> DeleteByIdAsync(Guid id);
    }
}

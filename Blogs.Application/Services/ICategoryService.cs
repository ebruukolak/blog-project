using Blogs.Application.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blogs.Application.Services
{
    public interface ICategoryService
    {
        Task<bool> CreateAsync(Category category);
        Task<Category?> GetByIdAsync(Guid id);
        Task<Category?> GetBySlugAsync(string slug);
        Task<IEnumerable<Category>> GetAllAsync();
        Task<Category?> UpdateAsync(Category category);
        Task<bool> DeleteByIdAsync(Guid id);
    }
}

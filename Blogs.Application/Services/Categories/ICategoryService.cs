using Blogs.Application.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blogs.Application.Services.Categories
{
    public interface ICategoryService
    {
        Task<bool> CreateAsync(Category category, CancellationToken token);
        Task<Category?> GetByIdAsync(Guid id, CancellationToken token);
        Task<Category?> GetBySlugAsync(string slug, CancellationToken token);
        Task<IEnumerable<Category>> GetAllAsync(CancellationToken token);
        Task<Category?> UpdateAsync(Category category, CancellationToken token);
        Task<bool> DeleteByIdAsync(Guid id, CancellationToken token);
    }
}

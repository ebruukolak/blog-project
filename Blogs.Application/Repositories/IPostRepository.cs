using Blogs.Application.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blogs.Application.Repositories
{
    public interface IPostRepository
    {
        Task<bool> CreateAsync(Post post);
        Task<Post?> GetByIdAsync(Guid id);
        Task<IEnumerable<Post>> GetAllAsync();
        Task<bool> UpdateAsync(Post post);
        Task<bool> DeleteByIdAsync(Guid id);
    }
}

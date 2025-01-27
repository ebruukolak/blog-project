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
        Task<bool> CreateAsync(Post post,CancellationToken token);
        Task<Post?> GetByIdAsync(Guid id, CancellationToken token);
        Task<Post?> GetBySlugAsync(string slug, CancellationToken token);
        Task<IEnumerable<Post>> GetAllAsync(CancellationToken token);
        Task<bool> UpdateAsync(Post post, CancellationToken token);
        Task<bool> DeleteByIdAsync(Guid id, CancellationToken token);
        Task<bool> ExistByIdAsync(Guid id, CancellationToken token);
    }
}

using Blogs.Application.Models;

namespace Blogs.Application.Services
{
    public interface IPostService
    {
        Task<bool> CreateAsync(Post post, CancellationToken token);
        Task<Post?> GetByIdAsync(Guid id, CancellationToken tok);
        Task<Post?> GetBySlugAsync(string slug, CancellationToken token);
        Task<IEnumerable<Post>> GetAllAsync(CancellationToken tok);
        Task<Post?> UpdateAsync(Post post, CancellationToken tok);
        Task<bool> DeleteByIdAsync(Guid id, CancellationToken token);
    }
}

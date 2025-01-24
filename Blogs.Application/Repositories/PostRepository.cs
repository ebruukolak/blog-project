using Blogs.Application.Models;

namespace Blogs.Application.Repositories
{
    public class PostRepository : IPostRepository
    {
        private readonly List<Post> _posts = new();
        public Task<bool> CreateAsync(Post post)
        {
            _posts.Add(post);
            return Task.FromResult(true);
        }

        public Task<Post?> GetByIdAsync(Guid id)
        {
            var post = _posts.SingleOrDefault(x => x.Id == id);
            return Task.FromResult(post);
        }
        public Task<Post?> GetBySlugAsync(string slug)
        {
            var post = _posts.SingleOrDefault(x => x.Slug == slug);
            return Task.FromResult(post);
        }

        public Task<IEnumerable<Post>> GetAllAsync()
        {
            return Task.FromResult(_posts.AsEnumerable());
        }

        public Task<bool> UpdateAsync(Post post)
        {
            var postIndex = _posts.FindIndex(x => x.Id == post.Id);
            if (postIndex == -1)
            {
                return Task.FromResult(false);
            }
            _posts[postIndex]=post;
            return Task.FromResult(true);
        }

        public Task<bool> DeleteByIdAsync(Guid id)
        {
            var removedCount = _posts.RemoveAll(x => x.Id == id);
            var removedPost = removedCount > 0;
            return Task.FromResult(removedPost);
        }

      
    }
}

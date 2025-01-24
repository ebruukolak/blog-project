using Blogs.Application.Models;
using Blogs.Contracts.Requests;
using Blogs.Contracts.Responses;

namespace Blogs.API.Mapping
{
    public static class PostContractMapping
    {
        public static Post MapToPost(this CreatePostRequest request)
        {
            var post = new Post
            {
                Id = Guid.NewGuid(),
                 Title= request.Title,
                Content = request.Content,
                //AuthorId = request.AuthorId,
                CategoryId = request.CategoryId,
                Tags = request.Tags.ToList(),
                IsDraft = request.IsDraft,
                PublishedDate = request.PublishedDate
            };

            return post;
        }

        public static Post MapToPost(this UpdatePostRequest request, Guid id)
        {
            var post = new Post
            {
                Id = id,
                Title = request.Title,
                Content = request.Content,
                //AuthorId = request.AuthorId,
                CategoryId = request.CategoryId,
                Tags = request.Tags.ToList(),
                IsDraft = request.IsDraft,
                PublishedDate = request.PublishedDate
            };

            return post;
        }

        public static PostResponse MaptoPostResponse(this Post request)
        {
            return new PostResponse
            {
                Id = request.Id,
                Title = request.Title,
                Slug = request.Slug,
                Content = request.Content,
                //AuthorId = request.AuthorId,
                CategoryId = request.CategoryId,
                Tags = request.Tags,
                IsDraft = request.IsDraft,
                PublishedDate = request.PublishedDate
            };
        }

        public static PostsResponse MaptoPostsResponse(this IEnumerable<Post> posts)
        {
            return new PostsResponse
            {
                Posts = posts.Select(x => x.MaptoPostResponse())
            };

        }
    }
}

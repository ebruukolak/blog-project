using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blogs.Contracts.Responses
{
    public class PostsResponse
    {
        public required IEnumerable<PostResponse> Posts { get; init; } = Enumerable.Empty<PostResponse>();
    }
}

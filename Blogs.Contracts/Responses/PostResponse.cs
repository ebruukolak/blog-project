using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blogs.Contracts.Responses
{
    public class PostResponse
    {
        public required Guid Id { get; init; }
        public required string Title { get; set; }
        public required string Slug { get; set; }
        public required string Content { get; set; }
        //public required int AuthorId { get; set; }
        public required Guid CategoryId { get; set; }
        public IEnumerable<string> Tags { get; set; } = Enumerable.Empty<string>();
        public required bool IsDraft { get; set; }
        public DateTime PublishedDate { get; set; }
    }
}

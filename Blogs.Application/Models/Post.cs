using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blogs.Application.Models
{
    public class Post
    {
        public required Guid Id { get; init; }
        public required string Title { get; set; }
        public required string Content { get; set; }
        public required int AuthorId { get; set; }
        public required int CategoryId { get; set; }
        public required List<string> Tags { get; init; } = new();
        public required bool IsDraft { get; set; }
        public DateTime PublishedDate { get; set; }
    }
}

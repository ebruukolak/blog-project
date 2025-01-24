using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blogs.Contracts.Requests
{
    public class CreatePostRequest
    {
        public required string Title {  get; init; }
        public required string Content {  get; init; }
        //public required int AuthorId {  get; set; }
        public required Guid CategoryId { get; set; }
        public IEnumerable<string> Tags { get; set; }= Enumerable.Empty<string>();
        public required bool IsDraft {  get; set; }
        public DateTime PublishedDate { get; set; }

    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blogs.Contracts.Requests
{
    public class CreateCategoryRequest
    {
        public required string Name { get; init; }
        public string Description { get; set; }
        public int? ParentCategoryId { get; set; }
    }
}

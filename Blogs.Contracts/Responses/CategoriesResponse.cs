using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blogs.Contracts.Responses
{
    public class CategoriesResponse
    {
        public required IEnumerable<CategoryResponse> Categories { get; init; } = Enumerable.Empty<CategoryResponse>();
    }
}

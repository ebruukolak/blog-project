using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blogs.Contracts.Responses
{
    public class RoleResponses
    {
        public required IEnumerable<RoleResponse> Roles { get; init; } = Enumerable.Empty<RoleResponse>();

    }
}

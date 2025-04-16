using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blogs.Contracts.Responses
{
    public  class RoleResponse
    {
        public required Guid Id { get; init; }
        public required string Name { get; set; }
    }
}

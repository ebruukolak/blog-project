using Org.BouncyCastle.Bcpg.OpenPgp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blogs.Application.Models
{
    public class Role
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public DateTime CreatedAdt { get; set; }
    }
}

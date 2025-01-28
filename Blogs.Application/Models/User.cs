using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blogs.Application.Models
{
    public class User
    {
        public required Guid Id { get; init; }
        public required string Email { get; set; }
        public required Guid RoleId { get; set; }
        public required string PasswordHash { get; init; }
        public required bool IsDeleted { get; set; }
        public required DateTime CreatedAt { get; init; }
        public DateTime Updatedat { get; set; }
    }
}

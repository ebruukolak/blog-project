using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blogs.Contracts.Requests
{
    public class RegistrationRequest
    {
        public required string FirstName {  get; set; } 
        public required string LastName { get; set; }
        [EmailAddress]
        public required string Email {  get; set; }
        public required string Password {  get; set; }
    }
}

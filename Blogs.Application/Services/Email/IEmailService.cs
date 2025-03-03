using Blogs.Application.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blogs.Application.Services.Email
{
    public interface IEmailService
    {
        Task SendEmailAsync(User user,string token,string confirmEmailPath,CancellationToken cancellationToken);
    }
}

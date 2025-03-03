using Blogs.Application.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blogs.Application.Services.Email
{
    public  interface IEmailVerificationService
    {
        Task<bool> CreateAsync(EmailVerificationToken emailVerification, CancellationToken token);
        Task<EmailVerificationToken?> GetByTokenAsync(string token, CancellationToken cancellationToken);
        Task<bool> DeleteByIdAsync(Guid id, CancellationToken token);
    }
}

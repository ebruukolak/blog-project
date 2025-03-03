using Blogs.Application.Models;
using Blogs.Application.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blogs.Application.Services.Email
{
    public class EmailVerificationService : IEmailVerificationService
    {
        private readonly IEmailVerificationRepository _emailVerificationRepository;
        public EmailVerificationService(IEmailVerificationRepository emailVerificationRepository)
        {
            _emailVerificationRepository = emailVerificationRepository;
        }
        public async Task<bool> CreateAsync(EmailVerificationToken emailVerification, CancellationToken token)
        {
            return await _emailVerificationRepository.CreateAsync(emailVerification, token);
        }
        public async Task<EmailVerificationToken?> GetByTokenAsync(string token, CancellationToken cancellationToken)
        {
            return await _emailVerificationRepository.GetByTokenAsync(token, cancellationToken);
        }

        public async Task<bool> DeleteByIdAsync(Guid id, CancellationToken token)
        {
            return await _emailVerificationRepository.DeleteByIdAsync(id, token);
        }

       
    }
}

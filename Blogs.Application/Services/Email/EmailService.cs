using Blogs.Application.AppSettingsModels;
using Blogs.Application.Exceptions;
using Blogs.Application.Models;
using MailKit.Net.Smtp;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using MimeKit;
using MimeKit.Text;

namespace Blogs.Application.Services.Email
{
    public class EmailService : IEmailService
    {
        private readonly EmailSettings _emailSettings;

        private readonly IHttpContextAccessor _httpContextAccessor;
        public EmailService(IOptions<EmailSettings> emailSettings, IHttpContextAccessor httpContextAccessor)
        {
            _emailSettings = emailSettings.Value;

            _httpContextAccessor = httpContextAccessor;
        }
        public async Task SendEmailAsync(User user,string token, string confirEmailPath, CancellationToken cancellationToken)
        {
            var email = new MimeMessage();
            email.From.Add(new MailboxAddress(_emailSettings.Sender, _emailSettings.SenderEmail));
            email.To.Add(MailboxAddress.Parse(user.Email));
            email.Subject="Email verification for IShouldTryIt";

            var confirmationLink = GenerateEmailConfirmationLink(user.Id,token, confirEmailPath);

            string emailBody = $"<p>Welcome to the IShouldTryIt! Click <a href='{confirmationLink}'>here</a> to confirm your email.</p>";

            email.Body = new TextPart(TextFormat.Html) {Text= emailBody };

            using var smtpClient = new SmtpClient();
            try
            {
                await smtpClient.ConnectAsync(_emailSettings.SmtpServer, _emailSettings.SmtpPort, _emailSettings.EnableSsl, cancellationToken);
                if (!string.IsNullOrEmpty(_emailSettings.SenderPassword))
                {
                    await smtpClient.AuthenticateAsync(_emailSettings.SenderEmail, _emailSettings.SenderPassword);
                }
                await smtpClient.SendAsync(email);
                await smtpClient.DisconnectAsync(true);
            }
            catch (Exception ex)
            {
                throw new AppException($"An error occurred while sending the email: {ex.Message}");
            }
        }

        private string GetBaseUrl()
        {
            var request = _httpContextAccessor.HttpContext.Request;
            if (request is null) return "https://localhost:5274/api";

            return $"{request.Scheme}://{request.Host}/api";
        }

        private string GenerateEmailConfirmationLink(Guid userId, string token, string confirmEmailEndpoint)
        {
            string baseUrl = GetBaseUrl();
            var uriBuilder = new UriBuilder(baseUrl)
            {
                Path = confirmEmailEndpoint,
                Query = $"userId={userId}&token={token}"
            };

            return uriBuilder.ToString();

        }
    }
}

using DeveloperLandingApi.Application.Interfaces;
using DeveloperLandingApi.Infrastructure.Options;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System.Net;
using System.Net.Mail;

namespace DeveloperLandingApi.Infrastructure.Email
{
    public class SmtpEmailSender : IEmailSender
    {
        private readonly SmtpOptions _options;
        private readonly ILogger<SmtpEmailSender> _logger;
        public SmtpEmailSender(IOptions<SmtpOptions> options, ILogger<SmtpEmailSender> logger)
        {
            _options = options.Value;
            _logger = logger;
        }

        public async Task SendAsync(
            string to,
            string subject,
            string body)
        {
            using var client = new SmtpClient(
                _options.Host,
                _options.Port)
            {
                Credentials = new NetworkCredential(
                    _options.Username,
                    _options.Password),

                EnableSsl = true
            };


            var mail =
                new MailMessage(
                    _options.FromEmail,
                    to,
                    subject,
                    body);

            await client.SendMailAsync(mail);
        }
    }
}

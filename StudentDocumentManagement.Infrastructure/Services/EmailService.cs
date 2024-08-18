using MailKit.Net.Smtp;
using Microsoft.Extensions.Options;
using MimeKit;
using StudentDocumentManagement.Core.Application.Dtos.Emails;
using StudentDocumentManagement.Core.Application.Interfaces;
using StudentDocumentManagement.Core.Domain.Settings;

namespace StudentDocumentManagement.Infrastructure.Services;

public class EmailService : IEmailService
{
    private readonly MailSettings _mailSettings;

    public EmailService(IOptions<MailSettings> mailSettings)
    {
        _mailSettings = mailSettings.Value;
    }

    public async Task SendAsync(EmailRequestDto req)
    {
        MimeMessage email = new();

        email.Sender = MailboxAddress.Parse($"{_mailSettings.DisplayName} < {_mailSettings.EmailFrom}>");
        email.To.Add(MailboxAddress.Parse(req.To));
        email.Subject = req.Subject;

        BodyBuilder builder = new();
        builder.HtmlBody = req.Body;
        email.Body = builder.ToMessageBody();

        using SmtpClient smtp = new();

        smtp.ServerCertificateValidationCallback = (s, c, h, e) => true;
        smtp.Connect(_mailSettings.SmtpHost, _mailSettings.SmtpPort, MailKit.Security.SecureSocketOptions.StartTls);
        smtp.Authenticate(_mailSettings.SmtpUser, _mailSettings.SmtpPass);
        await smtp.SendAsync(email);
        smtp.Disconnect(true);
    }
}

using StudentDocumentManagement.Core.Application.Dtos.Emails;

namespace StudentDocumentManagement.Core.Application.Interfaces;

public interface IEmailService
{
    Task SendAsync(EmailRequestDto req);
}

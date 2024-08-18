using MediatR;
using StudentDocumentManagement.Core.Application.Dtos.Emails;
using StudentDocumentManagement.Core.Application.Interfaces;

namespace StudentDocumentManagement.Core.Application.Students.Events.StudentRegistered;

internal class StudentRegisteredEventHandler : INotificationHandler<StudentRegisteredEvent>
{
    private readonly IEmailService _emailService;

    public StudentRegisteredEventHandler(IEmailService emailService)
    {
        _emailService = emailService;
    }

    public async Task Handle(StudentRegisteredEvent notification, CancellationToken cancellationToken)
    {
        var subject = "Bienvenido a Student Document Management App";
        var body = $"Se ha creado su cuenta con exito, ahora solo debe acceder con su usuario { notification.Username } " +
            $"y su contraseña { notification.Password }.";
        var emailRequest = new EmailRequestDto(notification.Email,
            subject, body);

        await _emailService.SendAsync(emailRequest);
    }
}

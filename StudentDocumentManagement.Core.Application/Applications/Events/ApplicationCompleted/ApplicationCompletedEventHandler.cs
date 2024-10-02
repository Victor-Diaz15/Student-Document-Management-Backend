using MediatR;
using StudentDocumentManagement.Core.Application.Dtos.Applications;
using StudentDocumentManagement.Core.Application.Dtos.Emails;
using StudentDocumentManagement.Core.Application.Interfaces;
using StudentDocumentManagement.Core.Domain.Abstractions;

namespace StudentDocumentManagement.Core.Application.Applications.Events.ApplicationCompleted;

internal class ApplicationCompletedEventHandler : INotificationHandler<ApplicationCompletedEvent>
{
    private readonly IEmailService _emailService;
    private readonly IApplicationRepository _applicationRepository;
    private readonly IAccountService _accountService;

    public ApplicationCompletedEventHandler(IEmailService emailService, IApplicationRepository applicationRepository, IAccountService accountService)
    {
        _emailService = emailService;
        _applicationRepository = applicationRepository;
        _accountService = accountService;
    }

    public async Task Handle(ApplicationCompletedEvent notification, CancellationToken cancellationToken)
    {
        //primero necesito buscar la informacion de cada application.
        var listApplications = await _applicationRepository.GetApplicationsByApplicationsIds(notification.ApplicationIds);

        foreach (var application in listApplications)
        {
            var student = await _accountService.GetStudentById(application.StudentId.ToString());

            if (student.Success)
            {
                var subject = $"Solicitud Completada";
                var body = $"Saludos { student.Data!.FirstName } { student.Data!.LastName } se ha completado " +
                    $"su solicitud { application.Service!.Name } ({ application.Service.ServiceType }) " +
                    $"y se le ha sido asignado el numero de oficio { notification.ApplicationNumberId }";

                var emailRequest = new EmailRequestDto(student.Data!.Email,
                    subject, body);

                await _emailService.SendAsync(emailRequest);
            }
        }
    }
}

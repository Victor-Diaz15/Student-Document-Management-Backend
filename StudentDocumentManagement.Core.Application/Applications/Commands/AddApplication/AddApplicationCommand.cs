
using StudentDocumentManagement.Core.Application.Dtos.Applications;
using StudentDocumentManagement.Core.Application.Interfaces.Messaging;

namespace StudentDocumentManagement.Core.Application.Applications.Commands.AddApplication;

public sealed record AddApplicationCommand(
    Guid ServiceId,
    List<ApplicationStudentFileCreateDto> Files
    ) : ICommand;

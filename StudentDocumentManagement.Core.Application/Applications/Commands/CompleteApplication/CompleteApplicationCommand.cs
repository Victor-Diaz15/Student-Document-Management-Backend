
using StudentDocumentManagement.Core.Application.Interfaces.Messaging;

namespace StudentDocumentManagement.Core.Application.Applications.Commands.CompleteApplication;

public sealed record CompleteApplicationCommand(List<Guid> ApplicationIds) : ICommand<string>;

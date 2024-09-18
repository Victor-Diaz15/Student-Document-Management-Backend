
using StudentDocumentManagement.Core.Application.Interfaces.Messaging;

namespace StudentDocumentManagement.Core.Application.Applications.Commands.PayApplication;

public sealed record PayApplicationCommand(Guid ApplicationId) : ICommand;

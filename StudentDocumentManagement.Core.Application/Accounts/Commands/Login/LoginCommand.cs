using StudentDocumentManagement.Core.Application.Dtos.Accounts;
using StudentDocumentManagement.Core.Application.Interfaces.Messaging;

namespace StudentDocumentManagement.Core.Application.Accounts.Commands.Login;

public sealed record LoginCommand(string UserName, string Password) : ICommand<AuthenticationResponseDto>;

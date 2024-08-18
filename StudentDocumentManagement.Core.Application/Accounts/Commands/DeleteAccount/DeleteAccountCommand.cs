using StudentDocumentManagement.Core.Application.Interfaces.Messaging;

namespace StudentDocumentManagement.Core.Application.Accounts.Commands.DeleteAccount;

public sealed record DeleteAccountCommand(string Id) : ICommand;

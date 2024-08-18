using StudentDocumentManagement.Core.Application.Interfaces;
using StudentDocumentManagement.Core.Application.Interfaces.Messaging;
using StudentDocumentManagement.Core.Application.Shared.Results;

namespace StudentDocumentManagement.Core.Application.Accounts.Commands.DeleteAccount;

internal class DeleteAccountCommandHandler : ICommandHandler<DeleteAccountCommand>
{
    private readonly IAccountService _accountService;

    public DeleteAccountCommandHandler(IAccountService accountService)
    {
        _accountService = accountService;
    }

    public async Task<Result> Handle(DeleteAccountCommand request, CancellationToken cancellationToken)
    {
        var result = await _accountService.DeleteUserAsync(request.Id);

        return result;
    }
}

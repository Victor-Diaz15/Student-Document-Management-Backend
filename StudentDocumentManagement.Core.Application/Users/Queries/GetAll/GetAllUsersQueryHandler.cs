using MediatR;
using StudentDocumentManagement.Core.Application.Dtos.Accounts;
using StudentDocumentManagement.Core.Application.Interfaces;
using StudentDocumentManagement.Core.Application.Interfaces.Messaging;
using StudentDocumentManagement.Core.Application.Shared.Results;

namespace StudentDocumentManagement.Core.Application.Users.Queries.GetAll;

internal class GetAllUsersQueryHandler : IQueryHandler<GetAllUsersQuery, List<UserDto>>
{
    private readonly IAccountService _accountService;

    public GetAllUsersQueryHandler(IAccountService accountService)
    {
        _accountService = accountService;
    }

    public async Task<ResultT<List<UserDto>>> Handle(GetAllUsersQuery request, CancellationToken cancellationToken)
    {
        return await _accountService.GetAllUsers();
    }
}

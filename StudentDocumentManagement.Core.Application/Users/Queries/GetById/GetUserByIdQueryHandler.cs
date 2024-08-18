using StudentDocumentManagement.Core.Application.Dtos.Accounts;
using StudentDocumentManagement.Core.Application.Interfaces;
using StudentDocumentManagement.Core.Application.Interfaces.Messaging;
using StudentDocumentManagement.Core.Application.Shared.Results;

namespace StudentDocumentManagement.Core.Application.Users.Queries.GetById;

internal class GetUserByIdQueryHandler : IQueryHandler<GetUserByIdQuery, UserDto>
{

    private readonly IAccountService _accountService;

    public GetUserByIdQueryHandler(IAccountService accountService)
    {
        _accountService = accountService;
    }

    public async Task<ResultT<UserDto>> Handle(GetUserByIdQuery request, CancellationToken cancellationToken)
    {
        var result = await _accountService.GetUserById(request.UserId);

        return result;
    }
}

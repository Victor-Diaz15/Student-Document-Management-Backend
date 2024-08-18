using StudentDocumentManagement.Core.Application.Dtos.Accounts;
using StudentDocumentManagement.Core.Application.Interfaces;
using StudentDocumentManagement.Core.Application.Interfaces.Messaging;
using StudentDocumentManagement.Core.Application.Shared.Results;

namespace StudentDocumentManagement.Core.Application.Accounts.Commands.Login;

internal class LoginCommandHandler : ICommandHandler<LoginCommand, AuthenticationResponseDto>
{
    private readonly IAccountService _accountService;

    public LoginCommandHandler(IAccountService accountService)
    {
        _accountService = accountService;
    }

    public async Task<ResultT<AuthenticationResponseDto>> Handle(LoginCommand request, CancellationToken cancellationToken)
    {
        var loginDto = new AuthenticationRequestDto()
        {
            UserName = request.UserName,
            Password = request.Password
        };

        var result = await _accountService.AuthenticationAsync(loginDto);

        return result;
    }
}

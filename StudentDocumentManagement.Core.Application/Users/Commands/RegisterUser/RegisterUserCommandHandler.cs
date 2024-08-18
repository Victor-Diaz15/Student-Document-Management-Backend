using StudentDocumentManagement.Core.Application.Dtos.Accounts;
using StudentDocumentManagement.Core.Application.Interfaces;
using StudentDocumentManagement.Core.Application.Interfaces.Messaging;
using StudentDocumentManagement.Core.Application.Shared.Results;

namespace StudentDocumentManagement.Core.Application.Users.Commands.RegisterUser;

internal class RegisterUserCommandHandler : ICommandHandler<RegisterUserCommand>
{
    private readonly IAccountService _accountService;

    public RegisterUserCommandHandler(IAccountService accountService)
    {
        _accountService = accountService;
    }

    public async Task<Result> Handle(RegisterUserCommand request, CancellationToken cancellationToken)
    {
        var userDto = new RegisterUserRequestDto()
        {
            IdentityCard = request.IdentityCard,
            FirstName = request.FirstName,
            LastName = request.LastName,
            Email = request.Email,
            UserName = request.UserName,
            Password = request.Password,
            PhoneNumber = request.PhoneNumber,
            Rol = request.Rol,
            ProfilePicture = request.ProfilePicture,
        };

        var result = await _accountService.RegisterUserAsync(userDto);

        return result;
    }
}

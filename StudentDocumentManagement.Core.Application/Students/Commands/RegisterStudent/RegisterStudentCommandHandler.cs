using StudentDocumentManagement.Core.Application.Dtos.Accounts;
using StudentDocumentManagement.Core.Application.Interfaces;
using StudentDocumentManagement.Core.Application.Interfaces.Messaging;
using StudentDocumentManagement.Core.Application.Shared.Results;

namespace StudentDocumentManagement.Core.Application.Students.Commands.RegisterStudent;

internal class RegisterStudentCommandHandler : ICommandHandler<RegisterStudentCommand>
{
    private readonly IAccountService _accountService;

    public RegisterStudentCommandHandler(IAccountService accountService)
    {
        _accountService = accountService;
    }

    public async Task<Result> Handle(RegisterStudentCommand request, CancellationToken cancellationToken)
    {
        var studentReqDto = new RegisterStudentRequestDto()
        {
            IdentityCard = request.IdentityCard,
            FirstName = request.FirstName,
            LastName = request.LastName,
            Email = request.Email,
            Password = request.Password,
            PhoneNumber = request.PhoneNumber,
            ProfilePicture = request.ProfilePicture,
            Rol = request.Rol
        };

        var result = await _accountService.RegisterStudentAsync(studentReqDto);

        return result;
    }
}

using MediatR;
using StudentDocumentManagement.Core.Application.Dtos.Accounts;
using StudentDocumentManagement.Core.Application.Interfaces;
using StudentDocumentManagement.Core.Application.Interfaces.Messaging;
using StudentDocumentManagement.Core.Application.Shared.Results;
using StudentDocumentManagement.Core.Application.Students.Events.StudentRegistered;

namespace StudentDocumentManagement.Core.Application.Students.Commands.RegisterStudent;

internal class RegisterStudentCommandHandler : ICommandHandler<RegisterStudentCommand>
{
    private readonly IAccountService _accountService;
    private readonly IMediator _mediator;

    public RegisterStudentCommandHandler(IAccountService accountService, IMediator mediator)
    {
        _accountService = accountService;
        _mediator = mediator;
    }

    public async Task<Result> Handle(RegisterStudentCommand request, CancellationToken cancellationToken)
    {
        var emailExisted = await _accountService.IsEmailUniqueAsync(request.Email);
        if (emailExisted)
        {
            return new Result(false, $"Email '{request.Email}' already registered.");
        }

        var identityCardExisted = await _accountService.IsIdentityCardUniqueAsync(request.IdentityCard);
        if (identityCardExisted)
        {
            return new Result(false, $"IdentityCard '{request.IdentityCard}' already registered.");
        }

        var studentReqDto = new RegisterStudentRequestDto()
        {
            IdentityCard = request.IdentityCard,
            FirstName = request.FirstName,
            LastName = request.LastName,
            Email = request.Email,
            Password = request.Password,
            PhoneNumber = request.PhoneNumber,
            ProfilePicture = request.ProfilePicture
        };

        var result = await _accountService.RegisterStudentAsync(studentReqDto);

        if (result.Success) 
        {
            var notification = new StudentRegisteredEvent(request.Email, result.Data!.Username, request.Password);
            await _mediator.Publish(notification);
        }

        return result;
    }
}

using FluentValidation;

namespace StudentDocumentManagement.Core.Application.Accounts.Commands.Login;

public class LoginCommandValidator : AbstractValidator<LoginCommand>
{
    public LoginCommandValidator()
    {
        RuleFor(x => x.UserName).NotEmpty();

        RuleFor(x => x.Password).NotEmpty();
    }
}

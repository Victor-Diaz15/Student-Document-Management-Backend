using FluentValidation;

namespace StudentDocumentManagement.Core.Application.Students.Commands.RegisterStudent;

public class RegisterStudentCommandValidator : AbstractValidator<RegisterStudentCommand>
{
    public RegisterStudentCommandValidator()
    {
        RuleFor(x => x.IdentityCard)
            .NotEmpty()
            .Length(11);

        RuleFor(x => x.FirstName)
            .NotEmpty()
            .MaximumLength(50);

        RuleFor(x => x.LastName)
            .NotEmpty()
            .MaximumLength(50);

        RuleFor(x => x.Email)
            .NotEmpty()
            .EmailAddress();

        RuleFor(x => x.Password)
            .NotEmpty();

        RuleFor(x => x.PhoneNumber)
            .NotEmpty()
            .Length(10);

        RuleFor(x => x.Rol)
            .NotEmpty();
        
    }
}

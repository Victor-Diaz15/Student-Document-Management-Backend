using FluentValidation;

namespace StudentDocumentManagement.Core.Application.Applications.Commands.AddApplication;

public class AddApplicationCommandValidator : AbstractValidator<AddApplicationCommand>
{
    public AddApplicationCommandValidator()
    {
        RuleFor(x => x.ServiceId)
            .NotEmpty();

        RuleForEach(x => x.Files)
            .NotEmpty();
    }
}

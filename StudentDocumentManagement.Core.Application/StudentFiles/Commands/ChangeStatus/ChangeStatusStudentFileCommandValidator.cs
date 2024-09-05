using FluentValidation;

namespace StudentDocumentManagement.Core.Application.StudentFiles.Commands.ChangeStatus;

public class ChangeStatusStudentFileCommandValidator : AbstractValidator<ChangeStatusStudentFileCommand>
{
    public ChangeStatusStudentFileCommandValidator()
    {
        RuleFor(x => x.Status)
            .IsInEnum().WithMessage("'Status' must be a valid enum value.");
    }
}

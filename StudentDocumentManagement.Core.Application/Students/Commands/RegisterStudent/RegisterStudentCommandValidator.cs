using FluentValidation;
using Microsoft.Extensions.Options;
using StudentDocumentManagement.Core.Application.Helpers;
using StudentDocumentManagement.Core.Domain.Settings;

namespace StudentDocumentManagement.Core.Application.Students.Commands.RegisterStudent;

public class RegisterStudentCommandValidator : AbstractValidator<RegisterStudentCommand>
{
    private readonly FileUploadSettings _fileUploadSettings;

    public RegisterStudentCommandValidator(IOptions<FileUploadSettings> fileUploadSettings)
    {
        _fileUploadSettings = fileUploadSettings.Value;

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

        // Validar el archivo solo si fue proporcionado
        When(x => x.ProfilePicture != null, () =>
        {
            RuleFor(x => x.ProfilePicture)
                .Must(file => FileValidationHelper.BeAValidFileType(file!, _fileUploadSettings.PermittedExtensions))
                .WithMessage("Invalid file type. Only .jpg, .jpeg, .png are allowed.")
                .Must(file => FileValidationHelper.BeAValidFileSize(file!, _fileUploadSettings.MaxFileSize))
                .WithMessage($"File size must be less than {_fileUploadSettings.MaxFileSize / (1024 * 1024)}MB.");
        });


    }
}

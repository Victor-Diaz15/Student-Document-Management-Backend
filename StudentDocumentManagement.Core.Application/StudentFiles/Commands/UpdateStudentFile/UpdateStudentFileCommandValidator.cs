using FluentValidation;
using FluentValidation.Validators;
using Microsoft.Extensions.Options;
using StudentDocumentManagement.Core.Application.Helpers;
using StudentDocumentManagement.Core.Domain.Settings;

namespace StudentDocumentManagement.Core.Application.StudentFiles.Commands.UpdateStudentFile;

public class UpdateStudentFileCommandValidator : AbstractValidator<UpdateStudentFileCommand>
{
    private readonly FileUploadSettings _fileUploadSettings;

    public UpdateStudentFileCommandValidator(IOptions<FileUploadSettings> fileUploadSettings)
    {
        _fileUploadSettings = fileUploadSettings.Value;

        RuleFor(x => x.File)
            .Must(file => FileValidationHelper.BeAValidFileType(file!, _fileUploadSettings.PermittedExtensions))
            .WithMessage("Invalid file type. Only .jpg, .jpeg, .png, .pdf are allowed.")
            .Must(file => FileValidationHelper.BeAValidFileSize(file!, _fileUploadSettings.MaxFileSize))
            .WithMessage($"File size must be less than {_fileUploadSettings.MaxFileSize / (1024 * 1024)}MB.");

        RuleFor(x => x.FileType)
            .IsInEnum().WithMessage("'File Type' must be a valid enum value.");
    }
}

using Microsoft.AspNetCore.Http;

namespace StudentDocumentManagement.Core.Application.Helpers;

public static class FileValidationHelper
{
    public static bool BeAValidFileType(IFormFile file, string[] permittedExtensions)
    {
        var extension = Path.GetExtension(file.FileName);
        return permittedExtensions.Contains(extension.ToLower());
    }

    public static bool BeAValidFileSize(IFormFile file, long maxFileSize)
    {
        return file.Length <= maxFileSize;
    }
}
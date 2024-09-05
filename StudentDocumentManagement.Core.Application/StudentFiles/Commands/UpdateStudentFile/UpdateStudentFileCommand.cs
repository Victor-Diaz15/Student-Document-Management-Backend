
using Microsoft.AspNetCore.Http;
using StudentDocumentManagement.Core.Application.Interfaces.Messaging;
using StudentDocumentManagement.Core.Domain.Enums;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace StudentDocumentManagement.Core.Application.StudentFiles.Commands.UpdateStudentFile;

public sealed record UpdateStudentFileCommand(
    [DataType(DataType.Upload)]
    IFormFile File,
    StudentFileType FileType
    ) : ICommand
{
    public Guid FileId { get; set; }
}

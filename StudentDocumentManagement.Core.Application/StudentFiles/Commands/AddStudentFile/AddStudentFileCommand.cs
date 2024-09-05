using Microsoft.AspNetCore.Http;
using StudentDocumentManagement.Core.Application.Interfaces.Messaging;
using StudentDocumentManagement.Core.Domain.Enums;
using System.ComponentModel.DataAnnotations;

namespace StudentDocumentManagement.Core.Application.StudentFiles.Commands.AddStudentFile;

public sealed record AddStudentFileCommand(
    [DataType(DataType.Upload)]
    IFormFile File,
    StudentFileType FileType
    ) : ICommand<Guid>;

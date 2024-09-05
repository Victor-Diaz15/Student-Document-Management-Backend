using StudentDocumentManagement.Core.Domain.Entities;
using StudentDocumentManagement.Core.Domain.Enums;

namespace StudentDocumentManagement.Core.Application.Dtos.StudentFiles;

public class StudentFileDto
{
    public Guid Id { get; set; }
    public Guid StudentId { get; set; }
    public string FileUrl { get; set; } = string.Empty;
    public string FileType { get; set; } = string.Empty;
    public string Status { get; set; } = string.Empty;
}

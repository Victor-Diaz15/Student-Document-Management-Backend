using StudentDocumentManagement.Core.Domain.Commons;
using StudentDocumentManagement.Core.Domain.Enums;

namespace StudentDocumentManagement.Core.Domain.Entities;

public class StudentFile : AuditableEntityBase
{
    public Guid StudentId { get; set; }
    public string Url { get; set; } = string.Empty;
    public StudentFileType? FileType { get; set; }
    public FileStatus Status { get; set; }
    public List<ApplicationStudentFile>? ApplicationsFiles { get; set; }
}

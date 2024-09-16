using StudentDocumentManagement.Core.Domain.Commons;
using StudentDocumentManagement.Core.Domain.Enums;

namespace StudentDocumentManagement.Core.Domain.Entities;

public class Application : AuditableEntityBase
{
    public Guid StudentId { get; set; }
    public Guid ServiceId { get; set; }
    public Service? Service { get; set; }
    public ApplicationStatus Status { get; set; }
    public List<ApplicationStudentFile>? Files { get; set; }
    public Guid? ApplicationNumberId { get; set; }
}

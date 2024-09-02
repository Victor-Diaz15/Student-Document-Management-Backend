using StudentDocumentManagement.Core.Domain.Entities;

namespace StudentDocumentManagement.Infrastructure.Identity.Entities;

public class Student : UserApp
{
    public string StudentId { get; set; } = string.Empty;
}

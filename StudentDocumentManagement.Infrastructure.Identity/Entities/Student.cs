using StudentDocumentManagement.Core.Domain.Entities;

namespace StudentDocumentManagement.Infrastructure.Identity.Entities;

public class Student : UserApp
{
    public string StudentId { get; set; } = string.Empty;
    public List<StudentFile> Files { get; set; } = [];
    public List<Application> Applications { get; set; } = [];

}

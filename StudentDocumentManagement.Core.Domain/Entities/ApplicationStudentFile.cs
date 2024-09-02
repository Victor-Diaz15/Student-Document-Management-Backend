namespace StudentDocumentManagement.Core.Domain.Entities;

public class ApplicationStudentFile
{
    public Guid ApplicationId { get; set; }
    public Application Application { get; set; } = new();
    public Guid StudentFileId { get; set; }
    public StudentFile StudentFile { get; set; } = new();
}

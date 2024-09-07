namespace StudentDocumentManagement.Core.Domain.Entities;

public class ApplicationStudentFile
{
    public Guid ApplicationId { get; set; }
    public Application? Application { get; set; }
    public Guid StudentFileId { get; set; }
    public StudentFile? StudentFile { get; set; }
}

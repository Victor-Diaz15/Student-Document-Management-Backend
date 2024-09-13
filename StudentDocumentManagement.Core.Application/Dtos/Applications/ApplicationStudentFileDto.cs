namespace StudentDocumentManagement.Core.Application.Dtos.Applications;

public class ApplicationStudentFileDto
{
    public Guid StudentFileId { get; set; }
    public string Url { get; set; } = string.Empty;
    public string FileType { get; set; } = string.Empty;
    public string Status { get; set; } = string.Empty;
}
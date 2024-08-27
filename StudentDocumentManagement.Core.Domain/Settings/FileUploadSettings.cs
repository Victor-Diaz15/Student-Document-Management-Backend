namespace StudentDocumentManagement.Core.Domain.Settings;

public class FileUploadSettings
{
    public string[] PermittedExtensions { get; set; } = [];
    public long MaxFileSize { get; set; }
}

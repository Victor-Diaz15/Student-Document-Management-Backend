using StudentDocumentManagement.Core.Domain.Enums;

namespace StudentDocumentManagement.Core.Application.Dtos.Services;

public class ServiceDto
{
    public Guid Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public int Price { get; set; }
    public string ServiceType { get; set; } = string.Empty;
}

using StudentDocumentManagement.Core.Domain.Commons;

namespace StudentDocumentManagement.Core.Domain.Entities;

public class Service : AuditableEntityBase
{
    public string Name { get; set; } = string.Empty;
    public int Price { get; set; }
    public ServiceType ServiceType { get; set; }
    public List<Application> Applications { get; set; } = [];
}

public enum ServiceType
{
    MESCYT,
    PERSONAL
}

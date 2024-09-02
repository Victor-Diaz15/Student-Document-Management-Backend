using StudentDocumentManagement.Core.Domain.Abstractions;

namespace StudentDocumentManagement.Core.Domain.Commons;

public abstract class AuditableEntityBase : IEntityBase
{
    public Guid Id { get; set; }
    public bool Borrado { get; set; }
}

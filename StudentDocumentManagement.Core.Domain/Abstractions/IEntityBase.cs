namespace StudentDocumentManagement.Core.Domain.Abstractions;

public interface IEntityBase
{
    public Guid Id { get; set; }
    public bool Borrado { get; set; }
}

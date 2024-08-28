namespace StudentDocumentManagement.Core.Domain.Abstractions;

public interface IUnitOfWork
{
    Task SaveChangesAsync();
}

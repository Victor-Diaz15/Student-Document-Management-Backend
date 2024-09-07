
namespace StudentDocumentManagement.Core.Domain.Abstractions;

public interface IApplicationRepository : IGenericBaseRepository<Entities.Application>
{
    Task<List<Entities.Application>> GetAllWithIncludeAndThenInclude();
}

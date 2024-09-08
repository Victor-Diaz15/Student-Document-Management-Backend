
namespace StudentDocumentManagement.Core.Domain.Abstractions;

public interface IApplicationRepository : IGenericBaseRepository<Entities.Application>
{
    Task<Entities.Application?> GetByIdWithIncludeAndThenInclude(Guid applicationId);
    Task<List<Entities.Application>> GetAllWithIncludeAndThenInclude();
}

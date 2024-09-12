
using StudentDocumentManagement.Core.Domain.Enums;

namespace StudentDocumentManagement.Core.Domain.Abstractions;

public interface IApplicationRepository : IGenericBaseRepository<Entities.Application>
{
    Task<Entities.Application?> GetByIdWithIncludeAndThenInclude(Guid applicationId);
    Task<List<Entities.Application>> GetAllWithIncludeAndThenInclude();
    Task<List<Entities.Application>> GetApplicationsByFilters(string? studentId, string? serviceId, ApplicationStatus? status);
}

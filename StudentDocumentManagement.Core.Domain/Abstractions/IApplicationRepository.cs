
using StudentDocumentManagement.Core.Domain.Enums;

namespace StudentDocumentManagement.Core.Domain.Abstractions;

public interface IApplicationRepository : IGenericBaseRepository<Entities.Application>
{
    Task<Entities.Application?> GetByIdWithIncludeAndThenInclude(Guid applicationId);
    Task<List<Entities.Application>> GetAllWithIncludeAndThenInclude();
    Task<List<Entities.Application>> GetApplicationsByFilters(string? studentId, string? applicationNumberId, string? serviceId, ApplicationStatus? status);
    Task<Entities.Application?> GetApplicationToUpdate(Guid applicationId);
    Task<string> CompleteApplication(List<Guid> applicationIds);
    Task<List<Entities.Application>> GetApplicationsByApplicationsIds(List<Guid> applicationIds);

}

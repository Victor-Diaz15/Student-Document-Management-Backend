using StudentDocumentManagement.Core.Domain.Entities;

namespace StudentDocumentManagement.Core.Domain.Abstractions;

public interface IStudentFileRepository : IGenericBaseRepository<StudentFile>
{
    Task<List<StudentFile>> GetAllStudentFilesAsync(Guid studentId);
    Task<List<StudentFile>> GetAllStudentFilesByApplicationIdAsync(Guid applicationId);
    Task<StudentFile?> GetByIdWithIncludeAndThenInclude(Guid StudentFileId);
}

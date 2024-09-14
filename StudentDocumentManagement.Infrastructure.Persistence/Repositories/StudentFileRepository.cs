using Azure.Core;
using Microsoft.EntityFrameworkCore;
using StudentDocumentManagement.Core.Domain.Abstractions;
using StudentDocumentManagement.Core.Domain.Entities;
using StudentDocumentManagement.Infrastructure.Persistence.Contexts;

namespace StudentDocumentManagement.Infrastructure.Persistence.Repositories;

public class StudentFileRepository : GenericBaseRepository<StudentFile>, IStudentFileRepository
{
    private readonly MainDbContext _dbContext;

    public StudentFileRepository(MainDbContext dbContext) : base(dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<List<StudentFile>> GetAllStudentFilesAsync(Guid studentId)
    {
        var query = base.GetQueryable();

        var studentFiles = await query
            .Where(x => x.StudentId == studentId)
            .ToListAsync();

        return studentFiles;
    }

    public async Task<List<StudentFile>> GetAllStudentFilesByApplicationIdAsync(Guid applicationId)
    {
        var query = base.GetQueryable();

        var studentFiles = await query
            .Where(x => x.ApplicationsFiles!.Select(x => x.ApplicationId).First() == applicationId)
            .ToListAsync();

        return studentFiles;
    }

    public async Task<StudentFile?> GetByIdWithIncludeAndThenInclude(Guid StudentFileId)
    {
        var file = await _dbContext.StudentFiles
            .Include(x => x.ApplicationsFiles!)
            .ThenInclude(x => x.Application)
            .FirstOrDefaultAsync(x => x.Id == StudentFileId);

        return file;
    }
}

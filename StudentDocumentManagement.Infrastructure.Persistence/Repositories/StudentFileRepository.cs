using Azure.Core;
using Microsoft.EntityFrameworkCore;
using StudentDocumentManagement.Core.Domain.Abstractions;
using StudentDocumentManagement.Core.Domain.Entities;
using StudentDocumentManagement.Infrastructure.Persistence.Contexts;

namespace StudentDocumentManagement.Infrastructure.Persistence.Repositories;

public class StudentFileRepository : GenericBaseRepository<StudentFile>, IStudentFileRepository
{
    public StudentFileRepository(MainDbContext dbContext) : base(dbContext)
    {
    }

    public async Task<List<StudentFile>> GetAllStudentFilesAsync(Guid studentId)
    {
        var query = base.GetQueryable();

        var studentFiles = await query
            .Where(x => x.StudentId == studentId)
            .ToListAsync();

        return studentFiles;
    }
}

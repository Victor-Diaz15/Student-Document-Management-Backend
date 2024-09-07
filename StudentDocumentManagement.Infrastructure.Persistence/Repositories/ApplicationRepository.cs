using Microsoft.EntityFrameworkCore;
using StudentDocumentManagement.Core.Domain.Abstractions;
using StudentDocumentManagement.Core.Domain.Entities;
using StudentDocumentManagement.Infrastructure.Persistence.Contexts;

namespace StudentDocumentManagement.Infrastructure.Persistence.Repositories;

public class ApplicationRepository : GenericBaseRepository<Application>, IApplicationRepository
{
    private readonly MainDbContext _dbContext;
    public ApplicationRepository(MainDbContext dbContext) : base(dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<List<Application>> GetAllWithIncludeAndThenInclude()
    {
        var list = await _dbContext.Applications
            .AsNoTracking()
            .Include(x => x.Service!)
            .Include(x => x.Files!)
            .ThenInclude(x => x.StudentFile)
            .ToListAsync();

        return list;
    }
}

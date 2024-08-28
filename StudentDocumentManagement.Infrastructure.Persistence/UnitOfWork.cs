using StudentDocumentManagement.Core.Domain.Abstractions;
using StudentDocumentManagement.Infrastructure.Persistence.Contexts;

namespace StudentDocumentManagement.Infrastructure.Persistence;

public class UnitOfWork : IUnitOfWork
{
    private readonly MainDbContext _context;

    public UnitOfWork(MainDbContext context)
    {
        _context = context;
    }

    public async Task SaveChangesAsync()
    {
        await _context.SaveChangesAsync();
    }
}

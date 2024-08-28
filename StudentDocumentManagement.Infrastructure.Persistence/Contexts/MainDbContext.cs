using Microsoft.EntityFrameworkCore;

namespace StudentDocumentManagement.Infrastructure.Persistence.Contexts;

public class MainDbContext : DbContext
{
    public MainDbContext(DbContextOptions<MainDbContext> options) : base(options)
    {
        
    }
}

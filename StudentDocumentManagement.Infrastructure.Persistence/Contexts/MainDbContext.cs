using Microsoft.EntityFrameworkCore;
using StudentDocumentManagement.Core.Domain.Entities;
using StudentDocumentManagement.Core.Domain.SeedData;
using System.Reflection;

namespace StudentDocumentManagement.Infrastructure.Persistence.Contexts;

public class MainDbContext : DbContext
{
    public MainDbContext(DbContextOptions<MainDbContext> options) : base(options) {}

    //In this section the dbsets of the entities will be created to then be able to access the tables.
    #region DbSets

    //Example, **replace this once you have the entities of the project in question.**
    public DbSet<Service> Services { get; set; }
    public DbSet<Application> Applications { get; set; }
    public DbSet<StudentFile> StudentFiles { get; set; }
    public DbSet<ApplicationStudentFile> ApplicationsStudentFiles { get; set; }

    #endregion

    /// <summary>
    /// Using the fluent api, we will use the OnModelCreating method to configure and register
    /// The entities that we will be using in our project etc.
    /// </summary>
    /// <param name="builder"></param>
    protected override void OnModelCreating(ModelBuilder builder)
    {
        //Through reflection, this part of code allows us,
        //register all classes that implement IEntityTypeConfiguration automatically
        builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

        //Agregando data inicial para la entidad de servicios
        ServiceData.Seed(builder);

        base.OnModelCreating(builder);

    }
}

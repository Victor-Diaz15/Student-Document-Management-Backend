using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using StudentDocumentManagement.Infrastructure.Identity.Entities;
using StudentDocumentManagement.Infrastructure.Identity.Seeds;
using System.Reflection.Emit;

namespace StudentDocumentManagement.Infrastructure.Identity.Context;

public class IdentityContext : IdentityDbContext<UserApp>
{
    public IdentityContext(DbContextOptions<IdentityContext> options) : base(options) { }

    public DbSet<Student> Students { get; set; }

    protected override void OnModelCreating(ModelBuilder mb)
    {
        //FLUENT API

        mb.HasDefaultSchema("Identity");

        #region Tables

        mb.Entity<UserApp>(entity =>
        {
            entity.ToTable(name: "Users");
        });

        mb.Entity<IdentityRole>(entity =>
        {
            entity.ToTable(name: "Roles");
        });

        mb.Entity<IdentityUserRole<string>>(entity =>
        {
            entity.ToTable(name: "UserRoles");
        });

        mb.Entity<IdentityUserLogin<string>>(entity =>
        {
            entity.ToTable(name: "UserLogins");
        });

        #endregion

        #region Properties

        // Configura TPH (Table Per Hierarchy)
        mb.Entity<UserApp>()
            .HasDiscriminator<string>("UserType")
            .HasValue<Student>("Student");

        //Configurando el campo identity card.
        mb.Entity<UserApp>(entity =>
        {
            entity.HasIndex(x => x.IdentityCard)
            .HasDatabaseName("IX_IdentityCard")
            .IsUnique();

            entity.Property(x => x.IdentityCard)
                .HasMaxLength(11);
        });

        //Configurando el campo de matricula del estudiante para que sea unico.
        mb.Entity<Student>(entity =>
        {
            entity.HasIndex(x => x.StudentId)
                .HasDatabaseName("IX_StudentId")
                .IsUnique();
        });

        #endregion

        #region Seed Data

        //Agregar roles del sistema
        SeedData.SeedRoles(mb);

        //Agregar usuario admin
        SeedData.SeedAdmin(mb);

        #endregion

        base.OnModelCreating(mb);
    }
}

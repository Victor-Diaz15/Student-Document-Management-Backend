using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using StudentDocumentManagement.Core.Domain.Entities;

namespace StudentDocumentManagement.Infrastructure.Persistence.EntityConfigurations;

public class ApplicationStudentFileConfiguration : IEntityTypeConfiguration<ApplicationStudentFile>
{
    public void Configure(EntityTypeBuilder<ApplicationStudentFile> builder)
    {
        builder.ToTable("ApplicationsStudentFiles");

        //Key compuesta
        builder.HasKey(x => new { x.StudentFileId, x.ApplicationId });

        //Relations
        builder.HasOne(x => x.StudentFile)
            .WithMany(x => x.ApplicationsFiles)
            .HasForeignKey(x => x.StudentFileId)
            .OnDelete(DeleteBehavior.NoAction);

        builder.HasOne(x => x.Application)
            .WithMany(x => x.Files)
            .HasForeignKey(x => x.ApplicationId)
            .OnDelete(DeleteBehavior.NoAction);
    }
}

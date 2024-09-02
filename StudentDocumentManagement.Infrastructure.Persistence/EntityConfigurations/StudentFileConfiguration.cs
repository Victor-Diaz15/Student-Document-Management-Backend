using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using StudentDocumentManagement.Core.Domain.Entities;

namespace StudentDocumentManagement.Infrastructure.Persistence.EntityConfigurations;

public class StudentFileConfiguration : IEntityTypeConfiguration<StudentFile>
{
    public void Configure(EntityTypeBuilder<StudentFile> builder)
    {
        builder.ToTable("StudentFiles");
        builder.HasKey(x => x.Id);

        //Property Configurations
        builder.Property(x => x.Id).HasColumnName("StudentFileId");

        //Global Filter
        builder.HasQueryFilter(x => !x.Borrado);
    }
}

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using StudentDocumentManagement.Core.Domain.Entities;

namespace StudentDocumentManagement.Infrastructure.Persistence.EntityConfigurations;

public class ApplicationConfiguration : IEntityTypeConfiguration<Application>
{
    public void Configure(EntityTypeBuilder<Application> builder)
    {
        builder.ToTable("Applications");
        builder.HasKey(x => x.Id);

        //Property Configurations
        builder.Property(x => x.Id).HasColumnName("ApplicationId");

        //Global Filter
        builder.HasQueryFilter(x => !x.Borrado);

        //Relations
        builder.HasOne(x => x.Service)
            .WithMany(x => x.Applications)
            .HasForeignKey(x => x.ServiceId)
            .OnDelete(DeleteBehavior.NoAction);
    }
}

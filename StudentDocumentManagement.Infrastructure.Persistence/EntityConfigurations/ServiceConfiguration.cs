using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using StudentDocumentManagement.Core.Domain.Entities;

namespace StudentDocumentManagement.Infrastructure.Persistence.EntityConfigurations;

public class ServiceConfiguration : IEntityTypeConfiguration<Service>
{
    public void Configure(EntityTypeBuilder<Service> builder)
    {
        builder.ToTable("Services");
        builder.HasKey(x => x.Id);

        //Property Configurations
        builder.Property(x => x.Id).HasColumnName("ServiceId");

        //Global Filter
        builder.HasQueryFilter(x => !x.Borrado);
    }
}

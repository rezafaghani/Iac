using Iac.Domain.AggregateModels;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Iac.Infrastructure.EntityConfigurations
{
    public class LocationEntityTypeConfiguration : IEntityTypeConfiguration<LocationEntity>
    {
        public void Configure(EntityTypeBuilder<LocationEntity> builder)
        {
            builder.ToTable("locations", StackContext.DEFAULT_SCHEMA);
            builder.HasKey(cr => cr.Id);
            builder.Property(cr => cr.Title).IsRequired();
        }
    }
}
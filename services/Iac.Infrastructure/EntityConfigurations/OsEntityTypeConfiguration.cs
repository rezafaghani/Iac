using Iac.Domain.AggregateModels;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Iac.Infrastructure.EntityConfigurations
{
    public class OsEntityTypeConfiguration : IEntityTypeConfiguration<OsEntity>
    {
        public void Configure(EntityTypeBuilder<OsEntity> builder)
        {
            builder.ToTable("oses", StackContext.DEFAULT_SCHEMA);
            builder.HasKey(cr => cr.Id);
            builder.Property(cr => cr.Title).IsRequired();
            builder.Property(cr => cr.ImageName).IsRequired();
            builder.Property(cr => cr.Version).IsRequired();
            builder.HasOne(p => p.Location)
                .WithMany(b => b.Oses)
                .HasForeignKey(p => p.LocationId);
        }
    }
}
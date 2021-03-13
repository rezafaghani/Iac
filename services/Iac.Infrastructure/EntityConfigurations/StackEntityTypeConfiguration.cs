using Iac.Domain.AggregateModels;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Iac.Infrastructure.EntityConfigurations
{
    public class StackEntityTypeConfiguration:IEntityTypeConfiguration<StackEntity>
    {
        public void Configure(EntityTypeBuilder<StackEntity> builder)
        {
            builder.ToTable("stacks", StackContext.DEFAULT_SCHEMA);
            builder.HasKey(cr => cr.Id);
            builder.Property(cr => cr.StackName).IsRequired();
            builder.HasOne(p => p.Os)
                .WithMany(b => b.Stacks)
                .HasForeignKey(p => p.OsId);
        }
    }
}
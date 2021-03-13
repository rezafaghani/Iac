using Iac.Domain.AggregateModels;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Iac.Infrastructure.EntityConfigurations
{
    public class StackItemVariablesEntityTypeConfiguration :IEntityTypeConfiguration<StackItemVariablesEntity>
    {
        public void Configure(EntityTypeBuilder<StackItemVariablesEntity> builder)
        {
            builder.ToTable("stackitemvariables", StackContext.DEFAULT_SCHEMA);
            builder.HasKey(cr => cr.Id);
            builder.Property(cr => cr.Name).IsRequired();
            builder.Property(cr => cr.Value).IsRequired();
            builder.HasOne(p => p.StackItem)
                .WithMany(b => b.Variables)
                .HasForeignKey(p => p.StackItemId);
        }
    }
}
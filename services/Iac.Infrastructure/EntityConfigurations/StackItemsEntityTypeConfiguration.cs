using Iac.Domain.AggregateModels;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Iac.Infrastructure.EntityConfigurations
{
    public class StackItemsEntityTypeConfiguration:IEntityTypeConfiguration<StackItemsEntity>
    {
        public void Configure(EntityTypeBuilder<StackItemsEntity> builder)
        {
            builder.ToTable("stackitems", StackContext.DEFAULT_SCHEMA);
            builder.HasKey(cr => cr.Id);
            builder.HasOne(p => p.Stack)
                .WithMany(b => b.StackItems)
                .HasForeignKey(p => p.StackId);
            
            builder.HasOne(p => p.Network)
                .WithMany(b => b.StackItems)
                .HasForeignKey(p => p.NetworkId);
        }
    }
}
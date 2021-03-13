using Iac.Domain.AggregateModels;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Iac.Infrastructure.EntityConfigurations
{
     class NetworkEntityTypeConfiguration:IEntityTypeConfiguration<NetworkEntity>
     {
         public void Configure(EntityTypeBuilder<NetworkEntity> builder)
         {
             builder.ToTable("networks", StackContext.DEFAULT_SCHEMA);
             builder.HasKey(cr => cr.Id);
             builder.Property(cr => cr.Name).IsRequired();
             builder.Property(cr => cr.NetworkType).IsRequired();
         }
     }
}
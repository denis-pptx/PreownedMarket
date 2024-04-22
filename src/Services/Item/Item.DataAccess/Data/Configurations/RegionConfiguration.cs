using Item.DataAccess.Models.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Item.DataAccess.Data.Configurations;

public class RegionConfiguration : IEntityTypeConfiguration<Region>
{
    public void Configure(EntityTypeBuilder<Region> builder)
    {
        builder.HasKey(region => region.Id);

        builder.Property(region => region.Name).HasMaxLength(50).IsRequired();
        builder.HasIndex(region => region.Name).IsUnique();
    }
}
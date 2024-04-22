using Item.DataAccess.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Item.DataAccess.Data.Configurations;

public class CityConfiguration : IEntityTypeConfiguration<City>
{
    public void Configure(EntityTypeBuilder<City> builder)
    {
        builder.HasKey(city => city.Id);

        builder.Property(city => city.Name).HasMaxLength(50).IsRequired();
        builder.HasIndex(city => city.Name).IsUnique();

        builder.HasOne(city => city.Region)
            .WithMany(region => region.Cities)
            .OnDelete(DeleteBehavior.Restrict)
            .IsRequired();
    }
}
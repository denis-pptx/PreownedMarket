using Item.DataAccess.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Item.DataAccess.Data.Configurations;

public class CategoryConfiguration : IEntityTypeConfiguration<Category>
{
    public void Configure(EntityTypeBuilder<Category> builder)
    {
        builder.HasKey(category => category.Id);

        builder.Property(category => category.Name).HasMaxLength(50).IsRequired();
        builder.HasIndex(category => category.Name).IsUnique();

        builder.Property(category => category.NormalizedName).HasMaxLength(50).IsRequired();
        builder.HasIndex(category => category.NormalizedName).IsUnique();
    }
}
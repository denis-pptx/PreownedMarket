using Item.DataAccess.Models.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Item.DataAccess.Data.Configurations;

public class StatusConfiguration : IEntityTypeConfiguration<Status>
{
    public void Configure(EntityTypeBuilder<Status> builder)
    {
        builder.HasKey(status => status.Id);

        builder.Property(status => status.Name).HasMaxLength(50).IsRequired();
        builder.HasIndex(status => status.Name).IsUnique();

        builder.Property(status => status.NormalizedName).HasMaxLength(50).IsRequired();
        builder.HasIndex(status => status.NormalizedName).IsUnique();
    }
}

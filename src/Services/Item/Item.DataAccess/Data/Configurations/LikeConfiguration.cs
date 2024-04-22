using Item.DataAccess.Models.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Item.DataAccess.Data.Configurations;

public class LikeConfiguration : IEntityTypeConfiguration<Like>
{
    public void Configure(EntityTypeBuilder<Like> builder)
    {
        builder.HasKey(like => like.Id);

        builder.HasIndex(like => new { like.UserId, like.ItemId })
            .IsUnique();

        builder.Property(like => like.CreatedOn)
            .HasDefaultValue(DateTime.UtcNow);

        builder.HasOne(like => like.User)
            .WithMany()
            .HasForeignKey(like => like.UserId)
            .IsRequired();

        builder.HasOne(like => like.Item)
            .WithMany()
            .HasForeignKey(like => like.ItemId)
            .IsRequired();
    }
}
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Item.DataAccess.Data.Configurations;

using Item = Models.Entities.Item;

public class ItemConfiguration : IEntityTypeConfiguration<Item>
{
    public void Configure(EntityTypeBuilder<Item> builder)
    {
        builder.HasKey(item => item.Id);

        builder.Property(item => item.Title)
            .HasMaxLength(100)
            .IsRequired();

        builder.Property(item => item.Description)
        .HasMaxLength(1000);

        builder.Property(item => item.Price) 
            .IsRequired();

        builder.Property(item => item.CreatedAt)
            .HasDefaultValue(DateTime.UtcNow)
            .IsRequired();

        builder.HasOne(item => item.City)
            .WithMany()
            .HasForeignKey(item => item.CityId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(item => item.Category)
            .WithMany()
            .HasForeignKey(item => item.CategoryId)
            .OnDelete(DeleteBehavior.Restrict)
            .IsRequired();

        builder.HasOne(item => item.Status)
            .WithMany()
            .HasForeignKey(item => item.StatusId)
            .OnDelete(DeleteBehavior.Restrict)
            .IsRequired();

        builder.HasOne(item => item.User)
            .WithMany(user => user.Items)
            .HasForeignKey(item => item.UserId)
            .OnDelete(DeleteBehavior.Cascade)
            .IsRequired();
    }
}
using Item.DataAccess.Data.Initializers;
using Item.DataAccess.Models;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace Item.DataAccess.Data;

public class ApplicationDbContext : DbContext
{
    public DbSet<Category> Categories { get; set; }
    public DbSet<Category> Cities { get; set; }
    public DbSet<Category> Items { get; set; }
    public DbSet<Category> Photos { get; set; }
    public DbSet<Category> Regions { get; set; }
    public DbSet<Category> Statuses { get; set; }
    public DbSet<Category> Users { get; set; }

    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
        //Database.EnsureCreated();
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        modelBuilder.Seed();
    }
}

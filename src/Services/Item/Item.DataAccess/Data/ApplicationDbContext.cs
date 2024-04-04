using Item.DataAccess.Data.Initializers;
using Item.DataAccess.Models;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace Item.DataAccess.Data;

public class ApplicationDbContext : DbContext
{
    public DbSet<Region> Categories { get; set; }
    public DbSet<Region> Cities { get; set; }
    public DbSet<Region> Items { get; set; }
    public DbSet<Region> Photos { get; set; }
    public DbSet<Region> Regions { get; set; }
    public DbSet<Region> Statuses { get; set; }
    public DbSet<Region> Users { get; set; }

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

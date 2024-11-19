using Base.EntityFramework.Entities.Identity;
using Base.EntityFramework.Seeding.IdentitySeeds;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using System.Reflection;

namespace Base.EntityFramework.Configs;
public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {

        // Apply Configurations
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        modelBuilder.ApplyConfigurationsFromAssembly(GetType().Assembly);

        modelBuilder.Entity<UserRole>().HasData(UserRoleSeed.All);
        modelBuilder.Entity<Role>().HasData(RoleSeed.All);
        modelBuilder.Entity<User>().HasData(UserSeed.All);
        
        // Creating Model
        base.OnModelCreating(modelBuilder);
    }
}

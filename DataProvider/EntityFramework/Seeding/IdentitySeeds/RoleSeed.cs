using DataProvider.EntityFramework.Entities.Identity;

namespace DataProvider.EntityFramework.Seeding.IdentitySeeds;

public static class RoleSeed
{
    public static List<Role> All => new List<Role>
    {
        new Role()
        {
            Id = 1,
            Title = "Owner",
            CreatedAt = DateTime.Now,
            UpdatedAt = DateTime.Now,
        },
         new Role()
        {
            Id = 2,
            Title = "Admin",
            CreatedAt = DateTime.Now,
            UpdatedAt = DateTime.Now,
        },
         new Role()
        {
            Id = 3,
            Title = "Writer",
            CreatedAt = DateTime.Now,
            UpdatedAt = DateTime.Now,
        },
         new Role()
        {
            Id = 4,
            Title = "Reader",
            CreatedAt = DateTime.Now,
            UpdatedAt = DateTime.Now,
        }
    };
}
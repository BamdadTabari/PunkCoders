﻿using Base.EntityFramework.Entities.Identity;

namespace Base.EntityFramework.Seeding.IdentitySeeds;

public static class UserRoleSeed
{
    public static List<UserRole> All => new List<UserRole>
    {
        new UserRole()
        {
            RoleId = 1,
            UserId = 1,
            CreatedAt = DateTime.Now,
            UpdatedAt = DateTime.Now
        }
    };
}
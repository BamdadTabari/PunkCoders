using Base.Assistant.Helpers;
using Base.Certain.Constants;
using Base.Certain.Enums;
using Base.EntityFramework.Entities.Identity;

namespace Base.EntityFramework.Seeding.IdentitySeeds;

public static class UserSeed
{
    public static List<User> All => new List<User>()
    {
        new User()
        {
            Id = 1,
            IsMobileConfirmed = false,
            Email = "bamdadtabari@outlook.com",
            Mobile = "09301724389",
            State = UserStateEnum.Active,
            Username = "Illegible_Owner",
            PasswordHash = PasswordHasher.Hash("owner"),
            ConcurrencyStamp = StampGenerator.CreateSecurityStamp(Defaults.SecurityStampLength),
            SecurityStamp = StampGenerator.CreateSecurityStamp(Defaults.SecurityStampLength),
            LastPasswordChangeTime = DateTime.Now,
            CreatedAt = DateTime.Now,
            UpdatedAt = DateTime.Now
        }
    };
}
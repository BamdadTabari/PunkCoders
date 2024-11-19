using Base.EntityFramework.Configs;
using Base.EntityFramework.Entities.Identity;
using Base.EntityFramework.Repository;

namespace Base.EntityFramework.Services.Identity;
internal class UserRoleRepo
{
}

public interface IUserRoleRepo : IRepository<UserRole>
{
}
public class UseRoleRepo(AppDbContext context) : Repository<UserRole>(context), IUserRoleRepo
{
}

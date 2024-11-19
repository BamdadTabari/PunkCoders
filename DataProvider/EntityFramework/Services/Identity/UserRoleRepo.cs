using DataProvider.EntityFramework.Configs;
using DataProvider.EntityFramework.Entities.Identity;
using DataProvider.EntityFramework.Repository;

namespace DataProvider.EntityFramework.Services.Identity;
internal class UserRoleRepo
{
}

public interface IUserRoleRepo : IRepository<UserRole>
{
}
public class UseRoleRepo(AppDbContext context) : Repository<UserRole>(context), IUserRoleRepo
{
}

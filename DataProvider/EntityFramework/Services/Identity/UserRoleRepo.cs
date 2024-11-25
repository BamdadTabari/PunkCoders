using DataProvider.EntityFramework.Configs;
using DataProvider.EntityFramework.Entities.Identity;
using DataProvider.EntityFramework.Repository;

namespace DataProvider.EntityFramework.Services.Identity;

public interface IUserRoleRepo : IRepository<UserRole>
{
}
public class UserRoleRepo(AppDbContext context) : Repository<UserRole>(context), IUserRoleRepo
{
}

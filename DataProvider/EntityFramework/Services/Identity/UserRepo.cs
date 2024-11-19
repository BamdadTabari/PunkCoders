using DataProvider.EntityFramework.Configs;
using DataProvider.EntityFramework.Entities.Identity;
using DataProvider.EntityFramework.Repository;

namespace DataProvider.EntityFramework.Services.Identity;
public interface IUserRepo : IRepository<User>
{
}
public class UseRepo(AppDbContext context) : Repository<User>(context), IUserRepo
{
}

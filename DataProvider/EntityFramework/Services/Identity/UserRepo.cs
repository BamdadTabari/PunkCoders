using DataProvider.EntityFramework.Configs;
using DataProvider.EntityFramework.Entities.Identity;
using DataProvider.EntityFramework.Repository;

namespace DataProvider.EntityFramework.Services.Identity;
public interface IUserRepo : IRepository<User>
{
    Task<User> GetUser(string username);
    Task<bool> AnyExist(string username);
}
public class UserRepo(AppDbContext context) : Repository<User>(context), IUserRepo
{
    public Task<bool> AnyExist(string username)
    {
        throw new NotImplementedException();
    }

    public Task<User> GetUser(string username)
    {
        return 
    }
}

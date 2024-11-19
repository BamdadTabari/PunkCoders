using Base.EntityFramework.Configs;
using Base.EntityFramework.Entities.Identity;
using Base.EntityFramework.Repository;

namespace Base.EntityFramework.Services.Identity;
public interface IUserRepo : IRepository<User>
{
}
public class UseRepo(AppDbContext context) : Repository<User>(context), IUserRepo
{
}

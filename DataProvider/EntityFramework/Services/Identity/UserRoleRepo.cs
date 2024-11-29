using DataProvider.EntityFramework.Configs;
using DataProvider.EntityFramework.Entities.Identity;
using DataProvider.EntityFramework.Repository;
using Microsoft.EntityFrameworkCore;
using Serilog;

namespace DataProvider.EntityFramework.Services.Identity;

public interface IUserRoleRepo : IRepository<UserRole>
{
    IEnumerable<UserRole> GetUserRolesByUserId(int userId);
    //Task<PaginatedList<UserRole>> GetByRoleId(int roleId);
}
public class UserRoleRepo : Repository<UserRole>, IUserRoleRepo
{
    private readonly IQueryable<UserRole> _queryable;

    private readonly ILogger _logger;

    public UserRoleRepo(AppDbContext context, ILogger logger) : base(context)
    {
        _queryable = DbContext.Set<UserRole>();
        _logger = logger;
    }

    public IEnumerable<UserRole> GetUserRolesByUserId(int userId)
    {
        try
        {
            return _queryable.Include(i => i.Role).Where(x => x.UserId == userId);
        }
        catch (Exception ex)
        {
            _logger.Error("Error in Get UserRole By UserId", ex);
            return new List<UserRole>().AsEnumerable();
        }
    }
}

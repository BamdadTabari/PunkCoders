using DataProvider.Assistant.Pagination;
using DataProvider.EntityFramework.Configs;
using DataProvider.EntityFramework.Entities.Identity;
using DataProvider.EntityFramework.Extensions.Identity;
using DataProvider.EntityFramework.Repository;
using DataProvider.Models.Query.Identity;
using Microsoft.EntityFrameworkCore;
using Serilog;

namespace DataProvider.EntityFramework.Services.Identity;
public interface IRoleRepo : IRepository<Role>
{
    Task<Role> GetRole(string title);
    PaginatedList<Role> GetPaginatedRoles(GetPagedRoleQuery filter);
    Task<Role> GetRole(int id);
    Task<bool> AnyExist(string title);
}
public class RoleRepo : Repository<Role>, IRoleRepo
{
    private readonly IQueryable<Role> _queryable;

    private readonly ILogger _logger;

    public RoleRepo(AppDbContext context, ILogger logger) : base(context)
    {
        _queryable = DbContext.Set<Role>();
        _logger = logger;
    }

    public async Task<bool> AnyExist(string title)
    {
        try
        {
            return await _queryable.AnyAsync(x => x.Title == title);
        }
        catch (Exception ex)
        {
            _logger.Error("Error in User AnyAsync", ex);
            return await Task.FromResult(false);
        }
    }

    public PaginatedList<Role> GetPaginatedRoles(GetPagedRoleQuery filter)
    {
        try
        {
            var query = _queryable.Include(i => i.UserRoles).ThenInclude(x=>x.User).AsNoTracking().ApplyFilter(filter).ApplySort(filter.SortBy);
            var dataTotalCount = _queryable.Count();
            return new PaginatedList<Role>([.. query], dataTotalCount, filter.Page, filter.PageSize);
        }
        catch (Exception ex)
        {
            _logger.Error("Error in Get Paginated Roles", ex);
            return new PaginatedList<Role>([], 0, filter.Page, filter.PageSize);
        }
    }

    public async Task<Role> GetRole(string title)
    {
        try
        {
            return await _queryable.SingleOrDefaultAsync(x => x.Title == title) ?? new Role();
        }
        catch (Exception ex)
        {
            _logger.Error("Error in Get Role By title", ex);
            return new Role();
        }
    }
    public async Task<Role> GetRole(int id)
    {
        try
        {
            return await _queryable.SingleOrDefaultAsync(x => x.Id == id) ?? new Role();
        }
        catch (Exception ex)
        {
            _logger.Error("Error in Get Role By Name", ex);
            return new Role();
        }
    }
}

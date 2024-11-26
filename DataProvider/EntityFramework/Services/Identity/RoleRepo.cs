using DataProvider.EntityFramework.Configs;
using DataProvider.EntityFramework.Entities.Identity;
using DataProvider.EntityFramework.Repository;
using Microsoft.EntityFrameworkCore;
using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataProvider.EntityFramework.Services.Identity;
public interface IRoleRepo: IRepository<Role>
{
    Task<Role> GetRoleByName(string title);
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

    public async Task<Role> GetRoleByName(string title)
    {
        try
        {
            return await _queryable.SingleOrDefaultAsync(x => x.Title == title) ?? new Role();
        }
        catch (Exception ex)
        {
            _logger.Error("Error in Get Role By Name", ex);
            return new Role();
        }
    }
}

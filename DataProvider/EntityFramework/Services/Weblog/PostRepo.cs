﻿using DataProvider.Assistant.Pagination;
using DataProvider.EntityFramework.Configs;
using DataProvider.EntityFramework.Entities.Blog;
using DataProvider.EntityFramework.Extensions.Blog;
using DataProvider.EntityFramework.Repository;
using Microsoft.EntityFrameworkCore;
using Serilog;

namespace DataProvider.EntityFramework.Services.Weblog;
public interface IPostRepo : IRepository<Post>
{
    /// <summary>
    /// id : category id to get posts
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    Task<List<Post>> GetAllCategoryPostsAsync(int id);
    Task<bool> AnyAsync(string name);
    Task<Post> GetByIdAsync(int id);
    PaginatedList<Post> GetPaginated(DefaultPaginationFilter filter);
    Task<List<Post>> GetAll();
}

public class PostRepo : Repository<Post>, IPostRepo
{
    private readonly IQueryable<Post> _queryable;

    private readonly ILogger _logger;

    public PostRepo(AppDbContext context, ILogger logger) : base(context)
    {
        _queryable = DbContext.Set<Post>();
        _logger = logger;
    }

    public async Task<List<Post>> GetAllCategoryPostsAsync(int id)
    {
        return await _queryable.Include(x => x.PostComments).Where(x => x.Id == id && x.IsDeleted == false).ToListAsync();
    }

    public Task<bool> AnyAsync(string name)
    {
        try
        {
            return _queryable.AnyAsync(x => x.Title.Replace(" ", "").ToLower() == name.Replace(" ", "").ToLower());

        }
        catch
        {

            _logger.Error("Error in Post AnyAsync");
            return Task.FromResult(false);
        }
    }

    public async Task<List<Post>> GetAll()
    {
        try
        {
            return await _queryable.ToListAsync();
        }
        catch
        {

            _logger.Error("Error in GetAll");
            return [];
        }
    }

    public async Task<Post> GetByIdAsync(int id)
    {
        try
        {
            return await _queryable.Include(i => i.PostComments).SingleOrDefaultAsync(x => x.Id == id && x.IsDeleted == false) ?? new Post();
        }
        catch
        {
            _logger.Error("Error in GetByPostIdAsync");
            return new Post();
        }
    }

    public PaginatedList<Post> GetPaginated(DefaultPaginationFilter filter)
    {

        try
        {
            var query = _queryable.Include(x => x.PostComments).AsNoTracking().ApplyFilter(filter).ApplySort(filter.SortBy);
            var dataTotalCount = _queryable.Count();
            return new PaginatedList<Post>([.. query], dataTotalCount, filter.Page, filter.PageSize);
        }
        catch
        {
            _logger.Error("Error in GetPaginatedPost");
            return new PaginatedList<Post>([], 0, filter.Page, filter.PageSize);
        }
    }
}

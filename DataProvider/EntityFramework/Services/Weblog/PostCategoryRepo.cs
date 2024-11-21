using DataProvider.Assistant.Pagination;
using DataProvider.EntityFramework.Configs;
using DataProvider.EntityFramework.Entities.Blog;
using DataProvider.EntityFramework.Extensions.Blog;
using DataProvider.EntityFramework.Repository;
using Microsoft.EntityFrameworkCore;
using Serilog;

namespace DataProvider.EntityFramework.Services.Weblog;
public interface IPostCategoryRepo : IRepository<PostCategory>
{
    Task<bool> AnyAsync(string name);
    Task<PostCategory?> GetByPostCategoryIdAsync(int id);
    PaginatedList<PostCategory> GetPaginatedPostCategory(DefaultPaginationFilter filter);
    Task<List<PostCategory>> GetAll();
}

public class PostCategoryRepo : Repository<PostCategory>, IPostCategoryRepo
{
    private readonly IQueryable<PostCategory> _queryable;

    private readonly ILogger _logger;

    public PostCategoryRepo(AppDbContext context, ILogger logger) : base(context)
    {
        _queryable = DbContext.Set<PostCategory>();
        _logger = logger;
    }

    public Task<bool> AnyAsync(string name)
    {
        try
        {
            _logger.Error("Error in AnyAsync");
            return _queryable.AnyAsync(x => x.Name.Replace(" ", "").ToLower() == name.Replace(" ", "").ToLower());


        }
        catch
        {

            _logger.Error("Error in GetAll");
            return Task.FromResult(false);
        }
    }

    public async Task<List<PostCategory>> GetAll()
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

    public async Task<PostCategory> GetByPostCategoryIdAsync(int id)
    {
        try
        {
            return await _queryable.Include(i => i.Posts).ThenInclude(i => i.PostComments).SingleOrDefaultAsync(x => x.Id == id && x.IsDeleted == false) ?? new PostCategory();
        }
        catch
        {
            _logger.Error("Error in GetByPostCategoryIdAsync");
            return new PostCategory();
        }
    }

    public PaginatedList<PostCategory> GetPaginatedPostCategory(DefaultPaginationFilter filter)
    {

        try
        {
            var query = _queryable.Include(x => x.Posts).ThenInclude(x => x.PostComments).AsNoTracking().ApplyFilter(filter).ApplySort(filter.SortBy);
            var dataTotalCount = _queryable.Count();
            return new PaginatedList<PostCategory>([.. query], dataTotalCount, filter.Page, filter.PageSize);
        }
        catch
        {
            _logger.Error("Error in GetPaginatedPostCategory");
            return new PaginatedList<PostCategory>(new List<PostCategory>(), 0, filter.Page, filter.PageSize);
        }
    }
}
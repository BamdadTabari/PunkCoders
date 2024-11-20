using DataProvider.Assistant.Pagination;
using DataProvider.EntityFramework.Configs;
using DataProvider.EntityFramework.Entities.Blog;
using DataProvider.EntityFramework.Repository;
using Microsoft.EntityFrameworkCore;

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

    public PostCategoryRepo(AppDbContext context) : base(context)
    {
        _queryable = DbContext.Set<PostCategory>();
    }

    public Task<bool> AnyAsync(string name)
    {
        return _queryable.AnyAsync(x => x.Name.Replace(" ", "").ToLower() == name.Replace(" ", "").ToLower());
    }

    public async Task<List<PostCategory>> GetAll()
    {
        return await _queryable.ToListAsync();
    }

    public async Task<PostCategory> GetByPostCategoryIdAsync(int id)
    {
        return await _queryable.Include(i => i.Posts).ThenInclude(i=>i.PostComments).SingleOrDefaultAsync(x => x.Id == id && x.IsDeleted == false) ?? new PostCategory();
    }

    public PaginatedList<PostCategory> GetPaginatedPostCategory(DefaultPaginationFilter filter)
    {
        var query = _queryable;
        // Apply Keyword filtering
        if (!string.IsNullOrEmpty(filter.Keyword))
            query = query.Where(x => x.Name.Contains(filter.Keyword));

        if (filter.IsDeleted.HasValue)
            query = query.Where(x => x.IsDeleted == filter.IsDeleted);
        
        // Get total count before applying pagination
        var count = query.Count();
        var items = new List<PostCategory>();
        // Apply Sorting and Apply pagination
        switch (filter.SortBy)
        {
            case SortByEnum.CreationDate:
                items = query
                    .Include(i=>i.Posts)
                    .ThenInclude(i=>i.PostComments)
                    .OrderBy(x => x.CreatedAt)
                    .Skip((filter.Page - 1) * filter.PageSize)
                    .Take(filter.PageSize)
                    .ToList();
                break;
            case SortByEnum.CreationDateDescending:
                items = query
                   .Include(i => i.Posts)
                   .ThenInclude(i => i.PostComments)
                   .OrderByDescending(x => x.CreatedAt)
                   .Skip((filter.Page - 1) * filter.PageSize)
                   .Take(filter.PageSize)
                   .ToList();
                break;
        }

        return new PaginatedList<PostCategory>(items, count, filter.Page, filter.PageSize);
    }
}
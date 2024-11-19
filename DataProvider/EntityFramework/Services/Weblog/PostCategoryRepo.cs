using Base.Assistant.Pagination;
using Base.EntityFramework.Configs;
using Base.EntityFramework.Entities.Blog;
using Base.EntityFramework.Repository;
using Microsoft.EntityFrameworkCore;

namespace Base.EntityFramework.Services.Weblog;
public interface IPostCategoryRepo : IRepository<PostCategory>
{
    Task<bool> AnyAsync(string name);
    Task<PostCategory?> GetByPostCategoryIdAsync(int id);
    PaginatedList<PostCategory> GetPaginatedPostCategory(string phone, DefaultPaginationFilter filter);
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
        return _queryable.AnyAsync(x => x.Name.Replace(" ", "").Equals(name.Replace(" ", ""), StringComparison.CurrentCultureIgnoreCase));
    }

    public async Task<PostCategory?> GetByPostCategoryIdAsync(int id)
    {
        return await _queryable.SingleOrDefaultAsync(x => x.Id == id);
    }

    public PaginatedList<PostCategory> GetPaginatedPostCategory(string phone, DefaultPaginationFilter filter)
    {
        var query = _queryable;
        // Apply Keyword filtering
        if (!string.IsNullOrEmpty(filter.Keyword))
        {
            query = query.Where(x => x.Name.Contains(filter.Keyword));
        }
        // Get total count before applying pagination
        var count = query.Count();
        var items = new List<PostCategory>();
        // Apply Sorting and Apply pagination
        switch (filter.SortBy)
        {
            case SortByEnum.CreationDate:
                items = query
                    .OrderBy(x => x.CreatedAt)
                    .Skip((filter.Page - 1) * filter.PageSize)
                    .Take(filter.PageSize)
                    .ToList();
                break;
            case SortByEnum.CreationDateDescending:
                items = query
                   .OrderByDescending(x => x.CreatedAt)
                   .Skip((filter.Page - 1) * filter.PageSize)
                   .Take(filter.PageSize)
                   .ToList();
                break;
        }

        return new PaginatedList<PostCategory>(items, count, filter.Page, filter.PageSize);
    }
}
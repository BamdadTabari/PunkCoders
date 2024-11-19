using DataProvider.EntityFramework.Configs;
using DataProvider.EntityFramework.Entities.Blog;
using DataProvider.EntityFramework.Repository;
using Microsoft.EntityFrameworkCore;

namespace DataProvider.EntityFramework.Services.Weblog;
public interface IPostRepo : IRepository<Post>
{
    Task<List<Post>> GetAllCategoryPostsAsync(int categoryId);
}

public class PostRepo : Repository<Post>, IPostRepo
{
    private readonly IQueryable<Post> _queryable;

    public PostRepo(AppDbContext context) : base(context)
    {
        _queryable = DbContext.Set<Post>();
    }
    public async Task<List<Post>> GetAllCategoryPostsAsync(int categoryId)
    {
        return await _queryable.Include(x=>x.PostComments).Where(x=>x.PostCategoryId == categoryId && x.IsDeleted == false).ToListAsync();
    }
}
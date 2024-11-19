using DataProvider.EntityFramework.Configs;
using DataProvider.EntityFramework.Entities.Blog;
using DataProvider.EntityFramework.Repository;

namespace DataProvider.EntityFramework.Services.Weblog;
public interface IPostRepo : IRepository<Post>
{
}

public class PostRepo(AppDbContext context) : Repository<Post>(context), IPostRepo
{
}
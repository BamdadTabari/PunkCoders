using Base.EntityFramework.Configs;
using Base.EntityFramework.Entities.Blog;
using Base.EntityFramework.Repository;

namespace Base.EntityFramework.Services.Weblog;
public interface IPostRepo : IRepository<Post>
{
}

public class PostRepo(AppDbContext context) : Repository<Post>(context), IPostRepo
{
}
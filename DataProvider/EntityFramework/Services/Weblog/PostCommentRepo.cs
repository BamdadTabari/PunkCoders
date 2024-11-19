using DataProvider.EntityFramework.Configs;
using DataProvider.EntityFramework.Entities.Blog;
using DataProvider.EntityFramework.Repository;

namespace DataProvider.EntityFramework.Services.Weblog;
public interface IPostCommentRepo : IRepository<PostComment>
{
}

public class PostCommentRepo(AppDbContext context) : Repository<PostComment>(context), IPostCommentRepo
{

}
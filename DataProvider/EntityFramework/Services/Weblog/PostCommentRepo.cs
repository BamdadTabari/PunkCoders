using Base.EntityFramework.Configs;
using Base.EntityFramework.Entities.Blog;
using Base.EntityFramework.Repository;

namespace Base.EntityFramework.Services.Weblog;
public interface IPostCommentRepo : IRepository<PostComment>
{
}

public class PostCommentRepo(AppDbContext context) : Repository<PostComment>(context), IPostCommentRepo
{

}
using DataProvider.EntityFramework.Entities.Blog;

namespace DataProvider.Models.Result.Blog;
public class GetCommentsResult
{
    public List<PostComment> PostComments { get; set; }
    public bool HasMorePosts { get; set; }
    public int PostCountToShow { get; set; }
}

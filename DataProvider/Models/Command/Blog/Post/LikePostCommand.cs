namespace DataProvider.Models.Command.Blog.Post;
public class LikePostCommand
{
    public int PostId { get; set; }
    public bool IsLike { get; set; }
}

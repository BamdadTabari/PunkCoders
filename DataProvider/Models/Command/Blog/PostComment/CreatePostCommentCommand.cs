namespace DataProvider.Models.Command.Blog.PostComment;
public class CreatePostCommentCommand
{
    public int PostId { get; set; }
    public string CommentText { get; set; }
}

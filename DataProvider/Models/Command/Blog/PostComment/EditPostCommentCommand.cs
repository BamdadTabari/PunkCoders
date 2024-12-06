namespace DataProvider.Models.Command.Blog.PostComment;
public class EditPostCommentCommand
{
    public int PostCommentId { get; set; }
    public string CommentText { get; set; }
}

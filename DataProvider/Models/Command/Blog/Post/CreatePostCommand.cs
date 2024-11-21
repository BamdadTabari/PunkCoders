namespace DataProvider.Models.Command.Blog.Post;
public class CreatePostCommand
{
    public string Title { get; set; }
    public string ShortDescription { get; set; }
    public string Content { get; set; }
    public bool IsPublished { get; set; }

    // navigation
    public int PostCategoryId { get; set; }
}

namespace DataProvider.Models.Command.Blog.Post;
public class EditPostCommand
{
    public string Title { get; set; }
    public string ShortDescription { get; set; }
    public string Content { get; set; }
    public string Image { get; set; }
    public bool IsPublished { get; set; }

    // navigation
    public int AuthorId { get; set; }
    public int PostCategoryId { get; set; }

}
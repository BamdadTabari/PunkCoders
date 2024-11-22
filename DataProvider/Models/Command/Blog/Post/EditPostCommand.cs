using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace DataProvider.Models.Command.Blog.Post;
public class EditPostCommand
{
    [DisplayName("Title")]
    [Required(ErrorMessage = "{0} is required")]
    [MaxLength(150, ErrorMessage = "{0} cannot exceed {1} characters")]
    [MinLength(10, ErrorMessage = "{0} must be at least {1} characters")]
    public string Title { get; set; }
    [DisplayName("ShortDescription")]
    [Required(ErrorMessage = "{0} is required")]
    [MaxLength(300, ErrorMessage = "{0} cannot exceed {1} characters")]
    [MinLength(20, ErrorMessage = "{0} must be at least {1} characters")]
    public string ShortDescription { get; set; }
    [DisplayName("Content")]
    [Required(ErrorMessage = "{0} is required")]
    public string Content { get; set; }
    [DisplayName("IsPublished")]
    [Required(ErrorMessage = "{0} is required")]
    public bool IsPublished { get; set; }

    // navigation
    [DisplayName("PostCategoryId")]
    [Required(ErrorMessage = "{0} is required")]
    public int PostCategoryId { get; set; }

    [DisplayName("Image")]
    public string? Image { get; set; }

}
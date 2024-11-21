using System.ComponentModel.DataAnnotations;

namespace DataProvider.Models.Command.Blog.PostCategory;
public class EditPostCategoryCommand
{
    [Required(ErrorMessage = "Name is required")]
    public string Name { get; set; }
    [Required(ErrorMessage = " Id is Required")]
    public int PostCategoryId { get; set; }
}

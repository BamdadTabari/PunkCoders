using System.ComponentModel.DataAnnotations;

namespace Base.Models.Command.Blog;

public class CreatePostCategoryCommand
{
    [Required(ErrorMessage = "Name is required")]
    public string Name { get; set; }

}

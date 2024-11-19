using System.ComponentModel.DataAnnotations;

namespace DataProvider.Models.Command.Blog;

public class CreatePostCategoryCommand
{
    [Required(ErrorMessage = "Name is required")]
    public string Name { get; set; }

}

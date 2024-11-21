using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace DataProvider.Models.Command.Blog.PostCategory;

public class CreatePostCategoryCommand
{
    [DisplayName("Name")]
    [Required(ErrorMessage = "{0} is required")]
    public string Name { get; set; }

}

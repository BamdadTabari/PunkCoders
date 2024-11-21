using System.ComponentModel.DataAnnotations;

namespace DataProvider.Models.Command.Blog.PostCategory;
public class DeletePostCategoryCommand
{
    [Required(ErrorMessage = " Id is Required")]
    public int PostCategoryId { get; set; }

}

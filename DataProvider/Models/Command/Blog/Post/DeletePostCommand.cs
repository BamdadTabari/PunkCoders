using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace DataProvider.Models.Command.Blog.Post;
public class DeletePostCommand
{
    [DisplayName("PostId")]
    [Required(ErrorMessage = "{0} is required")]
    public int PostId { get; set; }
}

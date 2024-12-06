using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace DataProvider.Models.Command.Blog.PostComment;
public class DeletePostCommentCommand
{
    [DisplayName("Pos Comment tId")]
    [Required(ErrorMessage = "{0} is required")]
    public int PostCommentId { get; set; }
}

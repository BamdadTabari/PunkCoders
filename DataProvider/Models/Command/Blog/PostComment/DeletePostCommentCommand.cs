using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataProvider.Models.Command.Blog.PostComment;
public class DeletePostCommentCommand
{
    [DisplayName("Pos Comment tId")]
    [Required(ErrorMessage = "{0} is required")]
    public int PostCommentId { get; set; }
}

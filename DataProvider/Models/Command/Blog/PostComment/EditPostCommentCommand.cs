using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataProvider.Models.Command.Blog.PostComment;
public class EditPostCommentCommand
{
    public int PostCommentId { get; set; }
    public string CommentText { get; set; }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataProvider.Models.Command.Blog.Post;
public class CommentPostCommand
{
    public int PostId { get; set; }
    public string CommentText { get; set; }
}

using DataProvider.EntityFramework.Entities.Blog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataProvider.Models.Result.Blog;
public class GetCommentsResult
{
    public List<PostComment> PostComments { get; set; }
    public bool HasMorePosts { get; set; }
    public int PostCountToShow { get; set; }
}

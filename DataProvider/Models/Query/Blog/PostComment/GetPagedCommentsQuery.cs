using DataProvider.Assistant.Pagination;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataProvider.Models.Query.Blog.PostComment;
public class GetPagedCommentsQuery: DefaultPaginationFilter
{
    public int? PostId { get; set; }
    public string? PostName { get; set; }
    public string? CommentAuthorName { get; set; }
    public string PostAuthorName { get; set; }
}

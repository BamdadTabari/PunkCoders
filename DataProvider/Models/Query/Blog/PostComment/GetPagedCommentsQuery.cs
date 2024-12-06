using DataProvider.Assistant.Pagination;

namespace DataProvider.Models.Query.Blog.PostComment;
public class GetPagedCommentsQuery : DefaultPaginationFilter
{
    public int? PostId { get; set; }
    public string? PostName { get; set; }
    public string? CommentAuthorName { get; set; }
    public string PostAuthorName { get; set; }
}

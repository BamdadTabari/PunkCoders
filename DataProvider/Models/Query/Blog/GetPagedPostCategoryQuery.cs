using DataProvider.Assistant.Pagination;

namespace DataProvider.Models.Query.Blog;
public class GetPagedPostCategoryQuery
{
    public DefaultPaginationFilter DefaultPaginationFilter { get; set; }
}

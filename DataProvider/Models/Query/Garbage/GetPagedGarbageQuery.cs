using DataProvider.Assistant.Enums;
using DataProvider.Assistant.Pagination;

namespace DataProvider.Models.Query.Garbage;
public class GetPagedGarbageQuery: DefaultPaginationFilter
{
    public GarbageItemsEnum GarbageItems { get; set; } = GarbageItemsEnum.Post;
}

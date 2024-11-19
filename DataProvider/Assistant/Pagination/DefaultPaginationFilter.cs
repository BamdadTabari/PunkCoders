namespace DataProvider.Assistant.Pagination
{
    public class DefaultPaginationFilter : PaginationFilter
    {
        public DefaultPaginationFilter(int pageNumber, int pageSize) : base(pageNumber, pageSize) { }
        public DefaultPaginationFilter() { }

        public string? Keyword { get; set; }
        public bool? IsDeleted { get; set; }
        public SortByEnum? SortBy { get; set; }
    }
}

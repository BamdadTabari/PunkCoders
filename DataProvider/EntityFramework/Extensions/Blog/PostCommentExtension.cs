using DataProvider.Assistant.Pagination;
using DataProvider.EntityFramework.Entities.Blog;
using DataProvider.Models.Query.Blog.PostComment;

namespace DataProvider.EntityFramework.Extensions.Blog;
public static class PostCommentExtension
{
    public static IQueryable<PostComment> ApplyFilter(this IQueryable<PostComment> query, GetPagedCommentsQuery filter)
    {

        if (!string.IsNullOrEmpty(filter.Keyword))
            query = query.Where(x => x.Text.ToLower().Contains(filter.Keyword.ToLower().Trim()));
        if (filter.PostId.HasValue)
            query = query.Where(x => x.PostId == filter.PostId.Value);
        if (!string.IsNullOrEmpty(filter.PostName))
            query = query.Where(x => x.Post.Title.ToLower().Contains(filter.PostName.ToLower().Trim()));
        if (!string.IsNullOrEmpty(filter.CommentAuthorName))
            query = query.Where(x => x.AuthorName.ToLower().Contains(filter.CommentAuthorName.ToLower().Trim()));
        if (!string.IsNullOrEmpty(filter.PostAuthorName))
                query = query.Where(x => x.Post.Author.Username.ToLower().Contains(filter.PostAuthorName.ToLower().Trim()));

        if (filter.IsDeleted.HasValue)
            query = query.Where(x => x.IsDeleted == filter.IsDeleted.Value);

        return query;
    }


    public static IQueryable<PostComment> ApplySort(this IQueryable<PostComment> query, SortByEnum? sortBy)
    {
        return sortBy switch
        {
            SortByEnum.CreationDate => query.OrderBy(x => x.CreatedAt),
            SortByEnum.CreationDateDescending => query.OrderByDescending(x => x.CreatedAt),
            SortByEnum.UpdateDate => query.OrderBy(x => x.UpdatedAt),
            SortByEnum.UpdateDateDescending => query.OrderByDescending(x => x.UpdatedAt),
            _ => query.OrderByDescending(x => x.Id)
        };
    }
}

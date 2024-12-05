﻿using DataProvider.Assistant.Pagination;
using DataProvider.EntityFramework.Configs;
using DataProvider.EntityFramework.Entities.Blog;
using DataProvider.EntityFramework.Extensions.Blog;
using DataProvider.EntityFramework.Repository;
using Microsoft.EntityFrameworkCore;
using Serilog;

namespace DataProvider.EntityFramework.Services.Weblog;
public interface IPostCommentRepo : IRepository<PostComment>
{
    Task<PostComment> GetByIdAsync(int id);
    PaginatedList<PostComment> GetPaginated(DefaultPaginationFilter filter);
    Task<List<PostComment>> GetAll();
}

public class PostCommentRepo : Repository<PostComment>, IPostCommentRepo
{
    private readonly IQueryable<PostComment> _queryable;

    private readonly ILogger _logger;

    public PostCommentRepo(AppDbContext context, ILogger logger) : base(context)
    {
        _queryable = DbContext.Set<PostComment>();
        _logger = logger;
    }

    public async Task<List<PostComment>> GetAll()
    {
        try
        {
            return await _queryable.Include(x => x.Post).ToListAsync();
        }
        catch
        {

            _logger.Error("Error in GetAll");
            return [];
        }
    }

    public async Task<PostComment> GetByIdAsync(int id)
    {
        try
        {
            return await _queryable.Include(x => x.Post).SingleOrDefaultAsync(x => x.Id == id && x.IsDeleted == false) ?? new PostComment();
        }
        catch
        {
            _logger.Error("Error in GetByPostCommentIdAsync");
            return new PostComment();
        }
    }

    public PaginatedList<PostComment> GetPaginated(DefaultPaginationFilter filter)
    {

        try
        {
            var query = _queryable.Include(x => x.Post).AsNoTracking().ApplyFilter(filter).ApplySort(filter.SortBy);
            var dataTotalCount = _queryable.Count();
            return new PaginatedList<PostComment>([.. query], dataTotalCount, filter.Page, filter.PageSize);
        }
        catch
        {
            _logger.Error("Error in GetPaginatedPostComment");
            return new PaginatedList<PostComment>(new List<PostComment>(), 0, filter.Page, filter.PageSize);
        }
    }
}
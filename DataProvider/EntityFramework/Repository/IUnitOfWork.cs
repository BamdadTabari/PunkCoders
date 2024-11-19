using DataProvider.EntityFramework.Configs;
using DataProvider.EntityFramework.Services.Weblog;

namespace DataProvider.EntityFramework.Repository;
public interface IUnitOfWork : IDisposable
{
    IPostCategoryRepo PostCategoryRepo { get; }
    IPostRepo PostRepo { get; }
    IPostCommentRepo PostCommentRepo { get; }

    Task<bool> CommitAsync();
}

public class UnitOfWork : IUnitOfWork
{
    private readonly AppDbContext _context;
    #region Blog
    public IPostCategoryRepo PostCategoryRepo { get; }
    public IPostRepo PostRepo { get; }
    public IPostCommentRepo PostCommentRepo { get; }
    #endregion

    public async Task<bool> CommitAsync() => await _context.SaveChangesAsync() > 0;

    // dispose and add to garbage collector
    public void Dispose()
    {
        _context.Dispose();
        GC.SuppressFinalize(this);
    }

    public UnitOfWork(AppDbContext context)
    {
        _context = context;
        #region Blog
        PostCategoryRepo = new PostCategoryRepo(_context);
        PostRepo = new PostRepo(_context);
        PostCommentRepo = new PostCommentRepo(_context);
        #endregion
    }
}

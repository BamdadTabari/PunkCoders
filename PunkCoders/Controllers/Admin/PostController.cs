using DataProvider.Assistant.Pagination;
using DataProvider.EntityFramework.Entities.Blog;
using DataProvider.EntityFramework.Repository;
using DataProvider.Models.Command.Blog.Post;
using DataProvider.Models.Query.Blog.PostCategory;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Options;
using PunkCoders.Configs;

namespace PunkCoders.Controllers.Admin;
[Route("ap")]
[ApiController]
public class PostController : ControllerBase
{
    private readonly IMemoryCache _memoryCache;
    private readonly CacheOptions _cacheOptions;
    private readonly IUnitOfWork _unitOfWork;
    private readonly ILogger _logger;
    private const string CacheKey = "Post";

    public PostController(IMemoryCache memoryCache, IOptions<CacheOptions> cacheOptions, IUnitOfWork unitOfWork, ILogger logger)
    {
        _memoryCache = memoryCache;
        _cacheOptions = cacheOptions.Value;
        _unitOfWork = unitOfWork;
        _logger = logger;
    }
    [HttpPost]
    [Route("create")]
    public async Task<IActionResult> Create([FromForm] CreatePostCommand createPostCommand)
    {
        try
        {
            // if name exist
            if (await _unitOfWork.PostRepo.AnyAsync(createPostCommand.Title))
                return BadRequest("Post is exist");

            var Entity = new Post()
            {
                CreatedAt = DateTime.Now,
                UpdatedAt = DateTime.Now,
                IsDeleted = false,
                Title = createPostCommand.Title,
            };
            await _unitOfWork.PostRepo.AddAsync(Entity);
            if (!await _unitOfWork.CommitAsync())
                return BadRequest("Error occurred while creating the post.");
            // Clear related cache
            CacheManager.ClearKeysByPrefix(_memoryCache, CacheKey);

            return Ok("Post created successfully.");
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error on create post  at {Time}", DateTime.UtcNow);
            return BadRequest("Error On Create Post");
        }
    }
    [HttpPut]
    [Route("edit")]
    public async Task<IActionResult> Edit([FromForm] EditPostCommand editPostCommand)
    {
        try
        {
            // if name exist
            if (await _unitOfWork.PostRepo.AnyAsync(editPostCommand.Title))
                return BadRequest("Post is exist");

            var entity = await _unitOfWork.PostRepo.GetByIdAsync(editPostCommand.PostId);
            entity.UpdatedAt = DateTime.Now;
            entity.Title = editPostCommand.Title;

            _unitOfWork.PostRepo.Update(entity);
            if (!await _unitOfWork.CommitAsync())
                return BadRequest("Error occurred while Updating the post.");
            // Clear related cache
            CacheManager.ClearKeysByPrefix(_memoryCache, CacheKey);

            return Ok("Post updated successfully.");
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error on update post at {Time}", DateTime.UtcNow);
            return BadRequest("Error On Update Post");
        }
    }
    [HttpGet]
    [Route("get-by-id")]
    public async Task<IActionResult> Get([FromQuery] GetPostQuery getPostQuery)
    {
        string cacheKey = $"{CacheKey}_{getPostQuery.PostId}";

        if (!_memoryCache.TryGetValue(cacheKey, out Post? result))
        {
            result = await _unitOfWork.PostRepo.GetByIdAsync(getPostQuery.PostId);

            if (result == null) return NotFound("Post not found.");

            _memoryCache.Set(cacheKey, result, new MemoryCacheEntryOptions
            {
                AbsoluteExpirationRelativeToNow = _cacheOptions.AbsoluteExpiration,
                SlidingExpiration = _cacheOptions.SlidingExpiration
            });

            CacheManager.AddKey(cacheKey);
        }

        return Ok(result);
    }

    [HttpGet]
    [Route("get-by-filter")]
    public IActionResult GetPaginated([FromQuery] GetPagedPostQuery getPagedPostQuery)
    {
        string cacheKey = $"{CacheKey}_Filter_Pagination";

        if (!_memoryCache.TryGetValue(cacheKey, out PaginatedList<Post>? result))
        {
            result = _unitOfWork.PostRepo.GetPaginated(getPagedPostQuery);

            _memoryCache.Set(cacheKey, result, new MemoryCacheEntryOptions
            {
                AbsoluteExpirationRelativeToNow = _cacheOptions.AbsoluteExpiration,
                SlidingExpiration = _cacheOptions.SlidingExpiration
            });

            CacheManager.AddKey(cacheKey);
        }

        return Ok(result);
    }

    [HttpDelete]
    [Route("clear-all-cache")]
    public IActionResult ClearAllCache()
    {
        CacheManager.ClearKeysByPrefix(_memoryCache, CacheKey);
        _logger.LogInformation("All cache for posts has been cleared At {Time}", DateTime.UtcNow);
        return Ok("All cache for posts has been cleared.");
    }


    [HttpDelete]
    [Route("delete")]
    public async Task<IActionResult> Delete([FromForm] DeletePostCommand deletePostCommand)
    {
        try
        {
            var entity = await _unitOfWork.PostRepo.GetByIdAsync(deletePostCommand.PostId);
            if (entity == null) return NotFound("Post category not found.");

            entity.IsDeleted = true;
            _unitOfWork.PostRepo.Update(entity);

            if (entity.PostComments != null)
            {
                foreach (var comment in entity.PostComments)
                {
                    comment.IsDeleted = true;
                    _unitOfWork.PostCommentRepo.Update(comment);
                }
            }
            if (!await _unitOfWork.CommitAsync())
                return BadRequest("Error occurred while deleting the post.");
            // Clear related cache
            CacheManager.ClearKeysByPrefix(_memoryCache, CacheKey);

            return Ok("Post deleted successfully.");
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error on delete post at {Time}", DateTime.UtcNow);
            return BadRequest("Error On Delete Post");
        }
    }
}

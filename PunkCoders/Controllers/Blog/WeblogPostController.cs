using DataProvider.Assistant.Pagination;
using DataProvider.EntityFramework.Entities.Blog;
using DataProvider.EntityFramework.Repository;
using DataProvider.Models.Command.Blog.Post;
using DataProvider.Models.Query.Blog.PostCategory;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Options;
using PunkCoders.Configs;

namespace PunkCoders.Controllers.Blog;
[Route("weblog-post")]
[ApiController]
public class WeblogPostController : ControllerBase
{
    private readonly IMemoryCache _memoryCache;
    private readonly CacheOptions _cacheOptions;
    private readonly IUnitOfWork _unitOfWork;
    private readonly ILogger _logger;
    private const string CacheKey = "weblog";

    public WeblogPostController(IMemoryCache memoryCache, IOptions<CacheOptions> cacheOptions, IUnitOfWork unitOfWork, ILogger logger)
    {
        _memoryCache = memoryCache;
        _cacheOptions = cacheOptions.Value;
        _unitOfWork = unitOfWork;
        _logger = logger;
    }

    [HttpGet]
    [Route("get-post-by-id")]
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

            result.ViewCount += 1;
            _unitOfWork.PostRepo.Update(result);
        }
        result.ViewCount += 1;
        _unitOfWork.PostRepo.Update(result);
        await _unitOfWork.CommitAsync();
        return Ok(result);
    }

    [HttpGet]
    [Route("get-posts-by-filter")]
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

    [HttpPost]
    [Route("like-post")]
    public async Task<IActionResult> LikePost([FromForm] LikePostCommand request)
    {
        string cacheKey = $"{CacheKey}_{request.PostId}";
        if (_memoryCache.TryGetValue(cacheKey,out Post? result))
        {
            if (request.IsLike)
            {
                result.LikeCount += 1;
            }
            else
            {
                if (result.LikeCount > 0)
                    result.LikeCount -= 1;
            }
            _unitOfWork.PostRepo.Update(result);
            await _unitOfWork.CommitAsync();
            return Ok(result);
        }
        result = await _unitOfWork.PostRepo.GetByIdAsync(request.PostId);
        if (request.IsLike)
        {
            result.LikeCount += 1;
        }
        else
        {
            if (result.LikeCount > 0)
                result.LikeCount -= 1;
        }
        _unitOfWork.PostRepo.Update(result);
        await _unitOfWork.CommitAsync();
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

}

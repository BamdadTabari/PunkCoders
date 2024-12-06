using DataProvider.EntityFramework.Entities.Blog;
using DataProvider.EntityFramework.Repository;
using DataProvider.Models.Command.Blog.Post;
using DataProvider.Models.Command.Blog.PostComment;
using DataProvider.Models.Query.Blog.PostComment;
using DataProvider.Models.Result.Blog;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Options;
using PunkCoders.Configs;

namespace PunkCoders.Controllers.Blog;
[Route("weblog-post-comment")]
[ApiController]
public class WeblogPostCommentController : ControllerBase
{
    private readonly IMemoryCache _memoryCache;
    private readonly CacheOptions _cacheOptions;
    private readonly IUnitOfWork _unitOfWork;
    private readonly ILogger _logger;
    private readonly JwtTokenService _tokenService;
    private const string CacheKey = "weblog-post-comment";

    public WeblogPostCommentController(IMemoryCache memoryCache, IOptions<CacheOptions> cacheOptions, IUnitOfWork unitOfWork, ILogger logger, JwtTokenService tokenService)
    {
        _memoryCache = memoryCache;
        _cacheOptions = cacheOptions.Value;
        _unitOfWork = unitOfWork;
        _logger = logger;
        _tokenService = tokenService;
    }
    [HttpPost]
    [Route("create")]
    public async Task<IActionResult> Create([FromForm] CreatePostCommentCommand createPostCommand)
    {
        try
        {
            var Entity = new PostComment()
            {
                CreatedAt = DateTime.Now,
                UpdatedAt = DateTime.Now,
                IsDeleted = false,
                AuthorName = (await _unitOfWork.UserRepo.GetUser(_tokenService.GetUserIdFromClaims(User)))?.Username ?? "Anonymous",
                PostId = createPostCommand.PostId,
                Text = createPostCommand.CommentText
            };

            await _unitOfWork.PostCommentRepo.AddAsync(Entity);
            if (!await _unitOfWork.CommitAsync())
                return BadRequest("Error occurred while creating the post comment.");
            // Clear related cache
            CacheManager.ClearKeysByPrefix(_memoryCache, CacheKey);

            return Ok("Post created successfully.");
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error on create post comment at {Time}", DateTime.UtcNow);
            return BadRequest("Error On Create Post Comment");
        }
    }

    [HttpGet]
    [Route("get")]
    public async Task<IActionResult> Get([FromQuery] GetPostCommentQuery getPostCommentQuery)
    {
        try
        {
            string cacheKey = $"{CacheKey}_{getPostCommentQuery.PostCommentId}";

            if (!_memoryCache.TryGetValue(cacheKey, out PostComment? result))
            {
                result =  await _unitOfWork.PostCommentRepo.GetByIdAsync(getPostCommentQuery.PostCommentId);

                if (result == null) return NotFound("Post Comment not found.");

                _memoryCache.Set(cacheKey, result, new MemoryCacheEntryOptions
                {
                    AbsoluteExpirationRelativeToNow = _cacheOptions.AbsoluteExpiration,
                    SlidingExpiration = _cacheOptions.SlidingExpiration
                });

                CacheManager.AddKey(cacheKey);
            }

            return Ok(result);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error on get post comment at {Time}", DateTime.UtcNow);
            return BadRequest("Error On Get Post Comment");
        }
    }

    [HttpPut]
    [Route("update")]
    public async Task<IActionResult> Update([FromForm] EditPostCommentCommand updatePostCommand)
    {
        try
        {
            var Entity = await _unitOfWork.PostCommentRepo.GetByIdAsync(updatePostCommand.PostCommentId);
            Entity.Text = updatePostCommand.CommentText;
            _unitOfWork.PostCommentRepo.Update(Entity);
            await _unitOfWork.CommitAsync();
            // Clear related cache
            CacheManager.ClearKeysByPrefix(_memoryCache, CacheKey);
            return Ok("Post Comment updated successfully.");
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error on update post comment at {Time}", DateTime.UtcNow);
            return BadRequest("Error On Update Post Comment");
        }
    }

    [HttpGet]
    [Route("get-by-count")]
    public async Task<IActionResult> GetByCount([FromForm] GetPostCommentsByCount request)
    {
        try
        {
            // Get the specified number of posts to display
            var blogPosts = await _unitOfWork.PostCommentRepo.GetByCount(request.PostCommentCount);

            // Determine if more posts are available
            bool hasMorePosts = (await _unitOfWork.PostCommentRepo.GetAll()).Count > request.PostCommentCount;

            // Build the ViewModel
            var result = new GetCommentsResult
            {
                PostComments = blogPosts,
                PostCountToShow = request.PostCommentCount,
                HasMorePosts = hasMorePosts
            };

            return Ok(result);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error on update post comment at {Time}", DateTime.UtcNow);
            return BadRequest("Error On Update Post Comment");
        }
    }


    [HttpDelete]
    [Route("delete")]
    public async Task<IActionResult> Delete([FromForm] DeletePostCommentCommand request)
    {
        try
        {
            var entity = await _unitOfWork.PostCommentRepo.GetByIdAsync(request.PostCommentId);
            if (entity == null) return NotFound("Post comment not found.");

            entity.IsDeleted = true;
            _unitOfWork.PostCommentRepo.Update(entity);

            if (!await _unitOfWork.CommitAsync())
                return BadRequest("Error occurred while deleting the post comment.");
            // Clear related cache
            CacheManager.ClearKeysByPrefix(_memoryCache, CacheKey);

            return Ok("Post comment deleted successfully.");
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error on delete post comment at {Time}", DateTime.UtcNow);
            return BadRequest("Error On Delete Post Comment");
        }
    }
    [HttpDelete]
    [Route("clear-all-cache")]
    public IActionResult ClearAllCache()
    {
        CacheManager.ClearKeysByPrefix(_memoryCache, CacheKey);
        _logger.LogInformation("All cache for post Comments has been cleared At {Time}", DateTime.UtcNow);
        return Ok("All cache for post Comments has been cleared.");
    }
}

using DataProvider.Assistant.Pagination;
using DataProvider.EntityFramework.Entities.Blog;
using DataProvider.EntityFramework.Repository;
using DataProvider.Models.Command.Blog.PostCategory;
using DataProvider.Models.Query.Blog;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Options;
using PunkCoders.Configs;
namespace PunkCoders.Controllers.Admin;
// API Controller
[Route("apc")]
[ApiController]
public class PostCategoryController : ControllerBase
{
    private readonly IMemoryCache _memoryCache;
    private readonly CacheOptions _cacheOptions;
    private readonly IUnitOfWork _unitOfWork;
    private const string CacheKey = "PostCategory";

    public PostCategoryController(IMemoryCache memoryCache, IOptions<CacheOptions> cacheOptions, IUnitOfWork unitOfWork)
    {
        _memoryCache = memoryCache;
        _cacheOptions = cacheOptions.Value;
        _unitOfWork = unitOfWork;
    }

    // Create a new post category
    [HttpPost]
    [Route("create")]
    public async Task<IActionResult> Create([FromForm] CreatePostCategoryCommand createPostCategoryCommand)
    {
        try
        {
            if (await _unitOfWork.PostCategoryRepo.AnyAsync(createPostCategoryCommand.Name))
                return BadRequest("PostCategory already exists");

            var entity = new PostCategory
            {
                CreatedAt = DateTime.Now,
                UpdatedAt = DateTime.Now,
                IsDeleted = false,
                Name = createPostCategoryCommand.Name,
            };

            await _unitOfWork.PostCategoryRepo.AddAsync(entity);
            if (!await _unitOfWork.CommitAsync())
                return BadRequest("Error occurred while creating the category.");

            // Clear related cache
            CacheManager.ClearKeysByPrefix(_memoryCache, CacheKey);

            return Ok("Post category created successfully.");
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    // Edit a post category
    [HttpPut]
    [Route("edit")]
    public async Task<IActionResult> Edit([FromForm] EditPostCategoryCommand editPostCategoryCommand)
    {
        try
        {
            if (await _unitOfWork.PostCategoryRepo.AnyAsync(editPostCategoryCommand.Name))
                return BadRequest("PostCategory already exists");

            var entity = await _unitOfWork.PostCategoryRepo.GetByIdAsync(editPostCategoryCommand.PostCategoryId);
            entity.UpdatedAt = DateTime.Now;
            entity.Name = editPostCategoryCommand.Name;

            _unitOfWork.PostCategoryRepo.Update(entity);
            if (!await _unitOfWork.CommitAsync())
                return BadRequest("Error occurred while updating the category.");

            // Clear related cache
            CacheManager.ClearKeysByPrefix(_memoryCache, CacheKey);

            return Ok("Post category updated successfully.");
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    // Get a specific post category by ID
    [HttpGet]
    [Route("get-by-id")]
    public async Task<IActionResult> Get([FromQuery] GetPostCategoryQuery getPostCategoryQuery)
    {
        string cacheKey = $"{CacheKey}_{getPostCategoryQuery.PostCategoryId}";

        if (!_memoryCache.TryGetValue(cacheKey, out PostCategory? result))
        {
            result = await _unitOfWork.PostCategoryRepo.GetByIdAsync(getPostCategoryQuery.PostCategoryId);

            if (result == null) return NotFound("Post category not found.");

            _memoryCache.Set(cacheKey, result, new MemoryCacheEntryOptions
            {
                AbsoluteExpirationRelativeToNow = _cacheOptions.AbsoluteExpiration,
                SlidingExpiration = _cacheOptions.SlidingExpiration
            });

            CacheManager.AddKey(cacheKey);
        }

        return Ok(result);
    }

    // Get paginated post categories
    [HttpGet]
    [Route("get-by-filter")]
    public IActionResult GetPaginated([FromQuery] GetPagedPostCategoryQuery getPagedPostCategoryQuery)
    {
        string cacheKey = $"{CacheKey}_Filter_Pagination";

        if (!_memoryCache.TryGetValue(cacheKey, out PaginatedList<PostCategory>? result))
        {
            result = _unitOfWork.PostCategoryRepo.GetPaginated(getPagedPostCategoryQuery);

            _memoryCache.Set(cacheKey, result, new MemoryCacheEntryOptions
            {
                AbsoluteExpirationRelativeToNow = _cacheOptions.AbsoluteExpiration,
                SlidingExpiration = _cacheOptions.SlidingExpiration
            });

            CacheManager.AddKey(cacheKey);
        }

        return Ok(result);
    }

    // Clear all cache for post categories
    [HttpDelete]
    [Route("clear-all-cache")]
    public IActionResult ClearAllCache()
    {
        CacheManager.ClearKeysByPrefix(_memoryCache, CacheKey);
        return Ok("All cache for post categories has been cleared.");
    }

    // Delete a post category
    [HttpDelete]
    [Route("delete")]
    public async Task<IActionResult> Delete([FromForm] DeletePostCategoryCommand deletePostCategoryCommand)
    {
        try
        {
            var entity = await _unitOfWork.PostCategoryRepo.GetByIdAsync(deletePostCategoryCommand.PostCategoryId);
            if (entity == null) return BadRequest("Post category not found.");

            entity.IsDeleted = true;
            _unitOfWork.PostCategoryRepo.Update(entity);

            if (!await _unitOfWork.CommitAsync())
                return BadRequest("Error occurred while deleting the category.");

            // Clear related cache
            CacheManager.ClearKeysByPrefix(_memoryCache, CacheKey);

            return Ok("Post category deleted successfully.");
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }
}
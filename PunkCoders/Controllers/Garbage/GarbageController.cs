using DataProvider.Assistant.Enums;
using DataProvider.Assistant.Pagination;
using DataProvider.EntityFramework.Entities.Blog;
using DataProvider.EntityFramework.Repository;
using DataProvider.Models.Query.Blog.PostCategory;
using DataProvider.Models.Query.Blog.PostComment;
using DataProvider.Models.Query.Garbage;
using DataProvider.Models.Query.Identity;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Options;
using PunkCoders.Configs;

namespace PunkCoders.Controllers.Garbage;
[Route("garbage")]
[ApiController]
public class GarbageController : ControllerBase
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly ILogger _logger;

    public GarbageController(IUnitOfWork unitOfWork, ILogger logger)
    {
        _unitOfWork = unitOfWork;
        _logger = logger;
    }

    //[HttpGet]
    //[Route("get-post-by-id")]
    //public async Task<IActionResult> Get([FromQuery] GetPostQuery getPostQuery)
    //{
    //    string cacheKey = $"{CacheKey}_{getPostQuery.PostId}";

    //    if (!_memoryCache.TryGetValue(cacheKey, out Post? result))
    //    {
    //        result = await _unitOfWork.PostRepo.GetByIdAsync(getPostQuery.PostId);

    //        if (result == null) return NotFound("Post not found.");

    //        _memoryCache.Set(cacheKey, result, new MemoryCacheEntryOptions
    //        {
    //            AbsoluteExpirationRelativeToNow = _cacheOptions.AbsoluteExpiration,
    //            SlidingExpiration = _cacheOptions.SlidingExpiration
    //        });

    //        CacheManager.AddKey(cacheKey);

    //        result.ViewCount += 1;
    //        _unitOfWork.PostRepo.Update(result);
    //    }
    //    //result = await _unitOfWork.PostRepo.GetByIdAsync(getPostQuery.PostId);
    //    result.ViewCount += 1;
    //    _unitOfWork.PostRepo.Update(result);
    //    await _unitOfWork.CommitAsync();
    //    return Ok(result);
    //}

    [HttpGet]
    [Route("get-posts-by-filter")]
    public IActionResult GetPaginated([FromQuery] GetPagedGarbageQuery request)
    {
        
        switch  (request.GarbageItems)
        {
            case GarbageItemsEnum.Post:
                var result1 = _unitOfWork.PostRepo.GetPaginated(new GetPagedPostQuery {
                    IsDeleted = true , SortByEnum = request.SortByEnum,TotalPageCount = request.TotalPageCount,
                    Keyword = request.Keyword, PageSize = request.PageSize , Page= request.Page});
                return Ok(result1);
            case GarbageItemsEnum.PostCategory:
                var result2 = _unitOfWork.PostCategoryRepo.GetPaginated(new GetPagedPostCategoryQuery()
                {
                    IsDeleted = true, SortByEnum = request.SortByEnum, TotalPageCount = request.TotalPageCount,
                    Keyword = request.Keyword, PageSize = request.PageSize, Page = request.Page
                });
                return Ok(result2);
             case GarbageItemsEnum.PostComment:
                var result3 = _unitOfWork.PostCommentRepo.GetPaginated(new GetPagedCommentsQuery()
                {
                    IsDeleted = true, SortByEnum = request.SortByEnum, TotalPageCount = request.TotalPageCount,
                    Keyword = request.Keyword, PageSize = request.PageSize, Page = request.Page
                });
                return Ok(result3);
            case GarbageItemsEnum.User:
                var result4 = _unitOfWork.UserRepo.GetPaginatedUsers(new GetPagedUserQuery()
                {
                    IsDeleted = true, SortByEnum = request.SortByEnum, TotalPageCount = request.TotalPageCount,
                    Keyword = request.Keyword, PageSize = request.PageSize, Page = request.Page
                });
                return Ok(result4);
            case GarbageItemsEnum.Role:
                var result5 = _unitOfWork.RoleRepo.GetPaginated(new GetPagedRoleQuery()
                {
                    IsDeleted = true, SortByEnum = request.SortByEnum, TotalPageCount = request.TotalPageCount,
                    Keyword = request.Keyword, PageSize = request.PageSize, Page = request.Page
                });
                return Ok(result5);
        }
        return BadRequest();
    }

}

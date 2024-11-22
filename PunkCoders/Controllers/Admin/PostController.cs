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
    private const string CacheKey = "Post";

    public PostController(IMemoryCache memoryCache, IOptions<CacheOptions> cacheOptions, IUnitOfWork unitOfWork)
    {
        _memoryCache = memoryCache;
        _cacheOptions = cacheOptions.Value;
        _unitOfWork = unitOfWork;
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
            var result = await _unitOfWork.CommitAsync();
            if (!result)
            {
                return BadRequest("something went wrong");
            }
            return Ok();
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
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
            var result = await _unitOfWork.CommitAsync();
            if (!result)
            {
                return BadRequest("something went wrong");
            }
            return Ok();
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }
    [HttpGet]
    [Route("get-by-id")]
    public async Task<IActionResult> Get([FromQuery] GetPostQuery getPostQuery)
    {
        try
        {
            var result = await _unitOfWork.PostRepo.GetByIdAsync(getPostQuery.PostId);
            return Ok(result);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpGet]
    [Route("get-by-filter")]
    public IActionResult GetPaginated([FromQuery] GetPagedPostQuery getPagedPostQuery)
    {
        try
        {
            return Ok(_unitOfWork.PostRepo.GetPaginated(getPagedPostQuery));
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpGet]
    [Route("get-all")]
    public async Task<IActionResult> GetAll()
    {
        try
        {
            return Ok(await _unitOfWork.PostRepo.GetAll());
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }


    [HttpDelete]
    [Route("delete")]
    public async Task<IActionResult> Delete([FromForm] DeletePostCommand deletePostCommand)
    {
        try
        {
            var entity = await _unitOfWork.PostRepo.GetByIdAsync(deletePostCommand.PostId);
            if (entity == null)
            {
                return BadRequest("not found");
            }
            else
            {
                entity.IsDeleted = true;
                _unitOfWork.PostRepo.Update(entity);
                await _unitOfWork.CommitAsync();
                var children = await _unitOfWork.PostRepo.GetAllCategoryPostsAsync(entity.Id);
                foreach (var child in children)
                {
                    child.IsDeleted = true;
                    _unitOfWork.PostRepo.Update(child);

                    if (child.PostComments != null)
                    {
                        foreach (var comment in child.PostComments)
                        {
                            comment.IsDeleted = true;
                            _unitOfWork.PostCommentRepo.Update(comment);
                        }
                    }
                   
                    await _unitOfWork.CommitAsync();
                }
            }
            return Ok();
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }
}

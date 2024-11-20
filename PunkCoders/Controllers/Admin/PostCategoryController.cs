using DataProvider.EntityFramework.Entities.Blog;
using DataProvider.EntityFramework.Repository;
using DataProvider.Models.Command.Blog;
using DataProvider.Models.Query.Blog;
using Microsoft.AspNetCore.Mvc;
namespace PunkCoders.Controllers.Admin;
[Route("apc")]
[ApiController]
public class PostCategoryController(IUnitOfWork unitOfWork) : ControllerBase
{
    [HttpPost]
    [Route("create")]
    public async Task<IActionResult> Create([FromForm] CreatePostCategoryCommand createPostCategoryCommand)
    {
        try
        {
            // if name exist
            if (await unitOfWork.PostCategoryRepo.AnyAsync(createPostCategoryCommand.Name))
                return BadRequest("PostCategory is exist");

            var Entity = new PostCategory()
            {
                CreatedAt = DateTime.Now,
                UpdatedAt = DateTime.Now,
                IsDeleted = false,
                Name = createPostCategoryCommand.Name,
            };
            await unitOfWork.PostCategoryRepo.AddAsync(Entity);
            var result = await unitOfWork.CommitAsync();
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
    public async Task<IActionResult> Edit([FromForm] EditPostCategoryCommand editPostCategoryCommand)
    {
        try
        {
            // if name exist
            if (await unitOfWork.PostCategoryRepo.AnyAsync(editPostCategoryCommand.Name))
                return BadRequest("PostCategory is exist");

            var Entity = new PostCategory()
            {
                UpdatedAt = DateTime.Now,
                IsDeleted = false,
                Name = editPostCategoryCommand.Name,
            };
            unitOfWork.PostCategoryRepo.Update(Entity);
            var result = await unitOfWork.CommitAsync();
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
    public async Task<IActionResult> Get([FromForm] GetPostCategoryQuery getPostCategoryQuery)
    {
        try
        {
            var result = await unitOfWork.PostCategoryRepo.GetByPostCategoryIdAsync(getPostCategoryQuery.PostCategoryId);
            return Ok(result);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpGet]
    [Route("get-by-filter")]
    public IActionResult GetPaginated([FromForm] GetPagedPostCategoryQuery getPagedPostCategoryQuery)
    {
        try
        {
            return Ok(unitOfWork.PostCategoryRepo.GetPaginatedPostCategory(getPagedPostCategoryQuery.DefaultPaginationFilter));
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpGet]
    [Route("get-all")]
    public IActionResult GetAll()
    {
        try
        {
            return Ok(unitOfWork.PostCategoryRepo.GetAll());
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }


    [HttpDelete]
    [Route("delete")]
    public async IActionResult Delete([FromForm] DeletePostCategoryCommand deletePostCategoryCommand)
    {
        try
        {
            var entity = await unitOfWork.PostCategoryRepo.GetByPostCategoryIdAsync(deletePostCategoryCommand.PostCategoryId);
            if (entity == null)
            {
                return BadRequest("not found");
            }
            else {
                entity.IsDeleted = true;
                unitOfWork.PostCategoryRepo.Update(entity);
                await unitOfWork.CommitAsync();
                var children = await unitOfWork.PostRepo.GetAllCategoryPostsAsync(entity.Id);
                foreach (var child in children) 
                { 
                    child.IsDeleted = true;
                    unitOfWork.PostRepo.Update(child);
                    
                    foreach (var comment in child.PostComments)
                    {
                        comment.IsDeleted = true;
                        unitOfWork.PostCommentRepo.Update(comment);
                    }
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

using DataProvider.EntityFramework.Entities.Blog;
using DataProvider.EntityFramework.Repository;
using DataProvider.Models.Command.Blog;
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
}

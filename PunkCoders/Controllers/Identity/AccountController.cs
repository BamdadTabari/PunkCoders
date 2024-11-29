using DataProvider.Assistant.Helpers;
using DataProvider.EntityFramework.Repository;
using DataProvider.Models.Command.Identity;
using Microsoft.AspNetCore.Identity.Data;
using Microsoft.AspNetCore.Mvc;

namespace PunkCoders.Controllers.Identity;
[ApiController]
[Route("api/[controller]")]
public class AccountController : Controller
{
    private readonly JwtTokenService _tokenService;
    private readonly IUnitOfWork _unitOfWork;

    public AccountController(JwtTokenService tokenService, IUnitOfWork unitOfWork)
    {
        _tokenService = tokenService;
        _unitOfWork = unitOfWork;
    }

    [HttpPost("reset-password")]
    public async Task<IActionResult> ResetPassword([FromForm] LoginCommand request)
    {
        var user = await _unitOfWork.UserRepo.GetUser(request.EmailOrUserName);

        if (user == null)
            return Unauthorized("Invalid credentials");

        if (!PasswordHasher.Check(user.PasswordHash, request.Password))
        {
            user.FailedLoginCount++;
            _unitOfWork.UserRepo.Update(user);
            if (user.FailedLoginCount >= 5)
            {
                user.IsLockedOut = true;
                _unitOfWork.UserRepo.Update(user);
                return Unauthorized("your account is locked out. Use Recovery Option to reset your password.");
            }

            return Unauthorized("Invalid credentials");
        }

        var role = _unitOfWork.UserRoleRepo.GetUserRolesByUserId(user.Id);
        var token = _tokenService.GenerateToken(user, role.Select(x => x.Role.Title).ToList());

        return Ok(new { Token = token });
    }

    [HttpPatch("change-password")]
    public async Task<IActionResult> ChangePassword([FromForm] PasswordResetCommand request)
    {
        var user = await _unitOfWork.UserRepo.GetUser(request.UserId);

        if (user == null)
            return Unauthorized("Invalid credentials");

        if (!PasswordHasher.Check(user.PasswordHash, request.OldPassword))
        {
            return Unauthorized("Invalid credentials");
        }
        user.PasswordHash = PasswordHasher.Hash(request.NewPassword);
        _unitOfWork.UserRepo.Update(user);
        await _unitOfWork.CommitAsync();

        var role = _unitOfWork.UserRoleRepo.GetUserRolesByUserId(user.Id);
        var token = _tokenService.GenerateToken(user, role.Select(x => x.Role.Title).ToList());

        return Ok(new { Token = token });
    }
}

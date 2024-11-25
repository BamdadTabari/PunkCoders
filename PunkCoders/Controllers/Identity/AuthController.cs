using DataProvider.Assistant.Helpers;
using DataProvider.Certain.Enums;
using DataProvider.EntityFramework.Entities.Identity;
using Microsoft.AspNetCore.Identity.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using DataProvider.EntityFramework.Repository;
using DataProvider.Models.Command.Identity;

namespace PunkCoders.Controllers.Identity;
[ApiController]
[Route("api/auth")]
public class AuthController : ControllerBase
{
    private readonly JwtTokenService _tokenService;
    private readonly IUnitOfWork _unitOfWork;

    public AuthController(JwtTokenService tokenService, IUnitOfWork unitOfWork)
    {
        _tokenService = tokenService;
        _unitOfWork = unitOfWork;
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] LoginCommand request)
    {
        var user = _unitOfWork.UserRepo.GetUser();
        
        if ((user = _unitOfWork.UserRepo.GetUser(request.UserName)) ==  || !PasswordHasher.Check(user.PasswordHash, request.Password))
            return Unauthorized("Invalid credentials");

        var roles = await _userManager.GetRolesAsync(user.Id);
        var token = _tokenService.GenerateToken(user, roles);

        return Ok(new { Token = token });
    }

    [HttpPost("register")]
    public async Task<IActionResult> Register([FromBody] RegisterRequest request)
    {
        var user = new User
        {
            Username = request.Username,
            Email = request.Email,
            Mobile = request.Mobile,
            PasswordHash = PasswordHasher.Hash(request.Password),
            SecurityStamp = StampGenerator.CreateSecurityStamp(16),
            State = UserStateEnum.Active
        };

        await _userManager.CreateAsync(user);
        return Ok("User registered successfully.");
    }
}

﻿using DataProvider.Assistant.Helpers;
using DataProvider.Certain.Enums;
using DataProvider.EntityFramework.Entities.Identity;
using Microsoft.AspNetCore.Identity.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using DataProvider.EntityFramework.Repository;
using DataProvider.Models.Command.Identity;

namespace PunkCoders.Controllers.Identity;
[ApiController]
[Route("api/[controller]")]
public class AuthenticationController : ControllerBase
{
    private readonly JwtTokenService _tokenService;
    private readonly IUnitOfWork _unitOfWork;

    public AuthenticationController(JwtTokenService tokenService, IUnitOfWork unitOfWork)
    {
        _tokenService = tokenService;
        _unitOfWork = unitOfWork;
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] LoginCommand request)
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

        var role =  _unitOfWork.UserRoleRepo.GetUserRolesByUserId(user.Id);
        var token = _tokenService.GenerateToken(user, role.Select(x => x.Role.Title).ToList());

        return Ok(new { Token = token });
    }

    [HttpPost("register")]
    public async Task<IActionResult> Register([FromBody] UserForRegistrationCommand request)
    {
        if (await _unitOfWork.UserRepo.AnyExistEmail(request.Email))
            return BadRequest("Email already exists");

        if (await _unitOfWork.UserRepo.AnyExistUserName(request.UserName))
            return BadRequest("Username already exists");

        var user = new User
        {
            Username = request.UserName,
            Email = request.Email,
            Mobile = request.PhoneNumber,
            PasswordHash = PasswordHasher.Hash(request.Password),
            SecurityStamp = StampGenerator.CreateSecurityStamp(16),
            State = UserStateEnum.Active
        };

        await _unitOfWork.UserRepo.AddAsync(user);
        await _unitOfWork.CommitAsync();

        return Ok("User registered successfully.");
    }
}
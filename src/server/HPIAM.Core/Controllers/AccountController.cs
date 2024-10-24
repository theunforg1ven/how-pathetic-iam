﻿using HPIAM.Application.DTOs.Auth;
using HPIAM.Application.DTOs.UserData;
using HPIAM.Application.Interfaces;
using HPIAM.Domain.Entities;
using HPIAM.Infrastructure.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Cryptography;
using System.Text;

namespace HPIAM.Core.Controllers;

public class AccountController : BaseApiController
{
    private readonly DataContext _context;
    private readonly ITokenService _tokenService;

    public AccountController(DataContext context, ITokenService tokenService)
    {
        _context = context;
        _tokenService = tokenService;
    }

    [HttpPost("register")] // api/account/register
    public async Task<ActionResult<UserDto>> Register(RegisterDto registerDto)
    {
        if (await UserExists(registerDto.Username))
            return BadRequest("Such username is already exists!");

        using var hmac = new HMACSHA512();

        var user = new AppUser
        {
            UserName = registerDto.Username.ToLower(),
            PasswordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(registerDto.Password)),
            PasswordSalt = hmac.Key
        };

        _context.Users.Add(user);
        await _context.SaveChangesAsync();

        return new UserDto
        {
            Username = user.UserName,
            AccessToken = _tokenService.CreateToken(user)
        };
    }

    [HttpPost("login")] // api/account/login
    public async Task<ActionResult<UserDto>> Login(LoginDto loginDto)
    {
        var user = await _context.Users
                           .FirstOrDefaultAsync(u => u.UserName == loginDto.Username.ToLower());

        if(user == null)
            return Unauthorized("Invalid username!");

        using var hmac = new HMACSHA512(user.PasswordSalt);
        var computedHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(loginDto.Password));

        for (int i = 0; i < computedHash.Length; i++)
            if (computedHash[i] != user.PasswordHash[i])
                return Unauthorized("Invalid password!");

        return new UserDto
        {
            Username = user.UserName,
            AccessToken = _tokenService.CreateToken(user)
        };

    }

    private async Task<bool> UserExists(string username) 
        => await _context.Users.AnyAsync(u => u.UserName.ToLower() == username.ToLower());
}

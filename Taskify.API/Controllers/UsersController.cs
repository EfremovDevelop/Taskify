﻿using Microsoft.AspNetCore.Mvc;
using Taskify.API.Contracts.Users;
using Taskify.Application.Services;
using Taskify.Infrastructure;

namespace Taskify.API.Controllers;

[ApiController]
[Route("api")]
public class UsersController : ControllerBase
{
    private readonly UsersService _usersService;
    private readonly IHttpContextAccessor _httpContextAccessor;

    public UsersController(UsersService usersService, IHttpContextAccessor httpContextAccessor)
    {
        _usersService = usersService;
        _httpContextAccessor = httpContextAccessor;
    }

    [HttpPost("register")]
    public async Task<IResult> Register([FromBody] UsersRequest usersRequest)
    {
        await _usersService.Register(
            usersRequest.UserName,
            usersRequest.Email,
            usersRequest.Password);
        return Results.Ok();
    }

    [HttpPost("login")]
    public async Task<ActionResult<UsersResponse>> Login(LoginUserRequest request)
    {
        var token = await _usersService.Login(request.Email, request.Password);

        var httpContext = _httpContextAccessor.HttpContext;

        if (httpContext != null)
            httpContext.Response.Cookies.Append("qwerty", token);

        var user = await _usersService.GetUserByEmail(request.Email);

        var response = new UsersResponse(user.Id, user.Name, user.Email);

        return Ok(response);
    }

    [HttpPost("logoff")]
    public IResult Logoff()
    {
        var httpContext = _httpContextAccessor.HttpContext;

        if (httpContext != null)
            httpContext.Response.Cookies.Delete("qwerty");

        return Results.Ok("Logged off successfully");
    }

    [HttpGet("isAuth")]
    public IActionResult CheckAuth()
    {
        if (User.Identity.IsAuthenticated)
        {
            var userId = User.Claims.FirstOrDefault(c => c.Type == CustomClaims.UserId)?.Value;
            var userName = User.Claims.FirstOrDefault(c => c.Type == CustomClaims.UserName)?.Value;
            return Ok(new { userName = userName, userId = userId });
        }
        else
        {
            return Unauthorized("User is not authenticated");
        }
    }
}

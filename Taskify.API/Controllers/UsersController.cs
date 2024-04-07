﻿using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Taskify.API.Contracts.Users;
using Taskify.Application.Services;

namespace Taskify.API.Controllers
{
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
        public async Task<IResult> Login(LoginUserRequest request)
        {
            var token = await _usersService.Login(request.Email, request.Password);

            // Получаем контекст HTTP
            var httpContext = _httpContextAccessor.HttpContext;

            if (httpContext != null)
                httpContext.Response.Cookies.Append("qwerty", token);

            return Results.Ok(token);
        }
    }
}

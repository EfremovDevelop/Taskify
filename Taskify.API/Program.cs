using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using System.Text.Json.Serialization;
using Taskify.API.Extensions;
using Taskify.Application.Services;
using Taskify.Core.Interfaces.Auth;
using Taskify.Core.Interfaces.Repositories;
using Taskify.Core.Interfaces.Services;
using Taskify.DataAccess;
using Taskify.DataAccess.Data;
using Taskify.DataAccess.Repositories;
using Taskify.Infrastructure;

var MyAllowSpecificOrigins = "_myAllowSpecificOrigins";

var builder = WebApplication.CreateBuilder(args);

var config = builder.Configuration;

builder.Services.Configure<JwtOptions>(config.GetSection(nameof(JwtOptions)));
builder.Services.Configure<AuthorizationOptions>(config.GetSection(nameof(AuthorizationOptions)));

//builder.Services.AddCors(options =>
//{
//    options.AddPolicy(name: MyAllowSpecificOrigins,
//                      policy =>
//                      {
//                          policy.WithOrigins("http://localhost:3000")
//                                .AllowAnyMethod()
//                                .AllowAnyHeader()
//                                .AllowCredentials();
//                      });
//});

builder.Services.AddCors(options =>
{
    options.AddPolicy(name: MyAllowSpecificOrigins,
                      policy =>
                      {
                          policy
                            .SetIsOriginAllowed(origin => true) // Разрешить все источники
                            .AllowAnyMethod()
                            .AllowAnyHeader()
                            .AllowCredentials();
                      });
});

// Add services to the container.
builder.Services.AddDbContext<DataContext>(
    options =>
    {
        options.UseNpgsql(config.GetConnectionString("DefaultConnection"));
    });

builder.Services.AddControllers().AddJsonOptions(x =>
                x.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles);
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Services
builder.Services.AddScoped<IProjectsService, ProjectsService>();
builder.Services.AddScoped<IIssuesService, IssuesService>();
builder.Services.AddScoped<UsersService>();
builder.Services.AddScoped<IProjectUsersService, ProjectUsersService>();
builder.Services.AddHttpContextAccessor();

// Auth
builder.Services.AddScoped<IJwtProvider, JwtProvider>();
builder.Services.AddScoped<IPasswordHash, PasswordHash>();

// Repository
builder.Services.AddScoped<IProjectsRepository, ProjectsRepository>();
builder.Services.AddScoped<IIssuesRepository, IssuesRepository>();
builder.Services.AddScoped<IUsersRepository, UsersRepository>();
builder.Services.AddScoped<IProjectUsersRepository, ProjectUsersRepository>();

builder.Services.AddApiAuthentication(builder.Services.BuildServiceProvider().GetRequiredService<IOptions<JwtOptions>>());

var app = builder.Build();

AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);

using (var scope = app.Services.CreateScope())
{
    var dataContext = scope.ServiceProvider.GetRequiredService<DataContext>();
    //await DataContextSeed.SeedAsync(dataContext);
}

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.UseCors(MyAllowSpecificOrigins);

app.MapControllers();

app.Run();

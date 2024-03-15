using System.Text.Json.Serialization;
using Taskify.Application.Services;
using Taskify.Core.Interfaces;
using Taskify.Core.Models;
using Taskify.DataAccess;
using Taskify.DataAccess.Data;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddDbContext<DataContext>();

builder.Services.AddControllers().AddJsonOptions(x =>
                x.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles);
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddScoped<IProjectsService, ProjectsService>();
builder.Services.AddScoped<IRepository<Project>, ProjectsRepository>();

var app = builder.Build();

AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);

using (var scope = app.Services.CreateScope())
{
    var dataContext =
   scope.ServiceProvider.GetRequiredService<DataContext>();
    await DataContextSeed.SeedAsync(dataContext);
}


// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();

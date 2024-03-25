using System.Text.Json.Serialization;
using Taskify.Application.Services;
using Taskify.Core.Interfaces;
using Taskify.Core.Interfaces.Repositories;
using Taskify.Core.Interfaces.Services;
using Taskify.DataAccess;
using Taskify.DataAccess.Data;
using Taskify.DataAccess.Repositories;

var MyAllowSpecificOrigins = "_myAllowSpecificOrigins";

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddCors(options =>
{
    options.AddPolicy(name: MyAllowSpecificOrigins,
                      policy =>
                      {
                          policy.WithOrigins("http://localhost:3000")
                                .AllowAnyMethod()
                                .AllowAnyHeader()
                                .AllowCredentials();
                      });
});

// Add services to the container.
builder.Services.AddDbContext<DataContext>();

builder.Services.AddControllers().AddJsonOptions(x =>
                x.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles);
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddScoped<IProjectsService, ProjectsService>();
builder.Services.AddScoped<IIssuesService, IssuesService>();
builder.Services.AddScoped<IIssueStatusesService, IssueStatusesService>();

builder.Services.AddScoped<IProjectsRepository, ProjectsRepository>();
builder.Services.AddScoped<IIssuesRepository, IssuesRepository>();
builder.Services.AddScoped<IIssueStatusesRepository, IssueStatusesRepository>();

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

app.UseCors(MyAllowSpecificOrigins);

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();

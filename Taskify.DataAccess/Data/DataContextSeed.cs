using Taskify.DataAccess.Entities;

namespace Taskify.DataAccess.Data;

public static class DataContextSeed
{
    public static async Task SeedAsync(DataContext context)
    {
        try
        {
            context.Database.EnsureCreated();

            if (context.Project.Any())
            {
                return;
            }

            var projects = new ProjectEntity[]
            {
                new ProjectEntity{
                    Name = "Hotel dev",
                    CreatedDate = DateTime.Now
                },
                new ProjectEntity{
                    Name = "Hotel design",
                    CreatedDate = DateTime.Now
                }
            };
            foreach (ProjectEntity p in projects)
            {
                context.Project.Add(p);
            }
            await context.SaveChangesAsync();

            int cnt = 1;
            foreach (ProjectEntity p in projects)
            {
                var issues = new IssueEntity[]
                {
                    new IssueEntity{
                        Name = p.Name+" Issue" + cnt.ToString(),
                        CreatedDate = DateTime.Now,
                        Description = "qwerty",
                        TimeSpent = 0,
                        StatusId = 1,
                        UpdatedDate = DateTime.Now,
                        RefId = 1,
                        ProjectId = p.Id
                    },
                    new IssueEntity{
                        Name = p.Name+" Issue" + (cnt+1).ToString(),
                        CreatedDate = DateTime.Now,
                        Description = "qwerty",
                        TimeSpent = 0,
                        StatusId = 1,
                        UpdatedDate = DateTime.Now,
                        RefId = 2,
                        ProjectId = p.Id
                    },
                    new IssueEntity{
                        Name = p.Name+" Issue" + (cnt+2).ToString(),
                        CreatedDate = DateTime.Now,
                        Description = "qwerty",
                        TimeSpent = 0,
                        StatusId = 1,
                        UpdatedDate = DateTime.Now,
                        RefId = 3,
                        ProjectId = p.Id
                    }
                };
                foreach (IssueEntity i in issues)
                {
                    context.Issue.Add(i);
                }
                await context.SaveChangesAsync();
            }
        }
        catch
        {
            throw;
        }
    }
}
using Taskify.Core.Models;
using Taskify.DataAccess;
using Taskify.DataAccess.Entities;

namespace Taskify.DataAccess.Data
{
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
            }
            catch
            {
                throw;
            }
        }
    }
}
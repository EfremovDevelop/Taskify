using Taskify.DataAccess.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace Taskify.DataAccess
{
    public class DataContext : DbContext
    {
        protected readonly IConfiguration Configuration;
        public DataContext(IConfiguration configuration)
        {
            Configuration = configuration;
        }
        protected override void OnConfiguring(DbContextOptionsBuilder options)
        {
            options.UseNpgsql(Configuration.GetConnectionString("DefaultConnection"));
        }
        public virtual DbSet<Project> Project { get; set; }
        public virtual DbSet<Issue> Issue { get; set; }
        protected override void OnModelCreating(ModelBuilder
       modelBuilder)
        {
            modelBuilder.Entity<Issue>(entity =>
            {
                entity.HasOne(d => d.Project)
                .WithMany(p => p.Issue)
                .HasForeignKey(d => d.ProjectId);
            });
            modelBuilder.Entity<Issue>()
                .Property(i => i.Status)
                .HasColumnType("smallint");
        }
    }
}

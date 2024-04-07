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
        public virtual DbSet<ProjectEntity> Project { get; set; }
        public virtual DbSet<IssueEntity> Issue { get; set; }
        public virtual DbSet<UserEntity> User { get; set; }
        public virtual DbSet<ProjectUserEntity> ProjectUser { get; set; }
        protected override void OnModelCreating(ModelBuilder
       modelBuilder)
        {
            modelBuilder.Entity<IssueEntity>(entity =>
            {
                entity.HasOne(d => d.Project)
                .WithMany(p => p.Issue)
                .HasForeignKey(d => d.ProjectId);
            });
            modelBuilder.Entity<IssueEntity>()
                .Property(i => i.Status)
                .HasColumnType("smallint");

            modelBuilder.Entity<ProjectUserEntity>()
            .HasKey(pu => new { pu.UserId, pu.ProjectId });
            modelBuilder.Entity<ProjectUserEntity>()
                .HasOne(pu => pu.User)
                .WithMany(u => u.Projects)
                .HasForeignKey(pu => pu.UserId);
            modelBuilder.Entity<ProjectUserEntity>()
                .HasOne(pu => pu.Project)
                .WithMany(p => p.Users)
                .HasForeignKey(pu => pu.ProjectId);
        }
    }
}

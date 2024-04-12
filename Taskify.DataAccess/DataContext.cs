using Taskify.DataAccess.Entities;
using Microsoft.EntityFrameworkCore;
using Taskify.DataAccess.Configurations;

namespace Taskify.DataAccess
{
    public class DataContext(DbContextOptions<DataContext> options)
        : DbContext(options)
    {
        public virtual DbSet<ProjectEntity> Project { get; set; }
        public virtual DbSet<IssueEntity> Issue { get; set; }
        public virtual DbSet<UserEntity> User { get; set; }
        public virtual DbSet<ProjectUserEntity> ProjectUser { get; set; }
        public virtual DbSet<ProjectUserRoleEntity> ProjectUserRole { get; set; }
        public virtual DbSet<PermissionEntity> Permission { get; set; }
        public virtual DbSet<RoleEntity> Role { get; set; }
        public virtual DbSet<StatusIssueEntity> StatusIssue { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new ProjectConfiguration());
            modelBuilder.ApplyConfiguration(new IssueConfiguration());
            modelBuilder.ApplyConfiguration(new ProjectUserConfiguration());
            modelBuilder.ApplyConfiguration(new UserConfiguration());

            modelBuilder.ApplyConfiguration(new StatusIssueConfiguration());
            modelBuilder.ApplyConfiguration(new RoleConfiguration());
            modelBuilder.ApplyConfiguration(new RolePermissionConfiguration());
            modelBuilder.ApplyConfiguration(new PermissionConfiguration());

            base.OnModelCreating(modelBuilder);
        }
    }
}

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Taskify.DataAccess.Entities;

namespace Taskify.DataAccess.Configurations
{
    public class ProjectConfiguration : IEntityTypeConfiguration<ProjectEntity>
    {
        public void Configure(EntityTypeBuilder<ProjectEntity> builder)
        {
            builder.HasKey(p => p.Id);

            builder.HasMany(u => u.Users)
                .WithMany(p => p.Projects)
                .UsingEntity<ProjectUserEntity>(
                    r => r.HasOne<UserEntity>().WithMany().HasForeignKey(p => p.UserId),
                    l => l.HasOne<ProjectEntity>().WithMany().HasForeignKey(r => r.ProjectId));

            builder.HasMany(p => p.Issues)
                .WithOne(i => i.Project)
                .HasForeignKey(i => i.ProjectId);
        }
    }
}

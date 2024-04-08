using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Taskify.DataAccess.Entities;

namespace Taskify.DataAccess.Configurations
{
    public class ProjectUserConfiguration : IEntityTypeConfiguration<ProjectUserEntity>
    {
        public void Configure(EntityTypeBuilder<ProjectUserEntity> builder)
        {
            builder.HasKey(i => i.Id);

            builder.HasMany(pu => pu.Roles)
                .WithMany(r => r.ProjectUsers)
                .UsingEntity<ProjectUserRoleEntity>(
                    l => l.HasOne<RoleEntity>().WithMany().HasForeignKey(r => r.RoleId),
                    r => r.HasOne<ProjectUserEntity>().WithMany().HasForeignKey(pu => pu.ProjectUserId));
        }
    }
}

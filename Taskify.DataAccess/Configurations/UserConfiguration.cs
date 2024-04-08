using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Taskify.DataAccess.Entities;

namespace Taskify.DataAccess.Configurations
{
    public class UserConfiguration : IEntityTypeConfiguration<UserEntity>
    {
        public void Configure(EntityTypeBuilder<UserEntity> builder)
        {
            builder.HasKey(i => i.Id);

            builder.HasMany(u => u.Projects)
                .WithMany(p => p.Users)
                .UsingEntity<ProjectUserEntity>(
                    l => l.HasOne<ProjectEntity>().WithMany().HasForeignKey(r => r.ProjectId),
                    r => r.HasOne<UserEntity>().WithMany().HasForeignKey(p => p.UserId));
        }
    }
}

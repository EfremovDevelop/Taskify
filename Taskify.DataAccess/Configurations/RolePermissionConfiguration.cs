using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Taskify.Core.Enums;
using Taskify.DataAccess.Entities;

namespace Taskify.DataAccess.Configurations
{
    public class RolePermissionConfiguration : IEntityTypeConfiguration<RolePermissionEntity>
    {
        //private readonly AuthorizationOptions

        public void Configure(EntityTypeBuilder<RolePermissionEntity> builder)
        {
            builder.HasKey(p => p.Id);

            //var roles = Enum
            //    .GetValues<Role>()
            //    .Select(r => new RoleEntity
            //    {
            //        Id = (int)r,
            //        Name = r.ToString(),
            //    });
            //builder.HasData(roles);
        }
    }
}

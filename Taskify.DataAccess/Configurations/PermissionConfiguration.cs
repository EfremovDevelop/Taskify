using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Taskify.Core.Enums;
using Taskify.DataAccess.Entities;

namespace Taskify.DataAccess.Configurations;

public class PermissionConfiguration : IEntityTypeConfiguration<PermissionEntity>
{
    public void Configure(EntityTypeBuilder<PermissionEntity> builder)
    {
        builder.HasKey(p => p.Id);

        var permissions = Enum
            .GetValues<Permission>()
            .Select(r => new PermissionEntity
            {
                Id = (int)r,
                Name = r.ToString(),
            });
        builder.HasData(permissions);
    }
}

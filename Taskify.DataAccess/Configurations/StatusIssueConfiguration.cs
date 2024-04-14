using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Taskify.Core.Enums;
using Taskify.DataAccess.Entities;

namespace Taskify.DataAccess.Configurations;

public class StatusIssueConfiguration : IEntityTypeConfiguration<StatusIssueEntity>
{
    public void Configure(EntityTypeBuilder<StatusIssueEntity> builder)
    {
        builder.HasKey(i => i.Id);

        var statuses = Enum
            .GetValues<Status>()
            .Select(s => new StatusIssueEntity
            {
                Id = (int)s,
                Name = s.ToString()
            });
        builder.HasData(statuses);
    }
}

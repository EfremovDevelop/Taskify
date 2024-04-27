using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Taskify.DataAccess.Entities;

namespace Taskify.DataAccess.Configurations;

public class IssueCommentConfiguration : IEntityTypeConfiguration<IssueCommentEntity>
{
    public void Configure(EntityTypeBuilder<IssueCommentEntity> builder)
    {
        builder.HasKey(i => i.Id);

        builder
            .HasOne(c => c.Issue)
            .WithMany(t => t.Comments)
            .HasForeignKey(c => c.IssueId);

        builder
            .HasOne(c => c.User)
            .WithMany(u => u.IssueComments)
            .HasForeignKey(c => c.UserId);
    }
}

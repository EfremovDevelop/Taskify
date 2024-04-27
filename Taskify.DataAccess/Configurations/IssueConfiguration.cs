﻿using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Taskify.DataAccess.Entities;

namespace Taskify.DataAccess.Configurations;

public class IssueConfiguration : IEntityTypeConfiguration<IssueEntity>
{
    public void Configure(EntityTypeBuilder<IssueEntity> builder)
    {
        builder.HasKey(i => i.Id);

        builder
            .HasOne(i => i.Project)
            .WithMany(p => p.Issues)
            .HasForeignKey(i => i.ProjectId);

        builder
            .HasOne(i => i.Status)
            .WithMany(s => s.Issues)
            .HasForeignKey(i => i.StatusId);

        builder
           .HasOne(i => i.AssignedUser)
           .WithMany()
           .HasForeignKey(i => i.AssignedUserId)
           .IsRequired(false); // Если AssignedUserId может быть null
    }
}

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

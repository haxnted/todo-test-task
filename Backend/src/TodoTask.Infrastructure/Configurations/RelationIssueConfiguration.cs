using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TodoTask.Domain.Entities;

namespace TodoTask.Infrastructure.Configurations;

/// <summary>
/// Конфигурация сущности <see cref="RelationIssue"/> для базы данных.
/// </summary>
public class RelationIssueConfiguration : IEntityTypeConfiguration<RelationIssue>
{
    public void Configure(EntityTypeBuilder<RelationIssue> builder)
    {
        builder.ToTable("relation_issues");

        builder.HasKey(r => new { r.IssueId, r.RelatedIssueId });

        builder.Property(r => r.IssueId).IsRequired();
        builder.Property(r => r.RelatedIssueId).IsRequired();

        builder.HasIndex(r => new { r.IssueId, r.RelatedIssueId }).IsUnique();
    }
}
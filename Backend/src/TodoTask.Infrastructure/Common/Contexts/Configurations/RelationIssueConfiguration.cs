using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TodoTask.Domain.Entities;

namespace TodoTask.Infrastructure.Common.Contexts.Configurations;

/// <summary>
/// Конфигурация сущности <see cref="RelationIssue"/> для базы данных.
/// </summary>
public class RelationIssueConfiguration : IEntityTypeConfiguration<RelationIssue>
{
    public void Configure(EntityTypeBuilder<RelationIssue> builder)
    {
        builder.ToTable("relation_issues");

        builder.HasKey(r => r.Id);

        builder.Property(r => r.Id)
            .HasColumnName("id")
            .IsRequired();

        builder.Property(r => r.IssueId)
            .HasColumnName("issue_id")
            .IsRequired();

        builder.Property(r => r.RelatedId)
            .HasColumnName("related_id")
            .IsRequired();

        builder.HasOne(r => r.Issue)
            .WithMany(i => i.RelatedIssues)  
            .HasForeignKey(r => r.IssueId)
            .HasConstraintName("fk_relation_issue_issue")
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasIndex(r => new { r.IssueId, r.RelatedId })
            .IsUnique();
    }
}
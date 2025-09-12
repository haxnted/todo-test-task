using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TodoTask.Domain.Aggregates;
using TodoTask.Domain.Enums;
using TodoTask.Domain.ValueObjects;

namespace TodoTask.Infrastructure.Common.Contexts.Configurations;

/// <summary>
/// Конфигурация сущности <see cref="Issue"/> для базы данных.
/// </summary>
public class IssueConfiguration : IEntityTypeConfiguration<Issue>
{
    public void Configure(EntityTypeBuilder<Issue> builder)
    {
        builder.ToTable("issues");

        builder.HasKey(i => i.Id);

        builder.Property(i => i.Id)
            .HasColumnName("id")
            .IsRequired();

        builder.Property(i => i.UserId)
            .HasColumnName("user_id") 
            .IsRequired();

        builder.Property(i => i.ExecutorId)
            .HasColumnName("executor_id")
            .IsRequired(false);

        builder.Property(i => i.Title)
            .HasConversion(v => v.Value, v => Title.Of(v))
            .HasMaxLength(Title.MAX_TITLE_LENGTH)
            .HasColumnName("title")
            .IsRequired();

        builder.Property(i => i.Description)
            .HasConversion(v => v.Value, v => Description.Of(v))
            .HasMaxLength(Description.MAX_DESCRIPTION_LENGTH)
            .HasColumnName("description")
            .IsRequired();

        builder.Property(i => i.Status)
            .HasConversion(v => v.ToString(),
                v => (IssueStatus)Enum.Parse(typeof(IssueStatus), v))
            .HasMaxLength(20)
            .HasColumnName("status")
            .IsRequired();

        builder.Property(i => i.Priority)
            .HasConversion(v => v.ToString(),
                v => (IssuePriority)Enum.Parse(typeof(IssuePriority), v))
            .HasMaxLength(20)
            .HasColumnName("priority")
            .IsRequired();

        builder.Property(i => i.CreatedAt)
            .HasColumnName("created_at")
            .IsRequired();

        builder.Property(i => i.UpdatedAt)
            .HasColumnName("updated_at")
            .IsRequired();

        builder.HasMany(i => i.SubIssues)
            .WithOne()
            .HasForeignKey(i => i.ParentIssueId)
            .OnDelete(DeleteBehavior.Restrict);
        
        builder.HasMany(i => i.RelatedIssues)
            .WithOne()
            .HasForeignKey(i => i.IssueId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TodoTask.Domain.Aggregates;

namespace TodoTask.Infrastructure.Configurations;

/// <summary>
/// Конфигурация сущности <see cref="Issue"/> для базы данных.
/// </summary>
public class IssueConfiguration : IEntityTypeConfiguration<Issue>
{
    public void Configure(EntityTypeBuilder<Issue> builder)
    {
        builder.ToTable("issues");

        builder.HasKey(i => i.Id);

        builder.Property(i => i.Title)
            .IsRequired()
            .HasMaxLength(200);

        builder.Property(i => i.Description)
            .HasMaxLength(2000);

        builder.Property(i => i.Status)
            .IsRequired();

        builder.Property(i => i.Priority)
            .IsRequired();

        builder.Property(i => i.CreatedAt)
            .IsRequired();

        builder.Property(i => i.UpdatedAt)
            .IsRequired();

        builder
            .HasMany(i => i.SubIssues)
            .WithOne()
            .HasForeignKey(i => i.ParentIssueId)
            .OnDelete(DeleteBehavior.Restrict);

        builder
            .HasMany(i => i.RelatedIssues)
            .WithOne()
            .HasForeignKey(r => r.IssueId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}
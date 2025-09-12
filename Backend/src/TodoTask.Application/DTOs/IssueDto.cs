using TodoTask.Domain.Enums;

namespace TodoTask.Application.DTOs;

/// <summary>
/// DTO для основной задачи.
/// </summary>
public record IssueDto(
    Guid Id,
    string Title,
    string? Description,
    IssueStatus Status,
    IssuePriority Priority,
    Guid? ExecutorId,
    IReadOnlyCollection<SubIssueDto> SubIssues,
    IReadOnlyCollection<RelationIssueDto> RelatedIssues);

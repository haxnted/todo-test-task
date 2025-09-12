using TodoTask.Domain.Enums;

namespace TodoTask.Application.DTOs;

/// <summary>
/// DTO для подзадачи.
/// </summary>
public record SubIssueDto(
    Guid Id,
    string Title,
    string? Description,
    IssueStatus Status,
    IssuePriority Priority,
    Guid? ExecutorId);

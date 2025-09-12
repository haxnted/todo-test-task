using TodoTask.Domain.Enums;

namespace TodoTask.Application.Handlers.Issues.Commands.CreateIssue;

/// <summary>
/// Команда на добавление задачи.
/// </summary>
public sealed record CreateIssueCommand(
    Guid UserId,
    IssueStatus Status,
    IssuePriority Priority,
    Guid? ExecutorId,
    string Title,
    string? Description);
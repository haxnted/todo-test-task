using TodoTask.Domain.Enums;

namespace TodoTask.Application.Handlers.Issues.Commands.UpdateIssue;

/// <summary>
/// Команда на обновление задачи.
/// </summary>
public sealed record UpdateIssueCommand(Guid IssueId, string Title, string Description, IssueStatus Status, IssuePriority Priority);
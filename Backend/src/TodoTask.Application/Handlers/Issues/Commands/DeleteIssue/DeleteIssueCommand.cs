namespace TodoTask.Application.Handlers.Issues.Commands.DeleteIssue;

/// <summary>
/// Команда на удаление задачи.
/// </summary>
public sealed record DeleteIssueCommand(Guid IssueId);

namespace TodoTask.Application.Handlers.Issues.Commands.RemoveSubIssue;

/// <summary>
/// Команда на удаление подзадачи.
/// </summary>
public sealed record RemoveSubIssueCommand(Guid IssueId, Guid SubIssueId);

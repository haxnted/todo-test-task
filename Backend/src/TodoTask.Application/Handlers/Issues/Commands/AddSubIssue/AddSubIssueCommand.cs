namespace TodoTask.Application.Handlers.Issues.Commands.AddSubIssue;

/// <summary>
/// Команда на добавление подзадачи.
/// </summary>
public sealed record AddSubIssueCommand(Guid IssueId, Guid SubIssueId);
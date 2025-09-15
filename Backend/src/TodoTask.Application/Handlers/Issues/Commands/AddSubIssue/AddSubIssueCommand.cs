namespace TodoTask.Application.Handlers.Issues.Commands.AddSubIssue;

/// <summary>
/// Команда на добавление подзадачи.
/// </summary>
/// <param name="IssueId">Идентификатор задачи.</param>
/// <param name="SubIssueId">Идентификатор подзадачи.</param>
public sealed record AddSubIssueCommand(Guid IssueId, Guid SubIssueId);
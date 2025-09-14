namespace TodoTask.Application.Handlers.Issues.Commands.RemoveSubIssue;

/// <summary>
/// Команда на удаление подзадачи.
/// </summary>
/// <param name="IssueId">Идентификатор задачи.</param>
/// <param name="SubIssueId">Идентификатор подзадачи.</param>
public sealed record RemoveSubIssueCommand(Guid IssueId, Guid SubIssueId);

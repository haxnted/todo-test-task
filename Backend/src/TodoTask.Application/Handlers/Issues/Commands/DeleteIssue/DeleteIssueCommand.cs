namespace TodoTask.Application.Handlers.Issues.Commands.DeleteIssue;

/// <summary>
/// Команда на удаление задачи.
/// </summary>
/// <param name="IssueId">Идентификатор задачи.</param>
public sealed record DeleteIssueCommand(Guid IssueId);

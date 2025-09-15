namespace TodoTask.Application.Handlers.Issues.Queries.GetIssue;

/// <summary>
/// Запрос на получение полного снимка задачи.
/// </summary>
/// <param name="IssueId">Идентификатор задачи.</param>
public sealed record GetIssueQuery(Guid IssueId);
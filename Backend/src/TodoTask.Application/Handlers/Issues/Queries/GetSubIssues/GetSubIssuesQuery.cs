namespace TodoTask.Application.Handlers.Issues.Queries.GetSubIssues;

/// <summary>
/// Запрос на получение списка подзадач.
/// </summary>
/// <param name="IssueId">Идентификатор задачи.</param>
public sealed record GetSubIssuesQuery(Guid IssueId);
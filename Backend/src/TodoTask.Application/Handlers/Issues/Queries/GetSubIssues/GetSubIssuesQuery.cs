namespace TodoTask.Application.Handlers.Issues.Queries.GetSubIssues;

/// <summary>
/// Запрос на получение списка подзадач.
/// </summary>
public sealed record GetSubIssuesQuery(Guid IssueId);
namespace TodoTask.Application.Handlers.Issues.Queries.GetIssue;

/// <summary>
/// Запрос на получение полного снимка задачи.
/// </summary>
public sealed record GetIssueQuery(Guid IssueId);
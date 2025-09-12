namespace TodoTask.Application.Handlers.Issues.Queries.GetRelatedIssues;

/// <summary>
/// Запрос на получение связанных задач.
/// </summary>
public sealed record GetRelatedIssuesQuery(Guid IssueId);
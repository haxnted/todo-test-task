namespace TodoTask.Application.Handlers.Issues.Queries.GetRelatedIssues;

/// <summary>
/// Запрос на получение связанных задач.
/// </summary>
/// <param name="IssueId">Идентификатор задачи.</param>
public sealed record GetRelatedIssuesQuery(Guid IssueId);
namespace TodoTask.Application.Handlers.Issues.Queries.GetAllIssues;

/// <summary>
/// Запрос на получение задач с пагинацией.
/// </summary>
/// <param name="PageIndex">Номер страницы.</param>
/// <param name="PageSize">Размер страницы.</param>
public record GetIssuesWithPaginationQuery(int PageIndex, int PageSize);
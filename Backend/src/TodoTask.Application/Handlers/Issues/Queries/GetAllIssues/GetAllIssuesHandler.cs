using TodoTask.Application.AppServices.Abstractions;
using TodoTask.Application.DTOs;
using TodoTask.Domain.Aggregates;

namespace TodoTask.Application.Handlers.Issues.Queries.GetAllIssues;

/// <summary>
/// Обработчик запроса на получение всех текущих задач.
/// </summary>
public class GetAllIssuesHandler(IIssueService IssueService)
{
    public async Task<IReadOnlyList<IssueDto>> Handle(GetAllIssuesQuery query, CancellationToken cancellationToken)
    {
        var issues = await IssueService.GetAllIssuesAsync(cancellationToken);

        var mappedIssues = issues.Select(ToIssueDto)
            .ToList();

        return mappedIssues;
    }

    /// <summary>
    /// Преобразует доменную модель <see cref="Issue"/> в модель DTO <see cref="IssueDto"/>.
    /// </summary>
    /// <param name="issue">Задача.</param>
    private static IssueDto ToIssueDto(Issue issue)
    {
        return new(issue.Id,
            issue.Title.Value,
            issue.Description.Value,
            issue.Status,
            issue.Priority,
            issue.ExecutorId,
            issue.SubIssues.Select(s => new SubIssueDto(s.Id,
                    s.Title.Value,
                    s.Description.Value,
                    s.Status,
                    s.Priority,
                    s.ExecutorId))
                .ToList(),
            issue.RelatedIssues.Select(r => new RelationIssueDto(r.RelatedId))
                .ToList());
    }
}
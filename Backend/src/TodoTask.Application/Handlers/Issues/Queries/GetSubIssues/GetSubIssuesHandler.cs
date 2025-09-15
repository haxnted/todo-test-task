using TodoTask.Application.AppServices.Abstractions;
using TodoTask.Application.DTOs;

namespace TodoTask.Application.Handlers.Issues.Queries.GetSubIssues;

/// <summary>
/// Обработчик запроса <see cref="GetSubIssuesQuery"/>
/// </summary>
public class GetSubIssuesHandler(IIssueService IssueService)
{
    public async Task<IReadOnlyCollection<SubIssueDto>> Handle(GetSubIssuesQuery request, CancellationToken cancellationToken)
    {
        var issue = await IssueService.GetIssueSnapshotAsync(request.IssueId, cancellationToken);

        return issue.SubIssues
            .Select(s => new SubIssueDto(s.Id,
                s.Title.Value,
                s.Description.Value,
                s.Status,
                s.Priority,
                s.ExecutorId))
            .ToList();
    }
}
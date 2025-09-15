using TodoTask.Application.AppServices.Abstractions;
using TodoTask.Application.DTOs;

namespace TodoTask.Application.Handlers.Issues.Queries.GetIssue;

/// <summary>
/// Обработчик запроса <see cref="GetIssueQuery"/>
/// </summary>
public class GetIssueHandler(IIssueService IssueService)
{
    public async Task<IssueDto> Handle(GetIssueQuery request, CancellationToken cancellationToken)
    {
        var issue = await IssueService.GetIssueSnapshotAsync(request.IssueId, cancellationToken);

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
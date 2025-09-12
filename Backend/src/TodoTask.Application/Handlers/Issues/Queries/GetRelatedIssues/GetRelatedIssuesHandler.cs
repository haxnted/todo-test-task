using TodoTask.Application.AppServices.Abstractions;
using TodoTask.Application.DTOs;

namespace TodoTask.Application.Handlers.Issues.Queries.GetRelatedIssues;

/// <summary>
/// Обработчик запроса <see cref="GetRelatedIssuesQuery"/>
/// </summary>
public class GetRelatedIssuesHandler
{
    private readonly IIssueService _issueService;

    public GetRelatedIssuesHandler(IIssueService issueService)
    {
        _issueService = issueService;
    }

    public async Task<IReadOnlyCollection<RelationIssueDto>> Handle(GetRelatedIssuesQuery request, CancellationToken cancellationToken)
    {
        var issue = await _issueService.GetIssueSnapshotAsync(request.IssueId, cancellationToken);

        return issue.RelatedIssues
            .Select(r => new RelationIssueDto(r.RelatedId))
            .ToList();
    }
}

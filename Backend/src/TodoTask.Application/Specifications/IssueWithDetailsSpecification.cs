using Ardalis.Specification;
using TodoTask.Domain.Aggregates;

namespace TodoTask.Application.Specifications;

/// <summary>
/// Спецификация для выборки задачи вместе с подробностями:
/// подзадачи и связанные задачи.
/// </summary>
public sealed class IssueWithDetailsSpecification : Specification<Issue>
{
    public IssueWithDetailsSpecification(Guid issueId)
    {
        Query.Include(issue => issue.SubIssues);
        Query.Include(issue => issue.RelatedIssues);
        Query.Where(issue => issue.Id == issueId);
    }
}
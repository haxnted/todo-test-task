using Ardalis.Specification;
using TodoTask.Domain.Aggregates;

namespace TodoTask.Application.Specifications;

/// <summary>
/// Спецификация для выборки задач вместе с подробностями:
/// подзадачи и связанные задачи, с поддержкой пагинации.
/// </summary>
public sealed class IssuesWithDetailsPaginatedSpecification : Specification<Issue>
{
    public IssuesWithDetailsPaginatedSpecification(int pageIndex, int pageSize)
    {
        Query
            .Include(issue => issue.SubIssues)
            .Include(issue => issue.RelatedIssues)
            .Where(issue => issue.ParentIssueId == null)
            .OrderBy(issue => issue.Title) 
            .Skip((pageIndex - 1) * pageSize)
            .Take(pageSize);
    }
}
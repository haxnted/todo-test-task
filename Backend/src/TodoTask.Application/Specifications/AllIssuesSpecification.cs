using Ardalis.Specification;
using TodoTask.Domain.Aggregates;

namespace TodoTask.Application.Specifications;

/// <summary>
/// Спецификация для выборки всех главных задач, с вложенными подзадачами и связанными задачами.
/// </summary>
public sealed class AllIssuesWithDependenciesSpecification : Specification<Issue>
{
    public AllIssuesWithDependenciesSpecification()
    {
        Query.Include(issue => issue.SubIssues)
            .Include(issue => issue.RelatedIssues)
            .Where(i => i.ParentIssueId == null);
    }
}
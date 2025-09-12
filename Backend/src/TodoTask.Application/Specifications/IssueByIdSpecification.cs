using Ardalis.Specification;
using TodoTask.Domain.Aggregates;

namespace TodoTask.Application.Specifications;

/// <summary>
/// Спецификация для выборки задачи только по идентификатору, без вложенных сущностей.
/// </summary>
public sealed class IssueByIdSpecification : Specification<Issue>
{
    public IssueByIdSpecification(Guid issueId)
    {
        Query.Where(issue => issue.Id == issueId);
    }
}
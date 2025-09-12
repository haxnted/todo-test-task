namespace TodoTask.Application.Handlers.Issues.Commands.RemoveRelation;

/// <summary>
/// Команда на удаление связи между задачами.
/// </summary>
public sealed record RemoveRelationCommand(Guid IssueId, Guid RelatedIssueId);
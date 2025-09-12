namespace TodoTask.Application.Handlers.Issues.Commands.AddRelation;

/// <summary>
/// Команда на добавление связи между задачами.
/// </summary>
public sealed record AddRelationCommand(Guid IssueId, Guid RelationIssueId);
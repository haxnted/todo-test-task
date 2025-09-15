namespace TodoTask.Application.Handlers.Issues.Commands.AddRelation;

/// <summary>
/// Команда на добавление связи между задачами.
/// </summary>
/// <param name="IssueId">Идентификатор задачи.</param>
/// <param name="RelationIssueId">Идентификатор связанной задачи.</param>
public sealed record AddRelationCommand(Guid IssueId, Guid RelationIssueId);
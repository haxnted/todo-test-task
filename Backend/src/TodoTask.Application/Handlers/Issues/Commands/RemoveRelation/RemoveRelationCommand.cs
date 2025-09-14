namespace TodoTask.Application.Handlers.Issues.Commands.RemoveRelation;

/// <summary>
/// Команда на удаление связи между задачами.
/// </summary>
/// <param name="IssueId">Идентификатор задачи.</param>
/// <param name="RelatedIssueId">Идентификатор связанной задачи.</param>
public sealed record RemoveRelationCommand(Guid IssueId, Guid RelatedIssueId);
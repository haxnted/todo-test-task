namespace TodoTask.Application.Handlers.Issues.Commands.RemoveExecutor;

/// <summary>
/// Команда на удаление исполнителя.
/// </summary>
/// <param name="IssueId">Идентификатор задачи.</param>
public sealed record RemoveExecutorCommand(Guid IssueId);
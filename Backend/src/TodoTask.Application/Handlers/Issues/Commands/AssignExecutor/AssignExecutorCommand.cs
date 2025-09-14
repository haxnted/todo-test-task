namespace TodoTask.Application.Handlers.Issues.Commands.AssignExecutor;

/// <summary>
/// Команда на назначение исполнителя.
/// </summary>
/// <param name="IssueId">Идентификатор задачи.</param>
/// <param name="ExecutorId">Идентификатор исполнителя.</param>
public sealed record AssignExecutorCommand(Guid IssueId, Guid ExecutorId);
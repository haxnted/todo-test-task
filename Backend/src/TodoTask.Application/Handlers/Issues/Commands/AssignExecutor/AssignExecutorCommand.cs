namespace TodoTask.Application.Handlers.Issues.Commands.AssignExecutor;

/// <summary>
/// Команда на назначение исполнителя.
/// </summary>
public sealed record AssignExecutorCommand(Guid IssueId, Guid ExecutorId);
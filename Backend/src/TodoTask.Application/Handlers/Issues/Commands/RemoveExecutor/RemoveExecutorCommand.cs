namespace TodoTask.Application.Handlers.Issues.Commands.RemoveExecutor;

/// <summary>
/// Команда на удаление исполнителя.
/// </summary>
public sealed record RemoveExecutorCommand(Guid IssueId);
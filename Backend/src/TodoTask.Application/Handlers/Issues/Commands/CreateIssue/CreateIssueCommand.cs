using TodoTask.Domain.Enums;

namespace TodoTask.Application.Handlers.Issues.Commands.CreateIssue;

/// <summary>
/// Команда на добавление задачи.
/// </summary>
/// <param name="UserId">Идентификатор пользователя.</param>
/// <param name="Title">Заголовок.</param>
/// <param name="Description">Описание задачи.</param>
/// <param name="Status">Статус задачи.</param>
/// <param name="Priority">Приоритет задачи.</param>
/// <param name="ExecutorId">Идентификатор исполнителя задачи.</param>
public sealed record CreateIssueCommand(
    Guid UserId,
    IssueStatus Status,
    IssuePriority Priority,
    Guid? ExecutorId,
    string Title,
    string Description);
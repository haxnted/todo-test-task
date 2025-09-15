using TodoTask.Domain.Enums;

namespace TodoTask.Application.Handlers.Issues.Commands.UpdateIssue;

/// <summary>
/// Команда на обновление задачи.
/// </summary>
/// <param name="IssueId">Идентификатор задачи.</param>
/// <param name="Title">Заголовок.</param>
/// <param name="Description">Описание задачи.</param>
/// <param name="Status">Статус задачи.</param>
/// <param name="Priority">Приоритет задачи.</param>
public sealed record UpdateIssueCommand(
    Guid IssueId,
    string Title,
    string Description,
    IssueStatus Status,
    IssuePriority Priority);
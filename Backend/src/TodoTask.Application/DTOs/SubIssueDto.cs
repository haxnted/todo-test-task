using TodoTask.Domain.Enums;

namespace TodoTask.Application.DTOs;

/// <summary>
/// DTO для  подзадачи.
/// </summary>
/// <param name="Id">Идентификатор задачи.</param>
/// <param name="Title">Заголовок.</param>
/// <param name="Description">Описание задачи.</param>
/// <param name="Status">Статус задачи.</param>
/// <param name="Priority">Приоритет задачи.</param>
/// <param name="ExecutorId">Идентификатор исполнителя задачи.</param>
public record SubIssueDto(
    Guid Id,
    string Title,
    string? Description,
    IssueStatus Status,
    IssuePriority Priority,
    Guid? ExecutorId);
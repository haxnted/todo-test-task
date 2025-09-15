using TodoTask.Domain.Enums;

namespace TodoTask.Application.DTOs;

/// <summary>
/// DTO для основной задачи.
/// </summary>
/// <param name="Id">Идентификатор задачи.</param>
/// <param name="Title">Заголовок.</param>
/// <param name="Description">Описание задачи.</param>
/// <param name="Status">Статус задачи.</param>
/// <param name="Priority">Приоритет задачи.</param>
/// <param name="ExecutorId">Идентификатор исполнителя задачи.</param>
/// <param name="SubIssues">Коллекция подзадач.</param>
/// <param name="RelatedIssues">Коллекция связанных задач.</param>
public record IssueDto(
    Guid Id,
    string Title,
    string? Description,
    IssueStatus Status,
    IssuePriority Priority,
    Guid? ExecutorId,
    IReadOnlyCollection<SubIssueDto> SubIssues,
    IReadOnlyCollection<RelationIssueDto> RelatedIssues);
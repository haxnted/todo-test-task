using TodoTask.Domain.Aggregates;
using TodoTask.Domain.Entities;
using TodoTask.Domain.Enums;

namespace TodoTask.Application.AppServices.Abstractions;

/// <summary>
/// Сервис для управления задачами.
/// Предоставляет операции по созданию, обновлению, удалению и получению задач.
/// </summary>
public interface IIssueService
{
    /// <summary>
    /// Создаёт новую задачу.
    /// </summary>
    Task CreateIssueAsync(
        Guid issueId,
        Guid userId,
        IssueStatus status,
        IssuePriority priority,
        Guid? executorId,
        string title,
        string description,
        CancellationToken cancellationToken);

    /// <summary>
    /// Обновляет общую информацию задачи: имя, описание, статус, приоритет.
    /// </summary>
    Task UpdateIssueAsync(
        Guid issueId,
        string title,
        string description,
        IssueStatus status,
        IssuePriority priority,
        CancellationToken cancellationToken);

    /// <summary>
    /// Назначает исполнителя задаче.
    /// </summary>
    Task AssignExecutorAsync(
        Guid issueId,
        Guid executorId,
        CancellationToken cancellationToken);

    /// <summary>
    /// Убирает исполнителя у задачи.
    /// </summary>
    Task RemoveExecutorAsync(
        Guid issueId,
        CancellationToken cancellationToken);

    /// <summary>
    /// Добавляет подзадачу к задаче.
    /// </summary>
    Task AddSubIssueAsync(
        Guid issueId,
        Guid subIssueId,
        CancellationToken cancellationToken);

    /// <summary>
    /// Удаляет подзадачу у задачи.
    /// </summary>
    Task RemoveSubIssueAsync(
        Guid issueId,
        Guid subIssueId,
        CancellationToken cancellationToken);

    /// <summary>
    /// Добавляет связь между задачами (related issue).
    /// </summary>
    Task AddRelationAsync(
        Guid issueId,
        Guid relationIssueId,
        CancellationToken cancellationToken);

    /// <summary>
    /// Удаляет связь между задачами.
    /// </summary>
    Task RemoveRelationAsync(
        Guid issueId,
        Guid relatedIssueId,
        CancellationToken cancellationToken);

    /// <summary>
    /// Получает снимок задачи по идентификатору Задачи (с подзадачами и связями).
    /// </summary>
    Task<Issue> GetIssueSnapshotAsync(
        Guid issueId,
        CancellationToken cancellationToken);

    /// <summary>
    /// Получает все задачи с вложенными подзадачами и связанными задачами.
    /// </summary>
    Task<IReadOnlyList<Issue>> GetAllIssuesAsync(CancellationToken cancellationToken);

    /// <summary>
    /// Удаляет задачу.
    /// </summary>
    Task DeleteIssueAsync(
        Guid issueId,
        CancellationToken cancellationToken);
}
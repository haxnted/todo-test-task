using TodoTask.Domain.Entities;
using TodoTask.Domain.Enums;
using TodoTask.Domain.Exceptions;

namespace TodoTask.Domain.Aggregates;

/// <summary>
/// Агрегат-сущность обозначающая задачу.
/// </summary>
public class Issue
{
    /// <summary>
    /// Идентификатор задачи.
    /// </summary>
    public Guid Id { get; }

    /// <summary>
    /// Идентификатор пользователя который создал задачу.
    /// </summary>
    public Guid UserId { get; }

    /// <summary>
    /// Статус задачи.
    /// </summary>
    public IssueStatus Status { get; private set; }

    /// <summary>
    /// Приоритет задачи.
    /// </summary>
    public IssuePriority Priority { get; private set; }

    /// <summary>
    /// Идентификатор пользователя которому принадлежит задача.
    /// </summary>
    public Guid? ExecutorId { get; private set; }

    /// <summary>
    /// Название задачи.
    /// </summary>
    public string Title { get; private set; }

    /// <summary>
    /// Описание задачи.
    /// </summary>
    public string? Description { get; private set; }

    /// <summary>
    /// Дата создания задачи.
    /// </summary>
    public DateTime CreatedAt { get; }

    /// <summary>
    /// Дата обновления задачи.
    /// </summary>
    public DateTime UpdatedAt { get; private set; }

    /// <summary>
    /// Родительская задача (для подзадач).
    /// </summary>
    public Guid? ParentIssueId { get; private set; }


    private readonly List<Issue> _subIssues = [];

    /// <summary>
    /// Коллекция подзадач.
    /// </summary>
    public virtual ICollection<Issue> SubIssues => _subIssues.AsReadOnly();

    private readonly List<RelationIssue> _relatedIssues = [];

    /// <summary>
    /// Коллекция связанных задач.
    /// </summary>
    public virtual ICollection<RelationIssue> RelatedIssues => _relatedIssues.AsReadOnly();


    /// <summary>
    /// Приватный конструктор, который используется в фабричном методе.
    /// </summary>
    private Issue(
        Guid issueId,
        Guid userId,
        IssueStatus status,
        IssuePriority priority,
        Guid? executorId,
        string title,
        string? description)
    {
        Id = issueId;
        UserId = userId;
        Status = status;
        Priority = priority;
        ExecutorId = executorId;
        Title = title;
        Description = description;
        CreatedAt = DateTime.UtcNow;
        UpdatedAt = DateTime.UtcNow;
    }

    /// <summary>
    /// Фабричный метод для создания нового экземпляра <see cref="Issue"/>.
    /// </summary>
    /// <param name="issueId">Идентификатор задачи.</param>
    /// <param name="userId">Идентификатор создателя задачи.</param>
    /// <param name="status">Статус задачи.</param>
    /// <param name="priority">Приоритет задачи.</param>
    /// <param name="executorId">Идентификатор исполнителя задачи.</param>
    /// <param name="title">Название задачи.</param>
    /// <param name="description">Описание задачи.</param>
    /// <exception cref="IssueException">
    /// Если название задачи пустое.
    /// </exception>
    public static Issue Create(
        Guid issueId,
        Guid userId,
        IssueStatus status,
        IssuePriority priority,
        Guid? executorId,
        string title,
        string? description)
    {
        if (string.IsNullOrWhiteSpace(title))
        {
            throw new IssueException("Заголовок задачи не может быть пустым.");
        }

        return new Issue(issueId, userId, status, priority, executorId, title, description);
    }

    /// <summary>
    /// Обновление исполнителя задачи.
    /// </summary>
    public void UpdateExecutor(Guid executorId)
    {
        if (executorId == ExecutorId)
        {
            throw new IssueException("Этот исполнитель уже является исполнителем этой задачи.");
        }

        ExecutorId = executorId;

        UpdatedAt = DateTime.UtcNow;
    }

    /// <summary>
    /// Удаление исполнителя задачи.
    /// </summary>
    public void DeleteExecutor()
    {
        if (ExecutorId == null)
        {
            throw new IssueException("Эта задача не имеет исполнителя.");
        }

        ExecutorId = null;

        UpdatedAt = DateTime.UtcNow;
    }

    /// <summary>
    /// Обновление общей информации о задаче.
    /// </summary>
    /// <param name="title">Название задачи.</param>
    /// <param name="description">Описание задачи.</param>
    /// <param name="priority">Приоритет задачи.</param>
    /// <param name="status">Статус задачи.</param>
    public void UpdateGeneralInformation(string title, string description, IssuePriority priority, IssueStatus status)
    {
        Title = title;
        Description = description;
        Priority = priority;
        Status = status;

        UpdatedAt = DateTime.UtcNow;
    }

    /// <summary>
    /// Обновление приоритета задачи.
    /// </summary>
    public void ChangePriority(IssuePriority priority)
    {
        Priority = priority;

        UpdatedAt = DateTime.UtcNow;
    }

    /// <summary>
    /// Обновление статуса задачи.
    /// </summary>
    public void ChangeStatus(IssueStatus status)
    {
        Status = status;

        UpdatedAt = DateTime.UtcNow;
    }

    /// <summary>
    /// Добавление связанной задачи.
    /// </summary>
    /// <param name="relation">Задача.</param>
    /// <exception cref="IssueException">
    /// Если задачи уже связаны.
    /// </exception>
    public void AddRelation(RelationIssue relation)
    {
        if (_relatedIssues.Any(r => r.RelatedIssueId == relation.RelatedIssueId))
            throw new IssueException("Задачи уже связаны.");

        _relatedIssues.Add(relation);

        UpdatedAt = DateTime.UtcNow;
    }

    /// <summary>
    /// Удаление связанной задачи.
    /// </summary>
    /// <param name="relation">Задача.</param>
    public void RemoveRelation(RelationIssue relation)
    {
        var existing = _relatedIssues.FirstOrDefault(r => r.RelatedIssueId == relation.RelatedIssueId);
        if (existing == null)
            throw new IssueException("Такая связь не существует.");

        _relatedIssues.Remove(existing);

        UpdatedAt = DateTime.UtcNow;
    }

    /// <summary>
    /// Добавление подзадачи.
    /// </summary>
    /// <param name="subIssue">Задача.</param>
    /// <exception cref="IssueException">
    /// Если задача уже является подзадачей.
    /// </exception>
    public void AddSubIssue(Issue subIssue)
    {
        if (_subIssues.Any(s => s.Id == subIssue.Id))
            throw new IssueException("Задача уже является подзадачей.");

        subIssue.ParentIssueId = Id;
        _subIssues.Add(subIssue);

        UpdatedAt = DateTime.UtcNow;
    }

    /// <summary>
    /// Удаление подзадачи.
    /// </summary>
    /// <param name="subIssueId">Идентификатор подзадачи.</param>
    public void RemoveSubIssue(Guid subIssueId)
    {
        var existing = _subIssues.FirstOrDefault(s => s.Id == subIssueId);
        if (existing == null)
            throw new IssueException("Такая подзадача не существует.");

        existing.ParentIssueId = null;
        _subIssues.Remove(existing);

        UpdatedAt = DateTime.UtcNow;
    }
}

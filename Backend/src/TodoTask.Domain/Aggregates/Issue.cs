using TodoTask.Domain.Entities;
using TodoTask.Domain.Enums;
using TodoTask.Domain.Exceptions;
using TodoTask.Domain.ValueObjects;

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
    public Guid UserId { get; private set; }

    /// <summary>
    /// Статус задачи.
    /// </summary>
    public IssueStatus Status { get; private set; }

    /// <summary>
    /// Приоритет задачи.
    /// </summary>
    public IssuePriority Priority { get; private set; }

    /// <summary>
    /// Идентификатор исполнителя задачи.
    /// </summary>
    public Guid? ExecutorId { get; private set; }

    /// <summary>
    /// Название задачи.
    /// </summary>
    public Title Title { get; private set; }

    /// <summary>
    /// Описание задачи.
    /// </summary>
    public Description Description { get; private set; }

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
        Guid id,
        Guid userId,
        IssueStatus status,
        IssuePriority priority,
        Guid? executorId,
        Title title,
        Description description)
    {
        Id = id;
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
    /// <param name="id">Идентификатор задачи.</param>
    /// <param name="userId">Идентификатор создателя задачи.</param>
    /// <param name="status">Статус задачи.</param>
    /// <param name="priority">Приоритет задачи.</param>
    /// <param name="executorIssueId">Идентификатор исполнителя задачи.</param>
    /// <param name="title">Название задачи.</param>
    /// <param name="description">Описание задачи.</param>
    /// <exception cref="IssueException">
    /// Если название задачи пустое.
    /// </exception>
    public static Issue Create(
        Guid id,
        Guid userId,
        IssueStatus status,
        IssuePriority priority,
        Guid? executorIssueId,
        string title,
        string description)
    {
        return new(id, 
            userId, 
            status, 
            priority, 
            executorIssueId,
            Title.Of(title),
            Description.Of(description));
    }

    /// <summary>
    /// Обновление исполнителя задачи.
    /// </summary>
    /// <exception cref="IssueException">
    /// Этот исполнитель уже является исполнителем этой задачи.
    /// </exception>
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
    /// <exception cref="IssueException">
    /// Если пользователь попытается удалить исполнителя который не назначен.
    /// </exception>
    public void RemoveExecutor()
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
    public void UpdateGeneralInformation(
        Title title,
        Description description,
        IssuePriority priority,
        IssueStatus status)
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
    /// Добавляет связь между смежными задачами.
    /// </summary>
    /// <param name="relationIssue">Ссылка на смежную задачу.</param>
    /// <exception cref="IssueException">
    /// Связь уже существует.
    /// </exception>
    public void AddRelation(RelationIssue relationIssue)
    {
        if (_relatedIssues.Any(r => r.RelatedId == relationIssue.RelatedId))
        {
            throw new IssueException("Связь уже существует.");
        }

        _relatedIssues.Add(relationIssue);
        UpdatedAt = DateTime.UtcNow;
    }
    
    /// <summary>
    /// Удаление связи с задачей.
    /// </summary>
    /// <param name="relatedIssueId">Идентификатор связанной задачи.</param>
    /// <exception cref="IssueException">
    /// Связь не существует.
    /// </exception>
    public void RemoveRelation(Guid relatedIssueId)
    {
        var existing = _relatedIssues.FirstOrDefault(r => r.RelatedId == relatedIssueId);

        if (existing == null)
            throw new IssueException("Связь не найдена.");

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
using TodoTask.Domain.Aggregates;
using TodoTask.Domain.Exceptions;
using TodoTask.Domain.ValueObjects;

namespace TodoTask.Domain.Entities;

/// <summary>
/// Объект-сущность связанных задач.
/// </summary>
/// <remarks>
/// Используется для реализации many-to-many связи.
/// </remarks>
public class RelationIssue
{
    /// <summary>
    /// Идентификатор связи (первичный ключ)
    /// </summary>
    public Guid Id { get; private set; }

    /// <summary>
    /// Идентификатор задачи.
    /// </summary>
    public Guid IssueId { get; private set; }
    
    /// <summary>
    /// Ссылка на задачу.
    /// </summary>
    public Issue Issue { get; private set; }

    /// <summary>
    /// Идентификатор связанной задачи.
    /// </summary>
    public Guid RelatedId { get; private set; }
    
    /// <summary>
    /// Ссылка на связанную задачу.
    /// </summary>
    public Issue RelatedIssue { get; private set; }
    
    /// <summary>
    /// Для EF.
    /// </summary>
    private RelationIssue() {}
    
    /// <summary>
    /// Приватный конструктор, используется в фабричном методе.
    /// </summary>
    /// <param name="id">Идентификатор связи</param>
    /// <param name="issueId">Идентификатор задачи.</param>
    /// <param name="relatedId">Идентификатор связанной задачи.</param>
    private RelationIssue(Guid id, Guid issueId, Guid relatedId)
    {
        Id = id;
        IssueId = issueId;
        RelatedId = relatedId;
    }

    /// <summary>
    /// Фабричный метод для создания нового экземпляра <see cref="RelationIssue"/>.
    /// </summary>
    /// <param name="id">Идентификатор связи</param>
    /// <param name="issueId">Идентификатор задачи.</param>
    /// <param name="relatedIssueId">Идентификатор связанной задачи.</param>
    public static RelationIssue Create(Guid id, Guid issueId, Guid relatedIssueId)
    {
        if (issueId == relatedIssueId)
        {
            throw new RelationIssueException("Нельзя связать задачу с самой собой.");
        }
        
        return new(id, issueId, relatedIssueId);
    }
}
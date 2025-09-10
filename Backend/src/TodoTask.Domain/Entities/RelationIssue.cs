using TodoTask.Domain.Aggregates;
using TodoTask.Domain.Exceptions;

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
    /// Идентификатор задачи.
    /// </summary>
    public Guid IssueId { get; }
    
    /// <summary>
    /// Ссылка на задачу.
    /// </summary>
    public Issue Issue => null!;

    /// <summary>
    /// Идентификатор связанной задачи.
    /// </summary>
    public Guid RelatedIssueId { get; }
    
    /// <summary>
    /// Ссылка на связанную задачу.
    /// </summary>
    public Issue RelatedIssue => null!;
    
    /// <summary>
    /// Приватный конструктор, используется в фабричном методе.
    /// </summary>
    /// <param name="issueId">Идентификатор задачи.</param>
    /// <param name="relatedIssueId">Идентификатор связанной задачи.</param>
    private RelationIssue(Guid issueId, Guid relatedIssueId)
    {
        IssueId = issueId;
        RelatedIssueId = relatedIssueId;
    }

    /// <summary>
    /// Фабричный метод для создания нового экземпляра <see cref="RelationIssue"/>.
    /// </summary>
    /// <param name="issueId">Идентификатор задачи.</param>
    /// <param name="relatedIssueId">Идентификатор связанной задачи.</param>
    public static RelationIssue Create(Guid issueId, Guid relatedIssueId)
    {
        if (issueId == relatedIssueId)
        {
            throw new RelatedIssueException("Нельзя связать задачу с самой собой.");
        }
        
        return new RelationIssue(issueId, relatedIssueId);
    }
}

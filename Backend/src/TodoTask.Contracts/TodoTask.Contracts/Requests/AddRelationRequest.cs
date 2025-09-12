namespace TodoTask.Contracts.Requests;

/// <summary>
/// Запрос на добавление связи между задачами.
/// </summary>
public class AddRelationRequest
{
    /// <summary>
    /// Связь с другой задачей.
    /// </summary>
    public Guid RelationId { get; set; }
}

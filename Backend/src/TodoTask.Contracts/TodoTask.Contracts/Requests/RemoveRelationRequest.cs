namespace TodoTask.Contracts.Requests;

/// <summary>
/// Запрос на удаление связи между задачами.
/// </summary>
public class RemoveRelationRequest
{
    /// <summary>
    /// Идентификатор задачи.
    /// </summary>
    public Guid IssueId { get; set; }

    /// <summary>
    /// Идентификатор связанной задачи для удаления связи.
    /// </summary>
    public Guid RelatedIssueId { get; set; }
}

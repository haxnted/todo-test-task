namespace TodoTask.Contracts.Requests;

/// <summary>
/// Запрос на удаление исполнителя у задачи.
/// </summary>
public class RemoveExecutorRequest
{
    /// <summary>
    /// Идентификатор связанной задачи/исполнителя.
    /// </summary>
    public Guid RelatedIssueId { get; set; }
}

namespace TodoTask.Contracts.Requests;

/// <summary>
/// Запрос на получение связанных задач.
/// </summary>
public class GetRelatedIssuesRequest
{
    /// <summary>
    /// Идентификатор задачи.
    /// </summary>
    public Guid IssueId { get; set; }
}

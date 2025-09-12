namespace TodoTask.Contracts.Requests;

/// <summary>
/// Запрос на получение списка подзадач.
/// </summary>
public class GetSubIssuesRequest
{
    /// <summary>
    /// Идентификатор задачи.
    /// </summary>
    public Guid IssueId { get; set; }
}
namespace TodoTask.Contracts.Requests;

/// <summary>
/// Запрос на удаление задачи.
/// </summary>
public class DeleteIssueRequest
{
    /// <summary>
    /// Идентификатор задачи.
    /// </summary>
    public Guid IssueId { get; set; }
}

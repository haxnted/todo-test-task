namespace TodoTask.Contracts.Requests;

/// <summary>
/// Запрос на получение полного снимка задачи.
/// </summary>
public class GetIssueRequest
{
    /// <summary>
    /// Идентификатор задачи.
    /// </summary>
    public Guid IssueId { get; set; }
}

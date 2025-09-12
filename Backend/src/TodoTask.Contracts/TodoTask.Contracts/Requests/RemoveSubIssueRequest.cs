namespace TodoTask.Contracts.Requests;

/// <summary>
/// Запрос на удаление подзадачи.
/// </summary>
public class RemoveSubIssueRequest
{
    /// <summary>
    /// Идентификатор родительской задачи.
    /// </summary>
    public Guid IssueId { get; set; }

    /// <summary>
    /// Идентификатор подзадачи для удаления.
    /// </summary>
    public Guid SubIssueId { get; set; }
}

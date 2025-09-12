namespace TodoTask.Contracts.Requests;

/// <summary>
/// Запрос на добавление подзадачи.
/// </summary>
public class AddSubIssueRequest
{
    /// <summary>
    /// Подзадача, которая будет добавлена.
    /// </summary>
    public Guid SubIssueId { get; set; }
}

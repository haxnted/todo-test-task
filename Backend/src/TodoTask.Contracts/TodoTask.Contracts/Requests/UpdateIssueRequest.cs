namespace TodoTask.Contracts.Requests;

/// <summary>
/// Запрос на обновление задачи.
/// </summary>
public class UpdateIssueRequest
{
    /// <summary>
    /// Название задачи.
    /// </summary>
    public string Title { get; set; } = null!;
    
    /// <summary>
    /// Описание задачи.
    /// </summary>
    public string Description { get; set; } = null!;
    
    /// <summary>
    /// Статус задачи.
    /// </summary>
    public IssueStatusRequest Status { get; set; }
    
    /// <summary>
    /// Приоритет задачи.
    /// </summary>
    public IssuePriorityRequest Priority { get; set; }
}

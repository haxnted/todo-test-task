namespace TodoTask.Contracts.Requests;

/// <summary>
/// Запрос на создание задачи.
/// </summary>
public class CreateIssueRequest
{
    /// <summary>
    /// Идентификатор пользователя, создавшего задачу.
    /// </summary>
    public Guid UserId { get; set; }

    /// <summary>
    /// Статус задачи.
    /// </summary>
    public IssueStatusRequest Status { get; set; }

    /// <summary>
    /// Приоритет задачи.
    /// </summary>
    public IssuePriorityRequest Priority { get; set; }

    /// <summary>
    /// Идентификатор исполнителя задачи.
    /// </summary>
    public Guid? ExecutorId { get; set; }

    /// <summary>
    /// Название задачи.
    /// </summary>
    public string Title { get; set; } = string.Empty;

    /// <summary>
    /// Описание задачи.
    /// </summary>
    public string Description { get; set; } = string.Empty;
}
namespace TodoTask.Contracts.Requests;

/// <summary>
/// Запрос на назначение исполнителя задаче.
/// </summary>
public class AssignExecutorRequest
{
    /// <summary>
    /// Идентификатор исполнителя.
    /// </summary>
    public Guid ExecutorId { get; set; }
}

namespace TodoTask.Contracts.Requests;

/// <summary>
/// Представляет возможные приоритеты задачи в контракте запроса.
/// Используется в API/DTO для согласования внешнего слоя с внутренней логикой.
/// </summary>
/// <remarks>
/// Отличается от доменного <see cref="TodoTask.Domain.Enums.IssuePriority"/>,
/// чтобы избежать прямой зависимости контрактов от доменной модели.
/// </remarks>
public enum IssuePriorityRequest
{
    /// <summary>
    /// Задача имеет низкий приоритет.
    /// </summary>
    Low,

    /// <summary>
    /// Задача имеет средний приоритет.
    /// </summary>
    Medium,

    /// <summary>
    /// Задача имеет высокий приоритет.
    /// </summary>
    High
}

namespace TodoTask.Contracts.Requests;

/// <summary>
/// Представляет возможные статусы задачи в контракте запроса.
/// Используется в API/DTO для согласования внешнего слоя с внутренней логикой.
/// </summary>
/// <remarks>
/// Отличается от доменного <see cref="TodoTask.Domain.Enums.IssueStatus"/>,
/// чтобы избежать прямой зависимости контрактов от доменной модели.
/// </remarks>
public enum IssueStatusRequest
{
    /// <summary>
    /// Новая задача, только что создана.
    /// </summary>
    New,

    /// <summary>
    /// Задача находится в процессе выполнения.
    /// </summary>
    InProgress,

    /// <summary>
    /// Задача выполнена и закрыта.
    /// </summary>
    Done
}

namespace TodoTask.Domain.Exceptions;

/// <summary>
/// Исключение связанное с задачами.
/// </summary>
public class IssueException(string message) : Exception(message);
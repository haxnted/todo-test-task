namespace TodoTask.Domain.Exceptions;

/// <summary>
/// Исключение связанное со связанными задачами.
/// </summary>
public class RelationIssueException(string message) : IssueException(message);

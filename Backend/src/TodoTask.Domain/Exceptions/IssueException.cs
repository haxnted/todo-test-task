namespace TodoTask.Domain.Exceptions;

/// <summary>
/// Исключение связанное с задачами.
/// </summary>
public class IssueException : Exception
{
    public IssueException(string message) : base(message) { }
}

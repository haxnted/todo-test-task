namespace TodoTask.Domain.Exceptions;

/// <summary>
/// Исключение связанное со связанными задачами.
/// </summary>
public class RelationIssueException : IssueException
{
    public RelationIssueException(string message) : base(message)
    {
    }
}

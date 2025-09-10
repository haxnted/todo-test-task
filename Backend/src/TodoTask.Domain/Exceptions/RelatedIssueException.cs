namespace TodoTask.Domain.Exceptions;

/// <summary>
/// Исключение связанное со связанными задачами.
/// </summary>
public class RelatedIssueException : IssueException
{
    public RelatedIssueException(string message) : base(message)
    {
    }
}

using TodoTask.Application.AppServices.Abstractions;

namespace TodoTask.Application.Handlers.Issues.Commands.RemoveExecutor;

/// <summary>
/// Обработчик команды <see cref="RemoveExecutorCommand"/>.
/// </summary>
public class RemoveExecutorHandler
{
    private readonly IIssueService _issueService;

    public RemoveExecutorHandler(IIssueService issueService)
    {
        _issueService = issueService;
    }

    public async Task Handle(RemoveExecutorCommand request, CancellationToken cancellationToken)
    {
        await _issueService.RemoveExecutorAsync(request.IssueId, cancellationToken);
    }
}

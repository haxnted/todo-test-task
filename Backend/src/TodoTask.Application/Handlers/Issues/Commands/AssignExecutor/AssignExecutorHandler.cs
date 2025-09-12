using TodoTask.Application.AppServices.Abstractions;

namespace TodoTask.Application.Handlers.Issues.Commands.AssignExecutor;

/// <summary>
/// Обработчик команды <see cref="AssignExecutorCommand"/>.
/// </summary>
public class AssignExecutorHandler
{
    private readonly IIssueService _issueService;

    public AssignExecutorHandler(IIssueService issueService)
    {
        _issueService = issueService;
    }

    public async Task Handle(AssignExecutorCommand request, CancellationToken cancellationToken)
    {
        await _issueService.AssignExecutorAsync(request.IssueId, request.ExecutorId, cancellationToken);
    }
}
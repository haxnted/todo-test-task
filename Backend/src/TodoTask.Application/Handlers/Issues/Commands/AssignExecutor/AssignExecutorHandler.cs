using TodoTask.Application.AppServices.Abstractions;

namespace TodoTask.Application.Handlers.Issues.Commands.AssignExecutor;

/// <summary>
/// Обработчик команды <see cref="AssignExecutorCommand"/>.
/// </summary>
public class AssignExecutorHandler(IIssueService IssueService)
{
    public async Task Handle(AssignExecutorCommand request, CancellationToken cancellationToken)
    {
        await IssueService.AssignExecutorAsync(request.IssueId, request.ExecutorId, cancellationToken);
    }
}
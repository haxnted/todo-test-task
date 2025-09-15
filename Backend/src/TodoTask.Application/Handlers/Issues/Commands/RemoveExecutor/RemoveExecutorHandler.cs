using TodoTask.Application.AppServices.Abstractions;

namespace TodoTask.Application.Handlers.Issues.Commands.RemoveExecutor;

/// <summary>
/// Обработчик команды <see cref="RemoveExecutorCommand"/>.
/// </summary>
public class RemoveExecutorHandler(IIssueService IssueService)
{
    public async Task Handle(RemoveExecutorCommand request, CancellationToken cancellationToken)
    {
        await IssueService.RemoveExecutorAsync(request.IssueId, cancellationToken);
    }
}
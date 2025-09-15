using TodoTask.Application.AppServices.Abstractions;

namespace TodoTask.Application.Handlers.Issues.Commands.DeleteIssue;

/// <summary>
/// Обработчик команды <see cref="DeleteIssueCommand"/>.
/// </summary>
public class DeleteIssueHandler(IIssueService IssueService)
{
    public async Task Handle(DeleteIssueCommand request, CancellationToken cancellationToken)
    {
        await IssueService.DeleteIssueAsync(request.IssueId, cancellationToken);
    }
}
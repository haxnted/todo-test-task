using TodoTask.Application.AppServices.Abstractions;

namespace TodoTask.Application.Handlers.Issues.Commands.RemoveSubIssue;

/// <summary>
/// Обработчик команды <see cref="RemoveSubIssueCommand"/>.
/// </summary>
public class RemoveSubIssueHandler(IIssueService IssueService)
{
    public async Task Handle(RemoveSubIssueCommand request, CancellationToken cancellationToken)
    {
        await IssueService.RemoveSubIssueAsync(request.IssueId, request.SubIssueId, cancellationToken);
    }
}
using TodoTask.Application.AppServices.Abstractions;

namespace TodoTask.Application.Handlers.Issues.Commands.AddSubIssue;

/// <summary>
/// Обработчик команды <see cref="AddSubIssueCommand"/>.
/// </summary>
public class AddSubIssueHandler(IIssueService IssueService)
{
    public async Task Handle(AddSubIssueCommand request, CancellationToken cancellationToken)
    {
        await IssueService.AddSubIssueAsync(request.IssueId, request.SubIssueId, cancellationToken);
    }
}
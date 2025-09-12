using TodoTask.Application.AppServices.Abstractions;

namespace TodoTask.Application.Handlers.Issues.Commands.RemoveSubIssue;

/// <summary>
/// Обработчик команды <see cref="RemoveSubIssueCommand"/>.
/// </summary>
public class RemoveSubIssueHandler 
{
    private readonly IIssueService _issueService;

    public RemoveSubIssueHandler(IIssueService issueService)
    {
        _issueService = issueService;
    }

    public async Task Handle(RemoveSubIssueCommand request, CancellationToken cancellationToken)
    {
        await _issueService.RemoveSubIssueAsync(request.IssueId, request.SubIssueId, cancellationToken);
    }
}
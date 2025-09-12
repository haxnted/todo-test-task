using TodoTask.Application.AppServices.Abstractions;

namespace TodoTask.Application.Handlers.Issues.Commands.AddSubIssue;

/// <summary>
/// Обработчик команды <see cref="AddSubIssueCommand"/>.
/// </summary>
public class AddSubIssueHandler
{
    private readonly IIssueService _issueService;

    public AddSubIssueHandler(IIssueService issueService)
    {
        _issueService = issueService;
    }

    public async Task Handle(AddSubIssueCommand request, CancellationToken cancellationToken)
    {
        await _issueService.AddSubIssueAsync(request.IssueId, request.SubIssueId, cancellationToken);
    }
}
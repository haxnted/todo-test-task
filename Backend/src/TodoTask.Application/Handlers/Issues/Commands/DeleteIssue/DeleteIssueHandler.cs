using TodoTask.Application.AppServices.Abstractions;

namespace TodoTask.Application.Handlers.Issues.Commands.DeleteIssue;

/// <summary>
/// Обработчик команды <see cref="DeleteIssueCommand"/>.
/// </summary>
public class DeleteIssueHandler
{
    private readonly IIssueService _issueService;

    public DeleteIssueHandler(IIssueService issueService)
    {
        _issueService = issueService;
    }

    public async Task Handle(DeleteIssueCommand request, CancellationToken cancellationToken)
    {
        await _issueService.DeleteIssueAsync(request.IssueId, cancellationToken);
    }
}
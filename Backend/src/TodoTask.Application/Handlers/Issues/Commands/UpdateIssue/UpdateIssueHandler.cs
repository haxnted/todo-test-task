using TodoTask.Application.AppServices.Abstractions;

namespace TodoTask.Application.Handlers.Issues.Commands.UpdateIssue;

/// <summary>
/// Обработчик команды <see cref="UpdateIssueCommand"/>.
/// </summary>
public class UpdateIssueHandler
{
    private readonly IIssueService _issueService;

    public UpdateIssueHandler(IIssueService issueService)
    {
        _issueService = issueService;
    }

    public async Task Handle(UpdateIssueCommand request, CancellationToken cancellationToken)
    {
        await _issueService.UpdateIssueAsync(request.IssueId,
            request.Title,
            request.Description,
            request.Status,
            request.Priority,
            cancellationToken);
    }
}
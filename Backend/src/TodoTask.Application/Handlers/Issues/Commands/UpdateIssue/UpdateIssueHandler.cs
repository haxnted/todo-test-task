using TodoTask.Application.AppServices.Abstractions;

namespace TodoTask.Application.Handlers.Issues.Commands.UpdateIssue;

/// <summary>
/// Обработчик команды <see cref="UpdateIssueCommand"/>.
/// </summary>
public class UpdateIssueHandler(IIssueService IssueService)
{
    public async Task Handle(UpdateIssueCommand request, CancellationToken cancellationToken)
    {
        await IssueService.UpdateIssueAsync(request.IssueId,
            request.Title,
            request.Description,
            request.Status,
            request.Priority,
            cancellationToken);
    }
}
using TodoTask.Application.AppServices.Abstractions;

namespace TodoTask.Application.Handlers.Issues.Commands.CreateIssue;

/// <summary>
/// Обработчик команды <see cref="CreateIssueCommand"/>.
/// Добавляет задачу.
/// </summary>
public class CreateIssueHandler
{
    private readonly IIssueService _issueService;

    public CreateIssueHandler(IIssueService issueService)
    {
        _issueService = issueService;
    }

    public async Task Handle(CreateIssueCommand request, CancellationToken cancellationToken)
    {
        var issueId = Guid.NewGuid();

        await _issueService.CreateIssueAsync(issueId,
            request.UserId,
            request.Status,
            request.Priority,
            request.ExecutorId,
            request.Title,
            request.Description,
            cancellationToken);
    }
}
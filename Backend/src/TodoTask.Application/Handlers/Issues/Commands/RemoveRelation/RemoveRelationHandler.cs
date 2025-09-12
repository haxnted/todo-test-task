using TodoTask.Application.AppServices.Abstractions;

namespace TodoTask.Application.Handlers.Issues.Commands.RemoveRelation;

/// <summary>
/// Обработчик команды <see cref="RemoveRelationCommand"/>.
/// </summary>
public class RemoveRelationHandler 
{
    private readonly IIssueService _issueService;

    public RemoveRelationHandler(IIssueService issueService)
    {
        _issueService = issueService;
    }

    public async Task Handle(RemoveRelationCommand request, CancellationToken cancellationToken)
    {
        await _issueService.RemoveRelationAsync(request.IssueId, request.RelatedIssueId, cancellationToken);
    }
}
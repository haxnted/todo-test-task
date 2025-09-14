using TodoTask.Application.AppServices.Abstractions;

namespace TodoTask.Application.Handlers.Issues.Commands.RemoveRelation;

/// <summary>
/// Обработчик команды <see cref="RemoveRelationCommand"/>.
/// </summary>
public class RemoveRelationHandler(IIssueService IssueService)
{
    public async Task Handle(RemoveRelationCommand request, CancellationToken cancellationToken)
    {
        await IssueService.RemoveRelationAsync(request.IssueId, request.RelatedIssueId, cancellationToken);
    }
}
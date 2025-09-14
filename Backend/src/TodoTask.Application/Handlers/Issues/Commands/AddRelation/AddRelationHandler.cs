using TodoTask.Application.AppServices.Abstractions;

namespace TodoTask.Application.Handlers.Issues.Commands.AddRelation;

/// <summary>
/// Обработчик команды <see cref="AddRelationCommand"/>.
/// </summary>
public class AddRelationHandler(IIssueService IssueService)
{
    public async Task Handle(AddRelationCommand request, CancellationToken cancellationToken)
    {
        await IssueService.AddRelationAsync(request.IssueId, request.RelationIssueId, cancellationToken);
    }
}
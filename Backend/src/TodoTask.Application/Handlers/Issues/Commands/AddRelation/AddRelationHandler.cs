using TodoTask.Application.AppServices.Abstractions;

namespace TodoTask.Application.Handlers.Issues.Commands.AddRelation;

/// <summary>
/// Обработчик команды <see cref="AddRelationCommand"/>.
/// </summary>
public class AddRelationHandler
{
    private readonly IIssueService _issueService;

    public AddRelationHandler(IIssueService issueService)
    {
        _issueService = issueService;
    }

    public async Task Handle(AddRelationCommand request, CancellationToken cancellationToken)
    {
        await _issueService.AddRelationAsync(request.IssueId, request.RelationIssueId, cancellationToken);
    }
}
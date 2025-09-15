using Microsoft.AspNetCore.Mvc;
using TodoTask.Application.DTOs;
using TodoTask.Application.Handlers.Issues.Commands.AddRelation;
using TodoTask.Application.Handlers.Issues.Commands.AddSubIssue;
using TodoTask.Application.Handlers.Issues.Commands.AssignExecutor;
using TodoTask.Application.Handlers.Issues.Commands.CreateIssue;
using TodoTask.Application.Handlers.Issues.Commands.DeleteIssue;
using TodoTask.Application.Handlers.Issues.Commands.RemoveExecutor;
using TodoTask.Application.Handlers.Issues.Commands.RemoveRelation;
using TodoTask.Application.Handlers.Issues.Commands.RemoveSubIssue;
using TodoTask.Application.Handlers.Issues.Commands.UpdateIssue;
using TodoTask.Application.Handlers.Issues.Queries.GetIssue;
using TodoTask.Application.Handlers.Issues.Queries.GetRelatedIssues;
using TodoTask.Application.Handlers.Issues.Queries.GetSubIssues;
using TodoTask.Contracts.Requests;
using TodoTask.Domain.Enums;
using TodoTask.Presentation.Extensions;
using Wolverine;

namespace TodoTask.Presentation.Controllers;

/// <summary>
/// Контроллер управления задачами.
/// </summary>
[ApiController]
[Route("issues")]
public class IssuesController(IMessageBus MessageBus) : ControllerBase
{
    /// <summary>
    /// Создаёт новую задачу.
    /// </summary>
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> CreateIssueAsync(CreateIssueRequest request, CancellationToken cancellationToken)
    {
        var status = request.Status.ToDomain<IssueStatus>();
        var priority = request.Priority.ToDomain<IssuePriority>();

        var command = new CreateIssueCommand(request.UserId,
            status, priority,
            request.ExecutorId,
            request.Title,
            request.Description);

        await MessageBus.InvokeAsync(command, cancellationToken);

        return Ok();
    }

    /// <summary>
    /// Обновляет задачу.
    /// </summary>
    [HttpPatch("{issueId:guid}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> UpdateIssue(Guid issueId, UpdateIssueRequest request, CancellationToken cancellationToken)
    {
        var status = request.Status.ToDomain<IssueStatus>();
        var priority = request.Priority.ToDomain<IssuePriority>();

        var command = new UpdateIssueCommand(issueId,
            request.Title,
            request.Description,
            status,
            priority);

        await MessageBus.InvokeAsync(command, cancellationToken);

        return Ok();
    }

    /// <summary>
    /// Удаляет задачу.
    /// </summary>
    [HttpDelete("{issueId:guid}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> DeleteIssue(Guid issueId, CancellationToken cancellationToken)
    {
        var command = new DeleteIssueCommand(issueId);
        await MessageBus.InvokeAsync(command, cancellationToken);

        return Ok();
    }

    /// <summary>
    /// Назначает исполнителя задаче.
    /// </summary>
    [HttpPut("{issueId:guid}/assign")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> AssignExecutor(Guid issueId, AssignExecutorRequest request, CancellationToken cancellationToken)
    {
        var command = new AssignExecutorCommand(issueId,
            request.ExecutorId);

        await MessageBus.InvokeAsync(command, cancellationToken);

        return Ok();
    }

    /// <summary>
    /// Снимает исполнителя с задачи.
    /// </summary>
    [HttpDelete("{issueId:guid}/remove-executor")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> RemoveExecutor(Guid issueId, CancellationToken cancellationToken)
    {
        var command = new RemoveExecutorCommand(issueId);

        await MessageBus.InvokeAsync(command, cancellationToken);

        return Ok();
    }

    /// <summary>
    /// Добавляет подзадачу.
    /// </summary>
    [HttpPost("{issueId:guid}/sub-issues")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> AddSubIssue(Guid issueId, AddSubIssueRequest request, CancellationToken cancellationToken)
    {
        var command = new AddSubIssueCommand(issueId, request.SubIssueId);
        await MessageBus.InvokeAsync(command, cancellationToken);

        return Ok();
    }

    /// <summary>
    /// Удаляет подзадачу.
    /// </summary>
    [HttpDelete("{issueId:guid}/sub-issues/{subIssueId:guid}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> RemoveSubIssue(Guid issueId, Guid subIssueId, CancellationToken cancellationToken)
    {
        var command = new RemoveSubIssueCommand(issueId, subIssueId);
        await MessageBus.InvokeAsync(command, cancellationToken);

        return Ok();
    }

    /// <summary>
    /// Добавляет связь между задачами.
    /// </summary>
    [HttpPost("{issueId:guid}/relations")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> AddRelation(Guid issueId, AddRelationRequest request, CancellationToken cancellationToken)
    {
        var command = new AddRelationCommand(issueId, request.RelationId);
        await MessageBus.InvokeAsync(command, cancellationToken);

        return Ok();
    }

    /// <summary>
    /// Удаляет связь между задачами.
    /// </summary>
    [HttpDelete("{issueId:guid}/relations/{relatedIssueId:guid}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> RemoveRelation(Guid issueId, Guid relatedIssueId, CancellationToken cancellationToken)
    {
        var command = new RemoveRelationCommand(issueId, relatedIssueId);
        await MessageBus.InvokeAsync(command, cancellationToken);

        return Ok();
    }
    
    /// <summary>
    /// Получить задачу по идентификатору
    /// </summary>
    [HttpGet("{issueId:guid}")]
    public async Task<ActionResult<IssueDto>> GetIssue(Guid issueId, CancellationToken cancellationToken)
    {
        var query = new GetIssueQuery(issueId);
        
        var result = await MessageBus.InvokeAsync<IssueDto>(query, cancellationToken);

        return Ok(result);
    }

    /// <summary>
    /// Получить связанные задачи
    /// </summary>
    [HttpGet("{issueId:guid}/related")]
    public async Task<ActionResult<IReadOnlyCollection<RelationIssueDto>>> GetRelatedIssues(Guid issueId, CancellationToken cancellationToken)
    {
        var query = new GetRelatedIssuesQuery(issueId);
        var result = await MessageBus.InvokeAsync<IReadOnlyCollection<RelationIssueDto>>(query, cancellationToken);
        return Ok(result);
    }

    /// <summary>
    /// Получить подзадачи
    /// </summary>
    [HttpGet("{issueId:guid}/subissues")]
    public async Task<ActionResult<IReadOnlyCollection<SubIssueDto>>> GetSubIssues(Guid issueId, CancellationToken cancellationToken)
    {
        var query = new GetSubIssuesQuery(issueId);
        var result = await MessageBus.InvokeAsync<IReadOnlyCollection<SubIssueDto>>(query, cancellationToken);
        return Ok(result);
    }
}
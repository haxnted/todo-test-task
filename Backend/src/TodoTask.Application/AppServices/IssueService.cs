using Microsoft.Extensions.Logging;
using TodoTask.Application.AppServices.Abstractions;
using TodoTask.Application.Specifications;
using TodoTask.Domain.Aggregates;
using TodoTask.Domain.Entities;
using TodoTask.Domain.Enums;
using TodoTask.Domain.Exceptions;
using TodoTask.Domain.ValueObjects;
using TodoTask.GeneralKernel.Database.Abstracts;

namespace TodoTask.Application.AppServices;

/// <inheritdoc/>
public sealed class IssueService(
    IRepository<Issue> IssueRepository,
    IRepository<RelationIssue> RelationIssueRepository,
    ILogger<IssueService> Logger) : IIssueService
{
    /// <inheritdoc/>
    public async Task CreateIssueAsync(
        Guid issueId,
        Guid userId,
        IssueStatus status,
        IssuePriority priority,
        Guid? executorId,
        string title,
        string description,
        CancellationToken cancellationToken)
    {
        var issue = Issue.Create(issueId, userId, status, priority, executorId,
            title, description);

        await IssueRepository.AddAsync(issue, cancellationToken);
    }

    /// <inheritdoc/>
    public async Task UpdateIssueAsync(
        Guid issueId,
        string title,
        string description,
        IssueStatus status,
        IssuePriority priority,
        CancellationToken cancellationToken)
    {
        var issueSpecification = new IssueByIdSpecification(issueId);

        var issue = await IssueRepository.FirstOrDefaultAsync(issueSpecification, cancellationToken)
                    ?? throw new IssueException("Задача не найдена.");

        var newTitle = Title.Of(title);
        var newDescription = Description.Of(description);

        issue.UpdateGeneralInformation(newTitle, newDescription, priority, status);

        await IssueRepository.UpdateAsync(issue, cancellationToken);

        Logger.LogInformation("Задача {IssueId} успешно обновлена", issueId);
    }

    /// <inheritdoc/>
    public async Task AssignExecutorAsync(
        Guid issueId,
        Guid executorId,
        CancellationToken cancellationToken)
    {
        var issueSpecification = new IssueByIdSpecification(issueId);

        var issue = await IssueRepository.FirstOrDefaultAsync(issueSpecification, cancellationToken)
                    ?? throw new IssueException("Задача не найдена.");

        issue.UpdateExecutor(executorId);

        await IssueRepository.UpdateAsync(issue, cancellationToken);

        Logger.LogInformation("Исполнитель {ExecutorId} успешно назначен задаче {IssueId}", executorId, issueId);
    }

    /// <inheritdoc/>
    public async Task RemoveExecutorAsync(
        Guid issueId,
        CancellationToken cancellationToken)
    {
        var issueSpecification = new IssueByIdSpecification(issueId);

        var issue = await IssueRepository.FirstOrDefaultAsync(issueSpecification, cancellationToken)
                    ?? throw new IssueException("Задача не найдена.");

        issue.RemoveExecutor();

        await IssueRepository.UpdateAsync(issue, cancellationToken);

        Logger.LogInformation("Исполнитель успешно удалён у задачи {IssueId}", issueId);
    }

    /// <inheritdoc/>
    public async Task AddSubIssueAsync(
        Guid issueId,
        Guid subIssueId,
        CancellationToken cancellationToken)
    {
        var issueSpecification = new IssueWithDetailsSpecification(issueId);

        var issue = await IssueRepository.FirstOrDefaultAsync(issueSpecification, cancellationToken)
                    ?? throw new IssueException("Задача не найдена.");

        var subIssueSpecification = new IssueByIdSpecification(subIssueId);

        var subIssue = await IssueRepository.FirstOrDefaultAsync(subIssueSpecification, cancellationToken)
                       ?? throw new IssueException("Задача не найдена.");

        issue.AddSubIssue(subIssue);

        await IssueRepository.UpdateAsync(issue, cancellationToken);

        Logger.LogInformation("Подзадача {SubIssueId} успешно добавлена к задаче {IssueId}", subIssueId, issueId);
    }

    /// <inheritdoc/>
    public async Task RemoveSubIssueAsync(
        Guid issueId,
        Guid subIssueId,
        CancellationToken cancellationToken)
    {
        var issueSpecification = new IssueWithDetailsSpecification(issueId);

        var issue = await IssueRepository.FirstOrDefaultAsync(issueSpecification, cancellationToken)
                    ?? throw new IssueException("Задача не найдена.");

        var subIssueSpecification = new IssueByIdSpecification(subIssueId);

        var subIssue = await IssueRepository.FirstOrDefaultAsync(subIssueSpecification, cancellationToken)
                       ?? throw new IssueException("Задача не найдена.");

        issue.RemoveSubIssue(subIssue.Id);

        await IssueRepository.UpdateAsync(issue, cancellationToken);

        Logger.LogInformation("Подзадача {SubIssueId} успешно удалена из задачи {IssueId}", subIssueId, issueId);
    }

    /// <inheritdoc/>
    public async Task AddRelationAsync(
        Guid issueId,
        Guid relatedIssueId,
        CancellationToken cancellationToken)
    {
        var issueSpecification = new IssueWithDetailsSpecification(issueId);

        var issue = await IssueRepository.FirstOrDefaultAsync(issueSpecification, cancellationToken)
                    ?? throw new IssueException("Задача не найдена.");

        var relatedIssueSpecification = new IssueByIdSpecification(relatedIssueId);

        var relatedIssue = await IssueRepository.FirstOrDefaultAsync(relatedIssueSpecification, cancellationToken)
                           ?? throw new IssueException("Связанная задача не найдена.");

        if (issue.RelatedIssues.Any(r => r.RelatedId == relatedIssueId))
            throw new IssueException("Задачи уже связаны.");

        var relation = RelationIssue.Create(Guid.NewGuid(), issue.Id, relatedIssue.Id);

        issue.AddRelation(relation);

        await RelationIssueRepository.AddAsync(relation, cancellationToken);

        await IssueRepository.UpdateAsync(issue, cancellationToken);

        Logger.LogInformation("Связь между задачами {IssueId} и {RelatedIssueId} успешно добавлена", issueId, relatedIssueId);
    }

    /// <inheritdoc/>
    public async Task RemoveRelationAsync(
        Guid issueId,
        Guid relatedIssueId,
        CancellationToken cancellationToken)
    {
        var issueSpecification = new IssueWithDetailsSpecification(issueId);

        var issue = await IssueRepository.FirstOrDefaultAsync(issueSpecification, cancellationToken)
                    ?? throw new IssueException("Задача не найдена.");

        issue.RemoveRelation(relatedIssueId);

        await IssueRepository.UpdateAsync(issue, cancellationToken);

        Logger.LogInformation("Связь между задачами {IssueId} и {RelatedIssueId} успешно удалена", issueId, relatedIssueId);
    }

    /// <inheritdoc/>
    public async Task<Issue> GetIssueSnapshotAsync(
        Guid issueId,
        CancellationToken cancellationToken)
    {
        var issueSpecification = new IssueWithDetailsSpecification(issueId);

        var issue = await IssueRepository.FirstOrDefaultAsync(issueSpecification, cancellationToken)
                    ?? throw new IssueException("Задача не найдена.");

        return issue;
    }

    /// <inheritdoc/>
    public async Task<IReadOnlyList<Issue>> GetIssuesWithPaginationAsync(int pageIndex, int pageSize,CancellationToken cancellationToken)
    {
        var issueSpecification = new IssuesWithDetailsPaginatedSpecification(pageIndex, pageSize);

        return await IssueRepository.GetAll(issueSpecification, cancellationToken);
    }

    /// <inheritdoc/>
    public async Task DeleteIssueAsync(
        Guid issueId,
        CancellationToken cancellationToken)
    {
        var issueSpecification = new IssueWithDetailsSpecification(issueId);

        var issue = await IssueRepository.FirstOrDefaultAsync(issueSpecification, cancellationToken)
                    ?? throw new IssueException("Задача не найдена.");

        await IssueRepository.RemoveAsync(issue, cancellationToken);

        Logger.LogInformation("Задача {IssueId} успешно удалена", issueId);
    }
}
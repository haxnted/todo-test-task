using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Distributed;
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
public sealed class IssueService : IIssueService
{
    private readonly IRepository<Issue> _issueRepository;

    private readonly IRepository<RelationIssue> _relationIssueRepository;

    
    private static string CacheKey(Guid issueId) => $"issue:{issueId}";

    public IssueService(IRepository<Issue> issueRepository, 
                        IRepository<RelationIssue> relationIssueRepository)
    {
        _issueRepository = issueRepository;
        _relationIssueRepository = relationIssueRepository;
    }

    
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
        var issue = Issue.Create(issueId, userId, status, priority, executorId, title, description);

        await _issueRepository.AddAsync(issue, cancellationToken);
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

        var issue = await _issueRepository.WithSpecification(issueSpecification)
                        .FirstOrDefaultAsync(cancellationToken)
                    ?? throw new IssueException("Задача не найдена.");

        var newTitle = Title.Of(title);
        var newDescription = Description.Of(description);
        
        issue.UpdateGeneralInformation(newTitle, newDescription, priority, status);

        await _issueRepository.UpdateAsync(issue, cancellationToken);
    }

    /// <inheritdoc/>
    public async Task AssignExecutorAsync(
        Guid issueId,
        Guid executorId,
        CancellationToken cancellationToken)
    {
        var issueSpecification = new IssueByIdSpecification(issueId);

        var issue = await _issueRepository.WithSpecification(issueSpecification)
                        .FirstOrDefaultAsync(cancellationToken)
                    ?? throw new IssueException("Задача не найдена.");

        issue.UpdateExecutor(executorId);

        await _issueRepository.UpdateAsync(issue, cancellationToken);
    }

    /// <inheritdoc/>
    public async Task RemoveExecutorAsync(
        Guid issueId,
        CancellationToken cancellationToken)
    {
        var issueSpecification = new IssueByIdSpecification(issueId);

        var issue = await _issueRepository
                        .WithSpecification(issueSpecification)
                        .FirstOrDefaultAsync(cancellationToken)
                    ?? throw new IssueException("Задача не найдена.");

        issue.RemoveExecutor();

        await _issueRepository.UpdateAsync(issue, cancellationToken);
    }

    /// <inheritdoc/>
    public async Task AddSubIssueAsync(
        Guid issueId,
        Guid subIssueId,
        CancellationToken cancellationToken)
    {
        var issueSpecification = new IssueWithDetailsSpecification(issueId);

        var issue = await _issueRepository.WithSpecification(issueSpecification)
                        .FirstOrDefaultAsync(cancellationToken)
                    ?? throw new IssueException("Задача не найдена.");

        var subIssueSpecification = new IssueByIdSpecification(subIssueId);

        var subIssue = await _issueRepository.WithSpecification(subIssueSpecification)
                           .FirstOrDefaultAsync(cancellationToken)
                       ?? throw new IssueException("Подзадача не найдена.");

        issue.AddSubIssue(subIssue);

        await _issueRepository.UpdateAsync(issue, cancellationToken);
    }

    /// <inheritdoc/>
    public async Task RemoveSubIssueAsync(
        Guid issueId,
        Guid subIssueId,
        CancellationToken cancellationToken)
    {
        var issueSpecification = new IssueWithDetailsSpecification(issueId);

        var issue = await _issueRepository.WithSpecification(issueSpecification)
                        .FirstOrDefaultAsync(cancellationToken)
                    ?? throw new IssueException("Задача не найдена.");

        var subIssueSpecification = new IssueByIdSpecification(subIssueId);

        var subIssue = await _issueRepository.WithSpecification(subIssueSpecification)
                           .FirstOrDefaultAsync(cancellationToken)
                       ?? throw new IssueException("Подзадача не найдена.");

        issue.RemoveSubIssue(subIssue.Id);

        await _issueRepository.UpdateAsync(issue, cancellationToken);
    }

    /// <inheritdoc/>
    public async Task AddRelationAsync(
        Guid issueId,
        Guid relatedIssueId,
        CancellationToken cancellationToken)
    {
        var issue = await _issueRepository.WithSpecification(new IssueWithDetailsSpecification(issueId))
                        .FirstOrDefaultAsync(cancellationToken)
                    ?? throw new IssueException("Задача не найдена.");

        var relatedIssue = await _issueRepository.WithSpecification(new IssueByIdSpecification(relatedIssueId))
                               .FirstOrDefaultAsync(cancellationToken)
                           ?? throw new IssueException("Связанная задача не найдена.");

        if (issue.RelatedIssues.Any(r => r.RelatedId == relatedIssueId))
            throw new IssueException("Задачи уже связаны.");

        var relation = RelationIssue.Create(Guid.NewGuid(), issue.Id, relatedIssue.Id);
        relation.SetIssueNavigation(issue, relatedIssue);
        
        await _relationIssueRepository.AddAsync(relation, cancellationToken);
    }


    /// <inheritdoc/>
    public async Task RemoveRelationAsync(
        Guid issueId,
        Guid relatedIssueId,
        CancellationToken cancellationToken)
    {
        var issueSpecification = new IssueWithDetailsSpecification(issueId);

        var issue = await _issueRepository.WithSpecification(issueSpecification)
                        .FirstOrDefaultAsync(cancellationToken)
                    ?? throw new IssueException("Задача не найдена.");

        issue.RemoveRelation(relatedIssueId);

        await _issueRepository.UpdateAsync(issue, cancellationToken);
    }

    /// <inheritdoc/>
    public async Task<Issue> GetIssueSnapshotAsync(
        Guid issueId,
        CancellationToken cancellationToken)
    {
        var issueSpecification = new IssueWithDetailsSpecification(issueId);

        var issue = await _issueRepository.WithSpecification(issueSpecification)
                        .FirstOrDefaultAsync(cancellationToken)
                    ?? throw new IssueException("Задача не найдена.");

        return issue;
    }

    /// <inheritdoc/>
    public async Task DeleteIssueAsync(
        Guid issueId,
        CancellationToken cancellationToken)
    {
        var issueSpecification = new IssueWithDetailsSpecification(issueId);

        var issue = await _issueRepository.WithSpecification(issueSpecification)
                        .FirstOrDefaultAsync(cancellationToken)
                    ?? throw new IssueException("Задача не найдена.");

        await _issueRepository.RemoveAsync(issue, cancellationToken);
    }
}
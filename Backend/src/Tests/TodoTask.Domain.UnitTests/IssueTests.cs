using FluentAssertions;
using TodoTask.Domain.Aggregates;
using TodoTask.Domain.Entities;
using TodoTask.Domain.Enums;
using TodoTask.Domain.Exceptions;
using TodoTask.Domain.ValueObjects;

namespace TodoTask.Domain.Tests.Aggregates;

/// <summary>
/// Юнит-тесты для задач.
/// </summary>
public class IssueTests
{
    private static Issue CreateDefaultIssue()
    {
        return Issue.Create(Guid.NewGuid(),
            Guid.NewGuid(),
            IssueStatus.InProgress,
            IssuePriority.Medium,
            null,
            "Test Title",
            "Test Description");
    }

    [Fact]
    public void Create_ShouldInitializeCorrectly()
    {
        // Act
        var issue = CreateDefaultIssue();

        // Assert
        issue.Id.Should()
            .NotBeEmpty();

        issue.UserId.Should()
            .NotBeEmpty();

        issue.Status.Should()
            .Be(IssueStatus.InProgress);

        issue.Priority.Should()
            .Be(IssuePriority.Medium);

        issue.ExecutorId.Should()
            .BeNull();

        issue.Title.Value.Should()
            .Be("Test Title");

        issue.Description.Value.Should()
            .Be("Test Description");
    }

    [Fact]
    public void UpdateExecutor_ShouldUpdateExecutorId()
    {
        var issue = CreateDefaultIssue();
        var executorId = Guid.NewGuid();

        issue.UpdateExecutor(executorId);

        issue.ExecutorId.Should()
            .Be(executorId);

        issue.UpdatedAt.Should()
            .BeAfter(issue.CreatedAt);
    }

    [Fact]
    public void UpdateExecutor_ShouldThrow_WhenSameExecutor()
    {
        var issue = CreateDefaultIssue();
        var executorId = Guid.NewGuid();
        issue.UpdateExecutor(executorId);

        Action act = () => issue.UpdateExecutor(executorId);

        act.Should()
            .Throw<IssueException>()
            .WithMessage("Этот исполнитель уже является исполнителем этой задачи.");
    }

    [Fact]
    public void RemoveExecutor_ShouldRemoveExecutor()
    {
        var issue = CreateDefaultIssue();
        var executorId = Guid.NewGuid();
        issue.UpdateExecutor(executorId);

        issue.RemoveExecutor();

        issue.ExecutorId.Should()
            .BeNull();
    }

    [Fact]
    public void RemoveExecutor_ShouldThrow_WhenNoExecutor()
    {
        var issue = CreateDefaultIssue();

        Action act = () => issue.RemoveExecutor();

        act.Should()
            .Throw<IssueException>()
            .WithMessage("Эта задача не имеет исполнителя.");
    }

    [Fact]
    public void UpdateGeneralInformation_ShouldUpdateAllProperties()
    {
        var issue = CreateDefaultIssue();
        var newTitle = Title.Of("New Title");
        var newDescription = Description.Of("New Description");

        issue.UpdateGeneralInformation(newTitle, newDescription, IssuePriority.High, IssueStatus.InProgress);

        issue.Title.Should()
            .Be(newTitle);

        issue.Description.Should()
            .Be(newDescription);

        issue.Priority.Should()
            .Be(IssuePriority.High);

        issue.Status.Should()
            .Be(IssueStatus.InProgress);
    }

    [Fact]
    public void ChangePriority_ShouldUpdatePriority()
    {
        var issue = CreateDefaultIssue();

        issue.ChangePriority(IssuePriority.Low);

        issue.Priority.Should()
            .Be(IssuePriority.Low);
    }

    [Fact]
    public void ChangeStatus_ShouldUpdateStatus()
    {
        var issue = CreateDefaultIssue();

        issue.ChangeStatus(IssueStatus.Done);

        issue.Status.Should()
            .Be(IssueStatus.Done);
    }

    [Fact]
    public void RemoveRelation_ShouldThrow_WhenNotFound()
    {
        var issue = CreateDefaultIssue();

        Action act = () => issue.RemoveRelation(Guid.NewGuid());

        act.Should()
            .Throw<IssueException>()
            .WithMessage("Связь не найдена.");
    }

    [Fact]
    public void AddSubIssue_ShouldAddCorrectly()
    {
        var issue = CreateDefaultIssue();
        var subIssue = CreateDefaultIssue();

        issue.AddSubIssue(subIssue);

        issue.SubIssues.Should()
            .ContainSingle(s => s.Id == subIssue.Id);

        subIssue.ParentIssueId.Should()
            .Be(issue.Id);
    }

    [Fact]
    public void AddSubIssue_ShouldThrow_WhenAlreadyExists()
    {
        var issue = CreateDefaultIssue();
        var subIssue = CreateDefaultIssue();
        issue.AddSubIssue(subIssue);

        Action act = () => issue.AddSubIssue(subIssue);

        act.Should()
            .Throw<IssueException>()
            .WithMessage("Задача уже является подзадачей.");
    }

    [Fact]
    public void AddRelation_ShouldAddSuccessfully()
    {
        var issue = CreateDefaultIssue();
        var relatedIssueId = Guid.NewGuid();

        var relation = RelationIssue.Create(Guid.NewGuid(), issue.Id, relatedIssueId);

        issue.AddRelation(relation);

        issue.RelatedIssues.Should()
            .ContainSingle(r => r.RelatedId == relatedIssueId);

        issue.UpdatedAt.Should()
            .BeAfter(issue.CreatedAt);
    }

    [Fact]
    public void AddRelation_ShouldThrow_WhenRelationAlreadyExists()
    {
        var issue = CreateDefaultIssue();
        var relatedIssueId = Guid.NewGuid();

        var relation = RelationIssue.Create(Guid.NewGuid(), issue.Id, relatedIssueId);

        issue.AddRelation(relation);

        Action act = () => issue.AddRelation(relation);

        act.Should()
            .Throw<IssueException>()
            .WithMessage("Связь уже существует.");
    }

    [Fact]
    public void RemoveRelation_ShouldRemoveSuccessfully()
    {
        var issue = CreateDefaultIssue();
        var relatedIssueId = Guid.NewGuid();

        var relation = RelationIssue.Create(Guid.NewGuid(), issue.Id, relatedIssueId);

        issue.AddRelation(relation);

        issue.RemoveRelation(relatedIssueId);

        issue.RelatedIssues.Should()
            .BeEmpty();

        issue.UpdatedAt.Should()
            .BeAfter(issue.CreatedAt);
    }

    [Fact]
    public void RemoveSubIssue_ShouldRemoveCorrectly()
    {
        var issue = CreateDefaultIssue();
        var subIssue = CreateDefaultIssue();
        issue.AddSubIssue(subIssue);

        issue.RemoveSubIssue(subIssue.Id);

        issue.SubIssues.Should()
            .BeEmpty();

        subIssue.ParentIssueId.Should()
            .BeNull();
    }

    [Fact]
    public void RemoveSubIssue_ShouldThrow_WhenNotExists()
    {
        var issue = CreateDefaultIssue();

        Action act = () => issue.RemoveSubIssue(Guid.NewGuid());

        act.Should()
            .Throw<IssueException>()
            .WithMessage("Такая подзадача не существует.");
    }
}
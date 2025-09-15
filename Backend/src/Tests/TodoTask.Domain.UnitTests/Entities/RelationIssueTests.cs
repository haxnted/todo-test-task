using FluentAssertions;
using TodoTask.Domain.Entities;
using TodoTask.Domain.Exceptions;

namespace TodoTask.Domain.Tests.Aggregates.Entities;

public class RelationIssueTests
{
    [Fact]
    public void Create_ShouldReturnValidRelationIssue()
    {
        // Arrange
        var id = Guid.NewGuid();
        var issueId = Guid.NewGuid();
        var relatedId = Guid.NewGuid();

        // Act
        var relation = RelationIssue.Create(id, issueId, relatedId);

        // Assert
        relation.Should().NotBeNull();
        relation.Id.Should().Be(id);
        relation.IssueId.Should().Be(issueId);
        relation.RelatedId.Should().Be(relatedId);
        relation.Issue.Should().BeNull();
        relation.RelatedIssue.Should().BeNull();
    }

    [Fact]
    public void Create_ShouldThrowException_WhenIssueIdEqualsRelatedId()
    {
        // Arrange
        var id = Guid.NewGuid();
        var issueId = Guid.NewGuid();

        // Act
        Action act = () => RelationIssue.Create(id, issueId, issueId);

        // Assert
        act.Should()
            .Throw<RelationIssueException>()
            .WithMessage("Нельзя связать задачу с самой собой.");
    }
}
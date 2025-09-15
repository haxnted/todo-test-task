using FluentAssertions;
using TodoTask.Domain.Exceptions;
using TodoTask.Domain.ValueObjects;

namespace TodoTask.Domain.Tests.Aggregates.ValueObjects;

public class TitleTests
{
    public static IEnumerable<object[]> ValidTitles => new List<object[]>
    {
        new object[] { "Valid Title" },
        new object[] { "12345" },
        new object[] { string.Concat(Enumerable.Repeat("x", 200)) } 
    };

    public static IEnumerable<object[]> InvalidTitles => new List<object[]>
    {
        new object[] { "" },
        new object[] { "    " },
        new object[] { "1234" }, 
        new object[] { string.Concat(Enumerable.Repeat("x", 201)) } 
    };

    [Theory]
    [MemberData(nameof(ValidTitles))]
    public void Of_ShouldCreateTitle_WhenValid(string value)
    {
        var title = Title.Of(value);
        title.Should().NotBeNull();
        title.Value.Should().Be(value);
    }

    [Theory]
    [MemberData(nameof(InvalidTitles))]
    public void Of_ShouldThrowIssueException_WhenInvalid(string value)
    {
        Action act = () => Title.Of(value);
        act.Should().Throw<IssueException>();
    }

    [Fact]
    public void Equals_ShouldReturnTrue_ForSameValue()
    {
        var title1 = Title.Of("Same Title");
        var title2 = Title.Of("Same Title");

        title1.Equals(title2).Should().BeTrue();
    }

    [Fact]
    public void Equals_ShouldReturnFalse_ForNullOrDifferentType()
    {
        var title = Title.Of("Test Title");

        title.Equals(null).Should().BeFalse();
        title.Equals("Test Title").Should().BeFalse();
    }

    [Fact]
    public void Equals_ShouldReturnFalse_ForDifferentValue()
    {
        var title1 = Title.Of("Title One");
        var title2 = Title.Of("Title Two");

        title1.Equals(title2).Should().BeFalse();
    }

    [Fact]
    public void CompareTo_ShouldReturnCorrectOrder()
    {
        var title1 = Title.Of("AAAAA"); 
        var title2 = Title.Of("BBBBB");

        title1.CompareTo(title2).Should().BeNegative();
        title2.CompareTo(title1).Should().BePositive();
        title1.CompareTo(title1).Should().Be(0);
    }
        
    [Fact]
    public void GetHashCode_ShouldBeSame_ForSameValue()
    {
        var title1 = Title.Of("Test Title");
        var title2 = Title.Of("Test Title");

        title1.GetHashCode().Should().Be(title2.GetHashCode());
    }

    [Fact]
    public void GetHashCode_ShouldBeDifferent_ForDifferentValue()
    {
        var title1 = Title.Of("Title One");
        var title2 = Title.Of("Title Two");

        title1.GetHashCode().Should().NotBe(title2.GetHashCode());
    }

    [Fact]
    public void CompareTo_ShouldReturnPositive_WhenOtherIsNull()
    {
        var title = Title.Of("Non-null Title");

        title.CompareTo(null).Should().BePositive();
    }
}
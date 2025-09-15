using FluentAssertions;
using TodoTask.Domain.Exceptions;
using TodoTask.Domain.ValueObjects;

namespace TodoTask.Domain.Tests.Aggregates.ValueObjects;

    public class DescriptionTests
    {
        public static IEnumerable<object[]> ValidDescriptions => new List<object[]>
        {
            new object[] { "Valid description" },
            new object[] { "12345" }, 
            new object[] { string.Concat(Enumerable.Repeat("x", 700)) } 
        };

        public static IEnumerable<object[]> InvalidDescriptions => new List<object[]>
        {
            new object[] { "" },
            new object[] { "    " },
            new object[] { "1234" }, 
            new object[] { string.Concat(Enumerable.Repeat("x", 701)) } 
        };

        [Theory]
        [MemberData(nameof(ValidDescriptions))]
        public void Of_ShouldCreateDescription_WhenValid(string value)
        {
            var description = Description.Of(value);
            description.Should().NotBeNull();
            description.Value.Should().Be(value);
        }

        [Theory]
        [MemberData(nameof(InvalidDescriptions))]
        public void Of_ShouldThrowIssueException_WhenInvalid(string value)
        {
            Action act = () => Description.Of(value);
            act.Should().Throw<IssueException>();
        }

        [Fact]
        public void Equals_ShouldReturnTrue_ForSameValue()
        {
            var desc1 = Description.Of("Same Description");
            var desc2 = Description.Of("Same Description");

            desc1.Equals(desc2).Should().BeTrue();
        }

        [Fact]
        public void Equals_ShouldReturnFalse_ForNullOrDifferentType()
        {
            var desc = Description.Of("Test Description");

            desc.Equals(null).Should().BeFalse();
            desc.Equals("Test Description").Should().BeFalse();
        }

        [Fact]
        public void Equals_ShouldReturnFalse_ForDifferentValue()
        {
            var desc1 = Description.Of("Description One");
            var desc2 = Description.Of("Description Two");

            desc1.Equals(desc2).Should().BeFalse();
        }

        [Fact]
        public void CompareTo_ShouldReturnCorrectOrder()
        {
            var desc1 = Description.Of("AAAAA"); 
            var desc2 = Description.Of("BBBBB");

            desc1.CompareTo(desc2).Should().BeNegative();
            desc2.CompareTo(desc1).Should().BePositive();
            desc1.CompareTo(desc1).Should().Be(0);
        }
        
        [Fact]
        public void GetHashCode_ShouldBeSame_ForSameValue()
        {
            var desc1 = Description.Of("Test Description");
            var desc2 = Description.Of("Test Description");

            desc1.GetHashCode().Should().Be(desc2.GetHashCode());
        }

        [Fact]
        public void GetHashCode_ShouldBeDifferent_ForDifferentValue()
        {
            var desc1 = Description.Of("Description One");
            var desc2 = Description.Of("Description Two");

            desc1.GetHashCode().Should().NotBe(desc2.GetHashCode());
        }

        [Fact]
        public void CompareTo_ShouldReturnPositive_WhenOtherIsNull()
        {
            var desc = Description.Of("Non-null Description");

            desc.CompareTo(null).Should().BePositive();
        }
    }
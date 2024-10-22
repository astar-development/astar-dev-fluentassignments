namespace AStar.Dev.FluentAssignments.Unit.Tests;

public class FluentAssignmentsExtensionsShould
{
    private readonly int value = 10;

    [Fact]
    public void AssignTheValueWhenTheCriteriaIsMatched()
    {
        var sut = new AnyClass
        {
            Id = 10.WillBeSet().IfItIs().NotNull().And().ItIsGreaterThan(5).And().ItIsLessThan(11)
        };

        _ = sut.Id.Should().Be(10);
    }

    [Theory]
    [InlineData(10)]
    [InlineData(-10)]
    [InlineData(0)]
    public void ReturnTheSameValueFromWillBeSetWhetherNegativePositiveOrZero(int value) => value.WillBeSet().Should().Be(value);

    [Theory]
    [InlineData(10)]
    [InlineData(-10)]
    [InlineData(0)]
    public void ReturnTheSameValueFromIfItIsWhetherNegativePositiveOrZero(int value) => value.IfItIs().Should().Be(value);

    [Fact]
    public void ThrowExceptionWhenNotNullIsCalledOnNullValue()
    {
        int? nullValue = null;

        Action comparison = () => nullValue.NotNull();

        _ = comparison.Should().Throw<ArgumentException>();
    }

    [Theory]
    [InlineData(10)]
    [InlineData(-10)]
    [InlineData(0)]
    public void ReturnTheSameValueFromTheAndExtensionWhetherNegativePositiveOrZero(int value) => value.And().Should().Be(value);

    [Theory]
    [InlineData(10)]
    [InlineData(-10)]
    [InlineData(0)]
    public void ReturnTheSameValueFromItIsGreaterThanWhenGreaterThanTheSpecifiedValue(int value) => value.ItIsGreaterThan(-11).Should().Be(value);

    [Fact]
    public void ThrowExceptionWhenItIsGreaterThanIsCalledWithValueLessThanSpecifiedMinimum()
    {
        Action comparison = () => value.ItIsGreaterThan(value+1);

        _ = comparison.Should().Throw<ArgumentException>();
    }

    [Theory]
    [InlineData(10)]
    [InlineData(-10)]
    [InlineData(0)]
    public void ReturnTheSameValueFromItIsLessThanWhenLessThanTheSpecifiedValue(int value) => value.ItIsLessThan(value + 1).Should().Be(value);

    [Fact]
    public void ThrowExceptionWhenItIsLessThanIsCalledWithValueLessThanSpecifiedMinimum()
    {
        Action comparison = () => value.ItIsLessThan(value-1);

        _ = comparison.Should().Throw<ArgumentException>();
    }

    private class AnyClass
    {
        public int Id { get; set; }
    }
}

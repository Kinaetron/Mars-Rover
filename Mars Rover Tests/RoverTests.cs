using System;
using Xunit;
using Mars_Rover;
using FluentAssertions;

namespace Mars_Rover_Tests
{
    public class RoverTests
    {
        private class Arrangement
        {
            public Rover SUT { get; }

            public Arrangement(int xPos, int yPos)
            {
                SUT = new Rover(xPos, yPos);
            }
        }

        private class ArrangementBuilder
        {
            private int xPos = 0;
            private int yPos = 0;

            public ArrangementBuilder WithXPosition(int xPos)
            {
                this.xPos = xPos;
                return this;
            }

            public ArrangementBuilder WithYPosition(int yPos)
            {
                this.yPos = yPos;
                return this;
            }

            public Arrangement Build()
            {
                return new Arrangement(xPos, yPos);
            }
        }

            [Fact]
        public void Ctor_XPositionLowOutOfBounds_ShouldThrowArgumentOutOfRangeException()
        {
            // Act 
            var error = Record.Exception(() => new Rover(-1, 1));

            // Assert
            error.Should().BeOfType<ArgumentOutOfRangeException>();
            error.Message.Should().Be("Specified argument was out of the range of valid values. (Parameter 'x')");
        }

        [Fact]
        public void Ctor_YPositionLowOutOfBounds_ShouldThrowArgumentOutOfRangeException()
        {
            // Act 
            var error = Record.Exception(() => new Rover(1, -1));

            // Assert
            error.Should().BeOfType<ArgumentOutOfRangeException>();
            error.Message.Should().Be("Specified argument was out of the range of valid values. (Parameter 'y')");
        }

        [Fact]
        public void InitialPosition_XPositionLowOutOfBounds_ShouldThrowArgumentOutOfRangeException()
        {
            // Arrange

            var arrangement = new ArrangementBuilder()
                  .WithXPosition(1)
                  .WithYPosition(2)
                  .Build();


            // Act 
            var error = Record.Exception(() => arrangement.SUT.InitialPosition(-1, 1, 'N'));
            // Assert
            error.Should().BeOfType<ArgumentOutOfRangeException>();
            error.Message.Should().Be("Specified argument was out of the range of valid values. (Parameter 'x')");
        }

        [Fact]
        public void InitialPosition_YPositionLowOutOfBounds_ShouldThrowArgumentOutOfRangeException()
        {
            // Arrange

            var arrangement = new ArrangementBuilder()
                  .WithXPosition(1)
                  .WithYPosition(2)
                  .Build();


            // Act 
            var error = Record.Exception(() => arrangement.SUT.InitialPosition(1, -1, 'N'));
            // Assert
            error.Should().BeOfType<ArgumentOutOfRangeException>();
            error.Message.Should().Be("Specified argument was out of the range of valid values. (Parameter 'y')");
        }

        [Fact]
        public void InitialPosition_DirectionInvalidInput_ShouldThrowArgumentException()
        {
            // Arrange

            var arrangement = new ArrangementBuilder()
                  .WithXPosition(1)
                  .WithYPosition(2)
                  .Build();


            // Act 
            var error = Record.Exception(() => arrangement.SUT.InitialPosition(1, 1, 'C'));
            // Assert
            error.Should().BeOfType<ArgumentException>();
            error.Message.Should().Be("Direction specified isn't valid.");
        }
        [Theory]
        [InlineData('N')]
        [InlineData('E')]
        [InlineData('S')]
        [InlineData('W')]
        [InlineData('n')]
        [InlineData('e')]
        [InlineData('s')]
        [InlineData('w')]
        public void InitialPosition_DirectionValidInput_ShouldBeOK(char input)
        {
            // Arrange

            var arrangement = new ArrangementBuilder()
                  .WithXPosition(1)
                  .WithYPosition(2)
                  .Build();


            // Act 
            arrangement.SUT.InitialPosition(1, 1, input);


            // Assert
            arrangement.SUT.Direction.Should().Be(char.ToUpperInvariant(input));
        }

        [Fact]
        public void Move_CommandsInValidCharacter_ShouldThrowArgumentNullException()
        {
            // Arrange
            var arrangement = new ArrangementBuilder()
                .WithXPosition(1)
                .WithYPosition(2)
                .Build();

            // Act 
            var error = Record.Exception(() => arrangement.SUT.Move("LRMT"));

            // Assert
            error.Should().BeOfType<ArgumentException>();
            error.Message.Should().Be("Your command list has an invalid letter.");
        }

        [Fact]
        public void Move_CommandsScenario1_ShouldReturnCorrectResults()
        {
            // Arrange
            var arrangement = new ArrangementBuilder()
                .WithXPosition(5)
                .WithYPosition(5)
                .Build();

            // Act 
            arrangement.SUT.InitialPosition(1, 2, 'N');
            var results = arrangement.SUT.Move("LMLMLMLMM");

            // Assert
            results.Item1.Should().Be(1);
            results.Item2.Should().Be(3);
            results.Item3.Should().Be('N');
        }

        [Fact]
        public void Move_CommandsScenario2_ShouldReturnCorrectResults()
        {
            // Arrange
            var arrangement = new ArrangementBuilder()
                .WithXPosition(5)
                .WithYPosition(5)
                .Build();

            // Act 
            arrangement.SUT.InitialPosition(3, 3, 'E');
            var results = arrangement.SUT.Move("MMRMMRMRRM");

            // Assert
            results.Item1.Should().Be(5);
            results.Item2.Should().Be(1);
            results.Item3.Should().Be('E');
        }

        [Fact]
        public void Move_CommandsScenario3_ShouldReturnCorrectResults()
        {
            // Arrange
            var arrangement = new ArrangementBuilder()
                .WithXPosition(3)
                .WithYPosition(3)
                .Build();

            // Act 
            arrangement.SUT.InitialPosition(1, 1, 'E');
            var results = arrangement.SUT.Move("MMMM");

            // Assert
            results.Item1.Should().Be(3);
            results.Item2.Should().Be(1);
            results.Item3.Should().Be('E');
        }

        [Fact]
        public void Move_CommandsScenario4_ShouldReturnCorrectResults()
        {
            // Arrange
            var arrangement = new ArrangementBuilder()
                .WithXPosition(3)
                .WithYPosition(3)
                .Build();

            // Act 
            arrangement.SUT.InitialPosition(1, 1, 'N');
            var results = arrangement.SUT.Move("MMMM");

            // Assert
            results.Item1.Should().Be(1);
            results.Item2.Should().Be(3);
            results.Item3.Should().Be('N');
        }

        [Fact]
        public void Move_CommandsScenario5_ShouldReturnCorrectResults()
        {
            // Arrange
            var arrangement = new ArrangementBuilder()
                .WithXPosition(3)
                .WithYPosition(3)
                .Build();

            // Act 
            arrangement.SUT.InitialPosition(1, 1, 'N');
            var results = arrangement.SUT.Move("MMMMRMMMM");

            // Assert
            results.Item1.Should().Be(3);
            results.Item2.Should().Be(3);
            results.Item3.Should().Be('E');
        }

    }
}

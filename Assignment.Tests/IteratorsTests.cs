using System;
using System.Collections.Generic;
using Xunit;

namespace Assignment.Tests
{
    public class UnitTest1
    {
        [Fact]
        public void Flatten_GivenStreamOfStreams_ReturnsJoinedStream() {
            // Arrange
            var stream = new[] { new[] { 2 }, new[] { 2 } };
            var expected = new[] { 2, 2 };
            
            var resultStream = Iterators.Flatten(stream); // Act
            Assert.Equal(expected, resultStream); // Assert
        }
        
        [Fact]
        public void Flatten_GivenEmptyStream_ReturnsEmptyStream() {
            // Arrange
            var stream = new int[1][];
            var expected = new int[1];
            
            var resultStream = Iterators.Flatten(stream); // Act
            Assert.Equal(expected, resultStream); // Assert
        }
        
        [Fact]
        public void Flatten_GivenNull_ThrowsArgumentNullException() {
            IEnumerable<IEnumerable<int>> stream = null; // Arrange
            Assert.Throws<ArgumentNullException>(() => Iterators.Flatten(stream)); // Assert
        }
        
        [Theory]
        [InlineData(new int[]{}, new int[]{})]
        [InlineData(new[]{2,4,6,8}, new[]{1,2,3,4,5,6,7,8})]
        [InlineData(new[]{2,-4,6,-8}, new[]{-1,2,3,-4,5,6,-7,-8})]
        [InlineData(new int[]{}, new[]{1})]
        public void Filter_FilterEvenNumbersInStream_ReturnsFilteredStream(IEnumerable<int> expected, IEnumerable<int> input) {
            Predicate<int> even = Number.Even; // Arrange
            var actual = Iterators.Filter(input,even); // Act
            Assert.Equal(expected, actual); // Assert
        }
        
        [Fact]
        public void Filter_GivenNull_ThrowsArgumentNullException() {
            // Arrange
            Predicate<int> even = Number.Even;
            IEnumerable<int> stream = null;

            Assert.Throws<ArgumentNullException>(() => Iterators.Filter(stream,even)); // Assert
        }
    }
}

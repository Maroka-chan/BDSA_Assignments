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
    }
}

using System;
using System.Collections.Generic;
using Xunit;

namespace Assignment.Tests
{
    public class UnitTest1
    {
        [Fact]
        public void Flatten_GivenStreamOfStreams_ReturnsJoinedStream()
        {
            // Arrange
            var stream = new[] { new[] { 2 }, new[] { 2 } };
            var expected = new[] { 2, 2 };

            // Act
            var resultStream = Iterators.Flatten(stream);

            // Assert
            Assert.Equal(expected, resultStream);
        }
        
        [Fact]
        public void Flatten_GivenEmptyStream_ReturnsEmptyStream()
        {
            // Arrange
            var stream = new IEnumerable<int>[1];
            var expected = new int[1];

            // Act
            var resultStream = Iterators.Flatten(stream);

            // Assert
            Assert.Equal(expected, resultStream);
        }
    }
}

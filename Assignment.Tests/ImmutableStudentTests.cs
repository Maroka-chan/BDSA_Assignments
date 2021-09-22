using System;
using System.Data;
using Xunit;

namespace Assignment.Tests
{
    public class ImmutableStudentTests
    {
        [Fact]
        public void GetStatus_WhileCurrentDateIsLaterThanGraduationDateAndEndIsLaterGraduation_ReturnsGraduated() {
            var immutableStudent = new ImmuntableStudent() // Arrange
            {
                EndDate = DateTime.Now,
                GraduationDate = DateTime.Now.AddDays(-1)
            };
            var status = immutableStudent.Status; // Act
            Assert.Equal(Status.Graduated, status); // Assert
        }
        
        [Fact]
        public void GetStatus_WhileCurrentDateIsLessOrEqualToTwoMonthsAfterStart_ReturnsNew() {
            var immutableStudent = new ImmuntableStudent() // Arrange
            {
                StartDate = DateTime.Now.AddDays(-1),
                EndDate = DateTime.Now.AddMonths(3)
            };
            var status = immutableStudent.Status; // Act
            Assert.Equal(Status.New, status); // Assert
        }

        [Fact]
        public void GetStatus_WhileCurrentDateIsBeforeStart_ThrowsInvalidConstraintException() {
            var immutableStudent = new ImmuntableStudent() // Arrange
                { StartDate = DateTime.Now.AddDays(1) };
            Assert.Throws<InvalidConstraintException>(() => immutableStudent.Status); // Assert
        }
        
        [Fact]
        public void GetStatus_WhileCurrentDateIsLaterThanTwoMonthsAfterStartAndBeforeGraduation_ReturnsActive() {
            var immutableStudent = new ImmuntableStudent() // Arrange
            {
                StartDate = DateTime.Now.AddMonths(-2).AddDays(-1),
                EndDate = DateTime.Now.AddDays(2),
                GraduationDate = DateTime.Now.AddDays(1)
            };
            var status = immutableStudent.Status; // Act
            Assert.Equal(Status.Active, status); // Assert
        }
        
        [Fact]
        public void GetStatus_WhileEndIsEarlierThanGraduation_ReturnsDropout() {
            var immutableStudent = new ImmuntableStudent() // Arrange
            {
                EndDate = DateTime.Now.AddDays(-2),
                GraduationDate = DateTime.Now.AddDays(-1)
            };
            var status = immutableStudent.Status; // Act
            Assert.Equal(Status.Dropout, status); // Assert
        }
        
        [Fact]
        public void GetStatus_WhileEndIsBeforeStart_ThrowsInvalidConstraintException() {
            var immutableStudent = new ImmuntableStudent() // Arrange
            {
                StartDate = DateTime.Now,
                EndDate = DateTime.Now.AddDays(-1),
            };
            Assert.Throws<InvalidConstraintException>(() => immutableStudent.Status); // Assert
        }
    }
}

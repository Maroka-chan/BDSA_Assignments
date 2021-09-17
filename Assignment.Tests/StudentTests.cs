using System;
using System.Data;
using Xunit;

namespace Assignment.Tests
{
    public class StudentTests
    {
        [Fact]
        public void GetStatus_WhileCurrentDateIsLaterThanGraduationDate_ReturnsGraduated() {
            var student = new Student // Arrange
                { GraduationDate = DateTime.Now.AddDays(-1) };
            var status = student.Status; // Act
            Assert.Equal(Status.Graduated, status); // Assert
        }
        
        [Fact]
        public void GetStatus_WhileCurrentDateIsLessOrEqualToTwoMonthsAfterStart_ReturnsNew() {
            var student = new Student // Arrange
                { StartDate = DateTime.Now.AddDays(-1) };
            var status = student.Status; // Act
            Assert.Equal(Status.New, status); // Assert
        }

        [Fact]
        public void GetStatus_WhileCurrentDateIsBeforeStart_ThrowsInvalidConstraintException() {
            var student = new Student // Arrange
                { StartDate = DateTime.Now.AddDays(1) };
            Assert.Throws<InvalidConstraintException>(() => student.Status); // Assert
        }
        
        [Fact]
        public void GetStatus_WhileCurrentDateIsLaterThanTwoMonthsAfterStartAndBeforeGraduation_ReturnsActive() {
            var student = new Student // Arrange
            {
                StartDate = DateTime.Now.AddMonths(-2).AddDays(-1),
                GraduationDate = DateTime.Now.AddDays(1)
            };
            var status = student.Status; // Act
            Assert.Equal(Status.Active, status); // Assert
        }
        
        [Fact]
        public void GetStatus_WhileCurrentDateIsLaterThanEndAndEndIsEarlierThanGraduation_ReturnsDropout() {
            var student = new Student // Arrange
            {
                EndDate = DateTime.Now.AddDays(-2),
                GraduationDate = DateTime.Now.AddDays(-1)
            };
            var status = student.Status; // Act
            Assert.Equal(Status.Dropout, status); // Assert
        }
        
        [Fact]
        public void GetStatus_WhileEndIsBeforeStart_ThrowsInvalidConstraintException() {
            var student = new Student // Arrange
            {
                StartDate = DateTime.Now,
                EndDate = DateTime.Now.AddDays(-1),
            };
            Assert.Throws<InvalidConstraintException>(() => student.Status); // Assert
        }
    }
}

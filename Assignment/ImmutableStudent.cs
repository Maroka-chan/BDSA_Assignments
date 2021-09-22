using System;
using System.Data;

namespace Assignment
{
    public record ImmuntableStudent
    {
        public int Id { get; init; }
        public string GivenName { get; init; }
        public string Surname { get; init; }
        
        public Status Status
        {
            get
            {
                var currentDate = DateTime.Now;
                var startDate = StartDate;
                var endDate = EndDate;
                if (endDate < startDate) throw new InvalidConstraintException("End Date can't be before Start Date.");
                if (currentDate < startDate) throw new InvalidConstraintException("Student haven't enrolled yet.");
                var twoMonthsAfterStart = StartDate.AddMonths(2);
                if (currentDate <= twoMonthsAfterStart) return Status.New;
                var graduationDate = GraduationDate;
                if (currentDate > graduationDate && endDate > graduationDate) return Status.Graduated;
                if (currentDate > twoMonthsAfterStart && currentDate < graduationDate) return Status.Active;
                if (endDate < GraduationDate) return Status.Dropout;
                throw new InvalidOperationException("Status couldn't be determined.");
            }
        }
        
        public DateTime StartDate { get; init; }
        public DateTime EndDate { get; init; }
        public DateTime GraduationDate { get; init; }
        
        
        public override string ToString() {
            return $"Id: {Id}\nGivenName: {GivenName}\n" +
                   $"Surname: {Surname}\n" +
                   $"Status: {Status}\n" +
                   $"StartDate: {StartDate}\n" +
                   $"EndDate: {EndDate}\n" +
                   $"GraduationDate: {GraduationDate}";
        }
    }
}
using System;

namespace Assignment
{
    public class Student
    {
        public int Id { get; init; }
        public string GivenName { get; set; }
        public string Surname { get; set; }
        public Status Status { get; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public DateTime GraduationDate { get; set; }


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
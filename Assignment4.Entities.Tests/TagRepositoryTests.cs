using System;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Assignment4.Entities;
using Xunit;
using System.Collections.Generic;


namespace Assignment4.Entities.Tests
{
    
    public class TagRepositoryTests : IDisposable
    {
        private readonly KanbanContext _context;
        private readonly Tag _tagRepo;//skal være vores tag repository

        public TagRepositoryTests()
        {
            var connection = new SqliteConnection("Filename=:memory:");
            connection.Open();
            var builder = new DbContextOptionsBuilder<KanbanContext>();
            builder.UseSqlite(connection);
            
            var context = new KanbanContext(builder.Options);
            context.Database.EnsureCreated();
            
            var user1 = new User {Id = 1,Name = "Anders", Email = "Ander@hotmail.com"};//de har også en collection af tasks
            var user2 = new User {Id = 2, Name = "Johan", Email = "Johan@hotmail.com"};
            var user3 = new User {Id = 3, Name = "Kim", Email = "Kim@hotmail.com"};
            var user4 = new User {Id = 4, Name = "Cirkeline", Email = "Cirkeline@hotmail.com"};
            var user5 = new User {Id = 5, Name = "Magrethe", Email = "Magrethe@hotmail.com"};

            var task1 = new Task {Id = 1, Title = "task1", AssignedTo = user1, State = State.New, Tags = null};
            var task2 = new Task {Id = 2, Title = "task2", AssignedTo = user2, State = State.Active, Tags = null};
            var task3 = new Task {Id = 3, Title = "task3", AssignedTo = user3, State = State.Closed, Tags = null};
            var task4 = new Task {Id = 4, Title = "task4", AssignedTo = user4, State = State.Removed, Tags = null};
            var task5 = new Task {Id = 5, Title = "task5", AssignedTo = user5, State = State.Resolved, Tags = null};
            
            var taskList1 = new List<Task> {task1,task2};
            var taskList2 = new List<Task> {task2,task3};
            var taskList3 = new List<Task> {task3,task4};
            var taskList4 = new List<Task> {task4,task5};
            var taskList5 = new List<Task> {task5,task1};


            context.Tags.AddRange(
     new Tag {Id = 1, Name = "tag1", Tasks = taskList1},
                new Tag {Id = 2, Name = "tag2", Tasks = taskList2},
                new Tag {Id = 3, Name = "tag3", Tasks = taskList3},
                new Tag {Id = 4, Name = "tag4", Tasks = taskList4},
                new Tag {Id = 5, Name ="tag5" , Tasks = taskList5},
                new Tag {Id = 6, Name = "tag6", Tasks = null},
                new Tag {Id = 7, Name = "tag7", Tasks = null},
                new Tag {Id = 8, Name = "tag8", Tasks = null},
                new Tag {Id = 9, Name = "tag9", Tasks = null},
                new Tag {Id = 10, Name = "tag10", Tasks = null}
            );

            context.SaveChanges();
            _context = context;
            _tagRepo = new Tag();//skal tage vores context som paramter, og være vores repo

        }
        
        //tests here

        public void Dispose()
        {
            _context.Dispose();
        }


    }
}
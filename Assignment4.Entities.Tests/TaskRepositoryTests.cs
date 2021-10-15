using System;
using System.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Data.Sqlite;
using Assignment4.Entities;
using Xunit;
using System.Collections.Generic;
using System.Linq;
using Assignment4.Core;


namespace Assignment4.Entities.Tests
{
    public class TaskRepositoryTests : IDisposable
    {
        private readonly KanbanContext _context;
        private readonly TaskRepository _taskRepo; //skal være vores task repository

        public TaskRepositoryTests()
        {
            var connection = new SqliteConnection("Filename=:memory:");
            connection.Open();
            var builder = new DbContextOptionsBuilder<KanbanContext>();
            builder.UseSqlite(connection);
            
            var context = new KanbanContext(builder.Options);
            context.Database.EnsureCreated();
            
            var user1 = new User {Id = 1, Name = "Anders", Email = "Ander@hotmail.com"};
            var user2 = new User {Id = 2, Name = "Johan", Email = "Johan@hotmail.com"};
            var user3 = new User {Id = 3, Name = "Kim", Email = "Kim@hotmail.com"};
            var user4 = new User {Id = 4, Name = "Cirkeline", Email = "Cirkeline@hotmail.com"};
            var user5 = new User {Id = 5, Name = "Magrethe", Email = "Magrethe@hotmail.com"};

            var tag1 = new Tag {Id = 1, Name = "tag1"};
            var tag2 = new Tag {Id = 2, Name = "tag2"};
            var tag3 = new Tag {Id = 3, Name = "tag3"};
            var tag4 = new Tag {Id = 4, Name = "tag4"};
            var tag5 = new Tag {Id = 5, Name = "tag5"};
            var tag6 = new Tag {Id = 6, Name = "tag6"};
            var tag7 = new Tag {Id = 7, Name = "tag7"};
            var tag8 = new Tag {Id = 8, Name = "tag8"};
            var tag9 = new Tag {Id = 9, Name = "tag9"};
            var tag10 = new Tag {Id = 10, Name = "tag10"};
            var tagList1 = new List<Tag> {tag1, tag2};
            var tagList2 = new List<Tag> {tag3, tag4};
            var tagList3 = new List<Tag> {tag5, tag6};
            var tagList4 = new List<Tag> {tag7, tag8};
            var tagList5 = new List<Tag> {tag9, tag10};

            context.AddRange(user1,user2,user3,user4,user5);

            context.AddRange(tag1, tag2, tag3, tag4, tag5, tag6, tag7, tag8, tag9, tag10);
            
            context.Tasks.AddRange(
                new Task{Id = 1, Title = "task1", AssignedTo = user1, State = State.New, Tags =tagList1},
                new Task{Id = 2, Title = "task2", AssignedTo = user2, State = State.Active, Tags = tagList2},
                new Task{Id = 3, Title = "task3", AssignedTo = user3, State = State.Closed, Tags = tagList3},
                new Task{Id = 4, Title = "task4", AssignedTo = user4, State = State.Removed, Tags = tagList4},
                new Task{Id = 5, Title = "task5", AssignedTo = user5, State = State.Resolved, Tags = tagList5}
            );

            context.SaveChanges();
            _context = context;
            _taskRepo = new TaskRepository(_context);
        }

        [Fact]
        public void Create_CreatesTaskWithStateNewAndCreatedAndStateUpdatedWithCurrentUTCTime() {
            var taskRepo = new TaskRepository(_context);
            var task = new TaskCreateDTO {Title = "New Task", AssignedToId = _context.Users.First().Id, Tags = new List<string>(){"tag2"}};
            var expected = DateTime.UtcNow;
            
            var (resp, createdTask) = taskRepo.Create(task);
            Assert.Equal(expected, _context.Tasks.Single(t => t.Id==createdTask).Created, precision: TimeSpan.FromSeconds(5));
            Assert.Equal(State.New, _context.Tasks.Single(t => t.Id==createdTask).State);
        }
        
        [Fact]
        public void Delete_DeleteResolvedTaskReturnsConflict() {
            var taskRepo = new TaskRepository(_context);
            Assert.Equal(Response.Conflict, taskRepo.Delete(5));
        }


        public void Dispose()
        {
            _context.Dispose();
        }
    }
}

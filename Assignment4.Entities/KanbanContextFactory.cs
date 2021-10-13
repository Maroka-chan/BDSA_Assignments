using System.Collections.Generic;
using System.IO;
using Assignment4.Core;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace Assignment4.Entities {
    public class KanbanContextFactory : IDesignTimeDbContextFactory<KanbanContext> {
        public KanbanContext CreateDbContext(string[] args)
        {
            var configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddUserSecrets<KanbanContext>()
                .Build();

            var connectionString = configuration.GetConnectionString("Kanban");

            var optionsBuilder = new DbContextOptionsBuilder<KanbanContext>()
                .UseSqlServer(connectionString);

            return new KanbanContext(optionsBuilder.Options);
        }

        public static void Seed(KanbanContext context)
        {
            var user1 = new User {Id = 1,Name = "Anders", Email = "Ander@hotmail.com"};//de har også en collection af tasks
            var user2 = new User {Id = 2, Name = "Johan", Email = "Johan@hotmail.com"};
            var user3 = new User {Id = 3, Name = "Kim", Email = "Kim@hotmail.com"};
            var user4 = new User {Id = 4, Name = "Cirkeline", Email = "Cirkeline@hotmail.com"};
            var user5 = new User {Id = 5, Name = "Magrethe", Email = "Magrethe@hotmail.com"};

            var tag1 = new Tag {Id = 1, Name = "tag1", Tasks = null};
            var tag2 = new Tag {Id = 2, Name = "tag2", Tasks = null};
            var tag3 = new Tag {Id = 3, Name = "tag3", Tasks = null};
            var tag4 = new Tag {Id = 4, Name = "tag4", Tasks = null};
            var tag5 = new Tag {Id = 5, Name ="tag5" , Tasks = null};
            var tag6 = new Tag {Id = 6, Name = "tag6", Tasks = null};
            var tag7 = new Tag {Id = 7, Name = "tag7", Tasks = null};
            var tag8 = new Tag {Id = 8, Name = "tag8", Tasks = null};
            var tag9 = new Tag {Id = 9, Name = "tag9", Tasks = null};
            var tag10 = new Tag {Id = 10, Name = "tag10", Tasks = null};
            var tagList1 = new List<Tag> {tag1, tag2};
            var tagList2 = new List<Tag> {tag3, tag4};
            var tagList3 = new List<Tag> {tag5, tag6};
            var tagList4 = new List<Tag> {tag7, tag8};
            var tagList5 = new List<Tag> {tag9, tag10};


            context.Tasks.AddRange(
                new Task{Id = 1,Title = "task1", AssignedTo = user1, State = State.New, Tags =tagList1 },
                new Task{Id = 2,Title = "task2", AssignedTo = user2, State = State.Active, Tags = tagList2},
                new Task{Id = 3,Title = "task3", AssignedTo = user3, State = State.Closed, Tags = tagList3},
                new Task{Id = 4,Title = "task4", AssignedTo = user4, State = State.Removed, Tags = tagList4},
                new Task{Id = 5,Title = "task5", AssignedTo = user5, State = State.Resolved, Tags = tagList5}
            );

            context.SaveChanges();
        }
    }
}
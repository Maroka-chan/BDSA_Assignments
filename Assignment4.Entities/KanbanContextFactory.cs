using System.IO;
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
    }
}
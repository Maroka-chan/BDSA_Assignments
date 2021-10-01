using System;
using System.IO;
using Assignment4.Entities;
using Microsoft.Extensions.Configuration;
using Microsoft.EntityFrameworkCore;

namespace Assignment4 {
    class Program {
        static void Main(string[] args) {
            var configuration = LoadConfiguration();
            var connectionString = configuration.GetConnectionString("Kanban");

            static IConfiguration LoadConfiguration() {
                var builder = new ConfigurationBuilder()
                    .SetBasePath(Directory.GetCurrentDirectory())
                    .AddJsonFile("appsettings.json")
                    .AddUserSecrets<Program>();
                
                return builder.Build();
            }
            var optionsBuilder = new DbContextOptionsBuilder<KanbanContext>().UseSqlServer(connectionString);
            using var context = new KanbanContext(optionsBuilder.Options);
        }
    }
}
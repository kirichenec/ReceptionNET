using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Reception.Model.Dto;

namespace Reception.Server.Data.Repository
{
    public class ReceptionContext : DbContext
    {
        public static readonly ILoggerFactory ReceptionLoggerFactory = LoggerFactory.Create(builder => { builder.AddConsole(); });

        public DbSet<PersonDto> Persons { get; set; }
        public DbSet<PostDto> Posts { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            var connectionStringBuilder = new SqliteConnectionStringBuilder { DataSource = $"{nameof(Reception)}.db" };
            var connectionString = connectionStringBuilder.ToString();
            var connection = new SqliteConnection(connectionString);

            optionsBuilder
                .UseLoggerFactory(ReceptionLoggerFactory)
                .UseSqlite(connection);
        }
    }
}
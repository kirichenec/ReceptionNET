using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Reception.Server.File.Model;

namespace Reception.Server.File.Repository
{
    public class FileContext : DbContext
    {
        public static readonly ILoggerFactory ReceptionLoggerFactory = LoggerFactory.Create(builder => { builder.AddConsole(); });

        public DbSet<FileData> Datas { get; set; }
        public DbSet<FileVersion> VersionInfoes { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            var connectionStringBuilder = new SqliteConnectionStringBuilder { DataSource = $"{nameof(Reception.Server.File)}.db" };
            var connectionString = connectionStringBuilder.ToString();
            var connection = new SqliteConnection(connectionString);

            optionsBuilder
                .UseLoggerFactory(ReceptionLoggerFactory)
                .UseSqlite(connection);
        }
    }
}
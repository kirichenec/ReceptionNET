using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Reception.Server.Data.Entities;

namespace Reception.Server.Data.Repository
{
    public class DataContext : DbContext
    {
        public DataContext()
        {
            Database.EnsureCreated();
            Database.Migrate();
        }

        public DbSet<Person> Persons { get; set; }
        public DbSet<Post> Posts { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            var connectionStringBuilder = new SqliteConnectionStringBuilder { DataSource = $"{nameof(Reception)}.{nameof(Server)}.{nameof(Data)}.db" };
            var connectionString = connectionStringBuilder.ToString();
            var connection = new SqliteConnection(connectionString);

            optionsBuilder
                .UseLoggerFactory(Startup.ReceptionLoggerFactory)
                .UseSqlite(connection);
        }
    }
}
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Reception.Server.Auth.Entities;

namespace Reception.Server.Auth.Repository
{
    public class UserContext : DbContext
    {
        public UserContext()
        {
            Database.EnsureCreated();
            Database.Migrate();
        }

        public DbSet<User> Users { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            var connectionStringBuilder = new SqliteConnectionStringBuilder { DataSource = $"{nameof(Reception)}.{nameof(Server)}.{nameof(Auth)}.db" };
            var connectionString = connectionStringBuilder.ToString();
            var connection = new SqliteConnection(connectionString);

            optionsBuilder
                .UseLoggerFactory(Startup.ReceptionLoggerFactory)
                .UseSqlite(connection);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>().HasAlternateKey(p =>p.Username).HasName("IX_Username");
        }
    }
}

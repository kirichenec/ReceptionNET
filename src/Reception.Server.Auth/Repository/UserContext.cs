using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Reception.Server.Auth.Entities;
using Reception.Server.Auth.PasswordHelper;

namespace Reception.Server.Auth.Repository
{
    public class UserContext : DbContext
    {
        private readonly IPasswordHasher _passwordHasher;

        public UserContext(IOptions<HashingOptions> hashingOptions)
        {
            _passwordHasher = new PasswordHasher(hashingOptions.Value);

            Database.EnsureCreated();
            Database.Migrate();
        }

        public DbSet<Token> Tokens { get; set; }

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
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<User>().HasAlternateKey(p => p.Login).HasName("IX_Login");
            modelBuilder.Entity<User>().HasData(new User { Id = 1, Login = "admin", Password = _passwordHasher.Hash("admin") });
        }
    }
}

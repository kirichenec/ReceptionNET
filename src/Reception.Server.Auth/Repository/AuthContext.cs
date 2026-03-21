using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.Extensions.Options;
using Reception.Server.Auth.Entities;
using Reception.Server.Auth.PasswordHelper;

namespace Reception.Server.Auth.Repository
{
    public class AuthContext : DbContext
    {
        private const string SHEMA_NAME_AUTH = "Auth";
        private const string TABLE_NAME_USER = "User";
        private const string TABLE_NAME_TOKEN = "Token";

        private readonly IPasswordHasher _passwordHasher;


        public AuthContext(IOptions<HashingOptions> hashingOptions)
        {
            _passwordHasher = new PasswordHasher(hashingOptions.Value);

            Database.Migrate();
        }


        public DbSet<Token> Tokens { get; set; }

        public DbSet<User> Users { get; set; }


        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            var connectionStringBuilder = new SqliteConnectionStringBuilder { DataSource = $"{nameof(Reception)}.{nameof(Server)}.{nameof(Auth)}.db" };
            var connectionString = connectionStringBuilder.ToString();
            var connection = new SqliteConnection(connectionString);

            optionsBuilder.UseSqlite(connection);
            optionsBuilder.ConfigureWarnings(w => w.Ignore(RelationalEventId.PendingModelChangesWarning));
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            ConfigureTokenEntity(modelBuilder);
            ConfigureUserEntity(modelBuilder, _passwordHasher);
        }

        private static void ConfigureTokenEntity(ModelBuilder modelBuilder)
        {
            var builder = modelBuilder.Entity<Token>();

            builder.ToTable(TABLE_NAME_TOKEN, SHEMA_NAME_AUTH);

            builder.HasKey(x => x.UserId);
            builder.Property(x => x.UserId).ValueGeneratedNever();

            builder.Property(x => x.Value).IsRequired();
        }

        private static void ConfigureUserEntity(ModelBuilder modelBuilder, IPasswordHasher _passwordHasher)
        {
            var builder = modelBuilder.Entity<User>();

            builder.ToTable(TABLE_NAME_USER, SHEMA_NAME_AUTH);

            builder.HasKey(x => x.Id);
            builder.HasAlternateKey(p => p.Login).HasName("IX_Login");

            builder.Property(x => x.Login).IsRequired();

            builder.HasData(new User { Id = 1, Login = "admin", Password = _passwordHasher.Hash("admin") });
        }
    }
}

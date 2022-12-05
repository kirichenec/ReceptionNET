using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Reception.Server.Data.Entities;

namespace Reception.Server.Data.Repository
{
    public class DataContext : DbContext
    {
        public DataContext()
        {
            Database.Migrate();
        }

        public DbSet<Person> Persons { get; set; }

        public DbSet<Post> Posts { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            var connectionStringBuilder = new SqliteConnectionStringBuilder
            {
                DataSource = $"{nameof(Reception)}.{nameof(Server)}.{nameof(Data)}.db"
            };
            var connectionString = connectionStringBuilder.ToString();
            var connection = new SqliteConnection(connectionString);

            optionsBuilder.UseSqlite(connection);
        }

#if DEBUG
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Post>().HasData(
                new Post
                {
                    Id = 1,
                    Name = "Brainfucker",
                });
            modelBuilder.Entity<Person>().HasData(
                new Person
                {
                    Id = 1,
                    FirstName = "Igor",
                    MiddleName = "Grigorievich",
                    SecondName = "Kirichenko",
                },
                new Person
                {
                    Id = 2,
                    FirstName = "Anna",
                    MiddleName = "Sergeevna",
                    SecondName = "Ushkalova"
                });
        }
#endif
    }
}
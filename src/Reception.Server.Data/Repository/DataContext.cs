using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Reception.Server.Data.Entities;

namespace Reception.Server.Data.Repository
{
    public class DataContext : DbContext
    {
        private const string SHEMA_NAME_PERSON = "Person";
        private const string TABLE_NAME_PERSON = "Person";
        private const string TABLE_NAME_PERSON_ADDITIONAL = "PersonAdditional";
        private const string TABLE_NAME_POST = "Post";


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

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            ConfigurePersonEntity(modelBuilder);
            ConfigurePersonAdditionalEntity(modelBuilder);
            ConfigurePostEntity(modelBuilder);
        }

        private static void ConfigurePersonEntity(ModelBuilder modelBuilder)
        {
            var builder = modelBuilder.Entity<Person>();

            builder.ToTable(TABLE_NAME_PERSON, SHEMA_NAME_PERSON);

            builder.HasKey(x => x.Id);

            builder.Property(x => x.FirstName).IsRequired();
            builder.Property(x => x.SecondName).IsRequired();

#if DEBUG
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
#endif
        }

        private static void ConfigurePersonAdditionalEntity(ModelBuilder modelBuilder)
        {
            var builder = modelBuilder.Entity<PersonAdditional>();

            builder.ToTable(TABLE_NAME_PERSON_ADDITIONAL, SHEMA_NAME_PERSON);

            builder.HasKey(x => x.Id);
        }

        private static void ConfigurePostEntity(ModelBuilder modelBuilder)
        {
            var builder = modelBuilder.Entity<Post>();

            builder.ToTable(TABLE_NAME_POST, SHEMA_NAME_PERSON);

            builder.HasKey(x => x.Id);

            builder.Property(x => x.Name).IsRequired();

#if DEBUG
            builder.HasData(
                new Post
                {
                    Id = 1,
                    Name = "Brainfucker",
                });
#endif
        }
    }
}

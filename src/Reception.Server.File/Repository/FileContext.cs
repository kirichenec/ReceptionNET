using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Reception.Extension.Converters;
using Reception.Model.Interface;
using Reception.Server.File.Entities;
using Reception.Server.File.Model;
using Reception.Server.File.Repository.Triggers;

namespace Reception.Server.File.Repository
{
    public class FileContext : DbContext
    {
        private const string SHEMA_NAME_FILES = "Files";
        private const string TABLE_NAME_FILEDATA = "FileData";

        private readonly AppSettings _appSettings;


        public FileContext(IOptions<AppSettings> appSettings)
        {
            _appSettings = appSettings.Value;

            Database.Migrate();
        }


        public DbSet<FileData> FileDatas { get; set; }


        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            var connectionStringBuilder = new SqliteConnectionStringBuilder
            {
                DataSource = $"{nameof(Reception)}.{nameof(Server)}.{nameof(File)}.db"
            };

            var connectionString = connectionStringBuilder.ToString();
            var connection = new SqliteConnection(connectionString);

            optionsBuilder
                .UseSqlite(connection)
                .UseTriggers(triggerOptions => triggerOptions.AddTrigger<FileDataBeforeSaveTrigger>());

            base.OnConfiguring(optionsBuilder);
        }

        protected override async void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            await ConfigureFileDataEntity(modelBuilder, _appSettings);
        }

        private static async Task ConfigureFileDataEntity(ModelBuilder modelBuilder, AppSettings appSettings)
        {
            var builder = modelBuilder.Entity<FileData>();

            builder.ToTable(TABLE_NAME_FILEDATA, SHEMA_NAME_FILES);

            builder.HasKey(x => x.Id);

            builder.Property(x => x.Data).IsRequired();
            builder.Property(x => x.Name).IsRequired();

            builder.HasData(new FileData
            {
                Id = 1,
                Data = await GetDefaultPhotoDataAsync(),
                Comment = "Default admin photo",
                Extension = "png",
                Name = "admin",
                Type = FileType.Photo,
                Version = Guid.NewGuid()
            });


            async Task<byte[]> GetDefaultPhotoDataAsync()
            {
                return await appSettings.DefaultVisitorPhotoPath.GetFileBytesByPathAsync(CancellationToken.None);
            }
        }
    }
}

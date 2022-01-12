using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Reception.Extension.Converters;
using Reception.Server.File.Entities;
using Reception.Server.File.Model;
using Reception.Server.File.Repository.Triggers;
using System;
using System.Threading.Tasks;
using static Reception.Model.Interface.IFileData;

namespace Reception.Server.File.Repository
{
    public class FileContext : DbContext
    {
        private readonly AppSettings _appSettings;

        public FileContext(IOptions<AppSettings> appSettings)
        {
            _appSettings = appSettings.Value;

            Database.EnsureCreated();
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
                .UseLoggerFactory(Startup.ReceptionLoggerFactory)
                .UseSqlite(connection)
                .UseTriggers(triggerOptions => triggerOptions.AddTrigger<FileDataBeforeSaveTrigger>());

            base.OnConfiguring(optionsBuilder);
        }

        protected override async void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<FileData>().HasData(new FileData
            {
                Id = 1,
                Data = await GetDefaultPhotoData(),
                Comment = "Default admin photo",
                Extension = "png",
                Name = "admin",
                Type = FileType.Photo,
                Version = Guid.NewGuid()
            });


            async Task<byte[]> GetDefaultPhotoData()
            {
                return await _appSettings.DefaultVisitorPhotoPath.GetFileBytesByPathAsync();
            }
        }
    }
}
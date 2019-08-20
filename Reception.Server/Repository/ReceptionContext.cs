﻿using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Reception.Model.Dto;

namespace Reception.Server.Repository
{
    public class ReceptionContext : DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            var connectionStringBuilder = new SqliteConnectionStringBuilder { DataSource = $"{nameof(Reception)}.db" };
            var connectionString = connectionStringBuilder.ToString();
            var connection = new SqliteConnection(connectionString);

            optionsBuilder.UseSqlite(connection);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder
                .Entity<PersonDto>().HasIndex(b => b.PostUid).IsUnique(false);
        }

        public DbSet<PersonDto> Persons { get; set; }
        public DbSet<PostDto> Posts { get; set; }
    }
}

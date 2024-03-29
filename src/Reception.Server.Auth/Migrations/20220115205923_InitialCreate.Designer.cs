﻿// <auto-generated />
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Reception.Server.Auth.Repository;

#nullable disable

namespace Reception.Server.Auth.Migrations
{
    [DbContext(typeof(AuthContext))]
    [Migration("20220115205923_InitialCreate")]
    partial class InitialCreate
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder.HasAnnotation("ProductVersion", "6.0.1");

            modelBuilder.Entity("Reception.Server.Auth.Entities.Token", b =>
                {
                    b.Property<int>("UserId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Value")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("UserId");

                    b.ToTable("Token", "Auth");
                });

            modelBuilder.Entity("Reception.Server.Auth.Entities.User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("FirstName")
                        .HasColumnType("TEXT");

                    b.Property<string>("LastName")
                        .HasColumnType("TEXT");

                    b.Property<string>("Login")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("MiddleName")
                        .HasColumnType("TEXT");

                    b.Property<string>("Password")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.HasAlternateKey("Login")
                        .HasName("IX_Login");

                    b.ToTable("User", "Auth");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Login = "admin",
                            Password = "10000.5ZAd1tkXXwpajvshEm95vQ==.jsMhLrZjMbYtHGBtpA0XYAyvGi0KucxT7+FzuqfhLnM="
                        });
                });
#pragma warning restore 612, 618
        }
    }
}

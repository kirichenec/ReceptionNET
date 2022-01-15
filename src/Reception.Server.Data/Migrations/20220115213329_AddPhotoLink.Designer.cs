﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Reception.Server.Data.Repository;

#nullable disable

namespace Reception.Server.Data.Migrations
{
    [DbContext(typeof(DataContext))]
    [Migration("20220115213329_AddPhotoLink")]
    partial class AddPhotoLink
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder.HasAnnotation("ProductVersion", "6.0.1");

            modelBuilder.Entity("Reception.Server.Data.Entities.Person", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<int?>("AdditionalInfoId")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Comment")
                        .HasColumnType("TEXT");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("MiddleName")
                        .HasColumnType("TEXT");

                    b.Property<int?>("PostId")
                        .HasColumnType("INTEGER");

                    b.Property<string>("SecondName")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.HasIndex("AdditionalInfoId");

                    b.HasIndex("PostId");

                    b.ToTable("Person", "Person");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            FirstName = "Igor",
                            MiddleName = "Grigorievich",
                            SecondName = "Kirichenko"
                        },
                        new
                        {
                            Id = 2,
                            FirstName = "Anna",
                            MiddleName = "Sergeevna",
                            SecondName = "Ushkalova"
                        });
                });

            modelBuilder.Entity("Reception.Server.Data.Entities.PersonAdditional", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<int?>("PhotoId")
                        .HasColumnType("INTEGER");

                    b.HasKey("Id");

                    b.ToTable("PersonAdditional", "Person");
                });

            modelBuilder.Entity("Reception.Server.Data.Entities.Post", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Comment")
                        .HasColumnType("TEXT");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("Post", "Person");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Name = "Brainfucker"
                        });
                });

            modelBuilder.Entity("Reception.Server.Data.Entities.Person", b =>
                {
                    b.HasOne("Reception.Server.Data.Entities.PersonAdditional", "AdditionalInfo")
                        .WithMany()
                        .HasForeignKey("AdditionalInfoId");

                    b.HasOne("Reception.Server.Data.Entities.Post", "Post")
                        .WithMany()
                        .HasForeignKey("PostId");

                    b.Navigation("AdditionalInfo");

                    b.Navigation("Post");
                });
#pragma warning restore 612, 618
        }
    }
}

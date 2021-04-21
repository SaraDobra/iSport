﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using iSportProjekti.Models;

namespace iSportProjekti.Migrations
{
    [DbContext(typeof(MyContext))]
    partial class MyContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "3.1.5")
                .HasAnnotation("Relational:MaxIdentifierLength", 64);

            modelBuilder.Entity("iSportProjekti.Models.Event", b =>
                {
                    b.Property<int>("EventId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<DateTime>("Date")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("Events")
                        .IsRequired()
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<string>("Location")
                        .IsRequired()
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.HasKey("EventId");

                    b.HasIndex("UserId");

                    b.ToTable("Events");
                });

            modelBuilder.Entity("iSportProjekti.Models.User", b =>
                {
                    b.Property<int>("UserId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("Birthdate")
                        .IsRequired()
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.HasKey("UserId");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("iSportProjekti.Models.UserEvent", b =>
                {
                    b.Property<string>("UserEventId")
                        .HasColumnType("varchar(255) CHARACTER SET utf8mb4");

                    b.Property<int>("EventId")
                        .HasColumnType("int");

                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.HasKey("UserEventId");

                    b.HasIndex("EventId");

                    b.HasIndex("UserId");

                    b.ToTable("UserEvents");
                });

            modelBuilder.Entity("iSportProjekti.Models.Event", b =>
                {
                    b.HasOne("iSportProjekti.Models.User", "Planner")
                        .WithMany("EventsPlanned")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("iSportProjekti.Models.UserEvent", b =>
                {
                    b.HasOne("iSportProjekti.Models.Event", null)
                        .WithMany("UserEvents")
                        .HasForeignKey("EventId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("iSportProjekti.Models.User", "Participants")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });
#pragma warning restore 612, 618
        }
    }
}

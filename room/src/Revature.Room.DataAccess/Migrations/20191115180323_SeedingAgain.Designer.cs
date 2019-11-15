﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;
using Revature.Room.DataAccess.Entities;

namespace Revature.Room.DataAccess.Migrations
{
    [DbContext(typeof(RoomServiceContext))]
    [Migration("20191115180323_SeedingAgain")]
    partial class SeedingAgain
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn)
                .HasAnnotation("ProductVersion", "3.0.0")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            modelBuilder.Entity("Revature.Room.DataAccess.Entities.Gender", b =>
                {
                    b.Property<string>("Type")
                        .HasColumnType("text");

                    b.HasKey("Type");

                    b.ToTable("Gender");

                    b.HasData(
                        new
                        {
                            Type = "Male"
                        },
                        new
                        {
                            Type = "Female"
                        },
                        new
                        {
                            Type = "NonBinary"
                        });
                });

            modelBuilder.Entity("Revature.Room.DataAccess.Entities.Room", b =>
                {
                    b.Property<Guid>("RoomID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<Guid>("ComplexID")
                        .HasColumnType("uuid");

                    b.Property<string>("GenderType")
                        .HasColumnType("text");

                    b.Property<DateTime>("LeaseEnd")
                        .HasColumnType("timestamp without time zone");

                    b.Property<DateTime>("LeaseStart")
                        .HasColumnType("timestamp without time zone");

                    b.Property<int>("NumberOfBeds")
                        .HasColumnType("integer");

                    b.Property<string>("RoomNumber")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("RoomTypeType")
                        .HasColumnType("text");

                    b.HasKey("RoomID");

                    b.HasIndex("GenderType");

                    b.HasIndex("RoomTypeType");

                    b.ToTable("Room");

                    b.HasData(
                        new
                        {
                            RoomID = new Guid("249e5358-169a-4bc6-aa0f-c054952456fd"),
                            ComplexID = new Guid("b5e050aa-6bfc-46ad-9a69-90b1f99ed606"),
                            GenderType = "Female",
                            LeaseEnd = new DateTime(2020, 2, 15, 0, 0, 0, 0, DateTimeKind.Local),
                            LeaseStart = new DateTime(2019, 11, 15, 12, 3, 23, 65, DateTimeKind.Local).AddTicks(256),
                            NumberOfBeds = 4,
                            RoomNumber = "2428B",
                            RoomTypeType = "Apartment"
                        });
                });

            modelBuilder.Entity("Revature.Room.DataAccess.Entities.RoomType", b =>
                {
                    b.Property<string>("Type")
                        .HasColumnType("text");

                    b.HasKey("Type");

                    b.ToTable("RoomType");

                    b.HasData(
                        new
                        {
                            Type = "Apartment"
                        },
                        new
                        {
                            Type = "Dormitory"
                        },
                        new
                        {
                            Type = "TownHouse"
                        });
                });

            modelBuilder.Entity("Revature.Room.DataAccess.Entities.Room", b =>
                {
                    b.HasOne("Revature.Room.DataAccess.Entities.Gender", "Gender")
                        .WithMany()
                        .HasForeignKey("GenderType");

                    b.HasOne("Revature.Room.DataAccess.Entities.RoomType", "RoomType")
                        .WithMany()
                        .HasForeignKey("RoomTypeType");
                });
#pragma warning restore 612, 618
        }
    }
}

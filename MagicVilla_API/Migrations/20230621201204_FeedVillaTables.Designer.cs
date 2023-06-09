﻿// <auto-generated />
using System;
using MagicVilla_API.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace MagicVilla_API.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20230621201204_FeedVillaTables")]
    partial class FeedVillaTables
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.5")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("MagicVilla_API.Models.Villa", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Amenity")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("CreationDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("Detail")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ImageUrl")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Occupants")
                        .HasColumnType("int");

                    b.Property<double>("Price")
                        .HasColumnType("float");

                    b.Property<int>("SquareMeters")
                        .HasColumnType("int");

                    b.Property<DateTime>("UpdateDate")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.ToTable("Villas");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Amenity = "",
                            CreationDate = new DateTime(2023, 6, 21, 15, 12, 3, 970, DateTimeKind.Local).AddTicks(6810),
                            Detail = "Detalle de la villa..",
                            ImageUrl = "",
                            Name = "Villa Real",
                            Occupants = 5,
                            Price = 200.0,
                            SquareMeters = 50,
                            UpdateDate = new DateTime(2023, 6, 21, 15, 12, 3, 970, DateTimeKind.Local).AddTicks(6820)
                        },
                        new
                        {
                            Id = 2,
                            Amenity = "",
                            CreationDate = new DateTime(2023, 6, 21, 15, 12, 3, 970, DateTimeKind.Local).AddTicks(6830),
                            Detail = "Detalle de la villa..",
                            ImageUrl = "",
                            Name = "Premium vista a la piscina",
                            Occupants = 4,
                            Price = 150.0,
                            SquareMeters = 40,
                            UpdateDate = new DateTime(2023, 6, 21, 15, 12, 3, 970, DateTimeKind.Local).AddTicks(6830)
                        });
                });
#pragma warning restore 612, 618
        }
    }
}

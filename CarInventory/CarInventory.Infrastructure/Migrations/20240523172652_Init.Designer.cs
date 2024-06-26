﻿// <auto-generated />
using System;
using CarInventory.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace CarInventory.Infrastructure.Migrations
{
    [DbContext(typeof(CarInventoryDbContext))]
    [Migration("20240523172652_Init")]
    partial class Init
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.5")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("CarInventory.Domain.Entities.Cars.Car", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Brand")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime2");

                    b.Property<string>("Model")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<decimal>("Price")
                        .HasColumnType("decimal(18,2)");

                    b.Property<string>("Status")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("UpdatedAt")
                        .HasColumnType("datetime2");

                    b.Property<string>("VIN")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Year")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("Cars");

                    b.HasData(
                        new
                        {
                            Id = new Guid("3ab41fd7-3588-4b67-bef7-4fb5f169c21b"),
                            Brand = "Toyota",
                            CreatedAt = new DateTime(2024, 5, 23, 17, 26, 52, 321, DateTimeKind.Utc).AddTicks(7935),
                            Model = "Camry",
                            Price = 24000.00m,
                            Status = "Available",
                            UpdatedAt = new DateTime(2024, 5, 23, 17, 26, 52, 321, DateTimeKind.Utc).AddTicks(7935),
                            VIN = "1HGBH41JXMN109186",
                            Year = 2020
                        },
                        new
                        {
                            Id = new Guid("648044af-905a-406a-bf8e-025996737a4a"),
                            Brand = "Honda",
                            CreatedAt = new DateTime(2024, 5, 23, 17, 26, 52, 321, DateTimeKind.Utc).AddTicks(7938),
                            Model = "Accord",
                            Price = 22000.00m,
                            Status = "Available",
                            UpdatedAt = new DateTime(2024, 5, 23, 17, 26, 52, 321, DateTimeKind.Utc).AddTicks(7938),
                            VIN = "2HGEH41JXMN109187",
                            Year = 2019
                        });
                });

            modelBuilder.Entity("CarInventory.Domain.Entities.Customers.Customer", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime2");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Phone")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("UpdatedAt")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.ToTable("Customers");

                    b.HasData(
                        new
                        {
                            Id = new Guid("19979b73-df6c-4d25-9b6c-7a749cd59bc1"),
                            CreatedAt = new DateTime(2024, 5, 23, 17, 26, 52, 321, DateTimeKind.Utc).AddTicks(8086),
                            Email = "john.doe@example.com",
                            Name = "John Doe",
                            Phone = "555-1234",
                            UpdatedAt = new DateTime(2024, 5, 23, 17, 26, 52, 321, DateTimeKind.Utc).AddTicks(8087)
                        },
                        new
                        {
                            Id = new Guid("a5e097ff-6d89-42b9-8c8e-5856e0cc5d7d"),
                            CreatedAt = new DateTime(2024, 5, 23, 17, 26, 52, 321, DateTimeKind.Utc).AddTicks(8089),
                            Email = "jane.smith@example.com",
                            Name = "Jane Smith",
                            Phone = "555-5678",
                            UpdatedAt = new DateTime(2024, 5, 23, 17, 26, 52, 321, DateTimeKind.Utc).AddTicks(8089)
                        });
                });

            modelBuilder.Entity("CarInventory.Domain.Entities.Sales.SalesRecord", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("CarId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime2");

                    b.Property<Guid>("CustomerId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("SaleDate")
                        .HasColumnType("datetime2");

                    b.Property<decimal>("SalePrice")
                        .HasColumnType("decimal(18,2)");

                    b.Property<DateTime>("UpdatedAt")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.HasIndex("CarId");

                    b.HasIndex("CustomerId");

                    b.ToTable("SalesRecords");
                });

            modelBuilder.Entity("CarInventory.Domain.Entities.Sales.SalesRecord", b =>
                {
                    b.HasOne("CarInventory.Domain.Entities.Cars.Car", "Car")
                        .WithMany()
                        .HasForeignKey("CarId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("CarInventory.Domain.Entities.Customers.Customer", "Customer")
                        .WithMany()
                        .HasForeignKey("CustomerId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Car");

                    b.Navigation("Customer");
                });
#pragma warning restore 612, 618
        }
    }
}

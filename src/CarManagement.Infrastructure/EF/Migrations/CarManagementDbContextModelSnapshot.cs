﻿// <auto-generated />
using System;
using CarManagement.Infrastructure.EF;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace CarManagement.Infrastructure.EF.Migrations
{
    [DbContext(typeof(CarManagementDbContext))]
    partial class CarManagementDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.8")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("CarManagement.Core.Users.Entities.Role", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<DateTimeOffset>("CreatedAt")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("character varying(50)");

                    b.HasKey("Id");

                    b.HasIndex("Name")
                        .IsUnique();

                    b.ToTable("Roles", (string)null);
                });

            modelBuilder.Entity("CarManagement.Core.Users.Entities.User", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<DateTimeOffset>("CreatedAt")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("character varying(100)");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("PhoneNumber")
                        .HasColumnType("text");

                    b.Property<Guid>("RoleId")
                        .HasColumnType("uuid");

                    b.Property<string>("Username")
                        .IsRequired()
                        .HasMaxLength(150)
                        .HasColumnType("character varying(150)");

                    b.HasKey("Id");

                    b.HasIndex("Email")
                        .IsUnique();

                    b.HasIndex("RoleId");

                    b.HasIndex("Username")
                        .IsUnique();

                    b.ToTable("Users", (string)null);
                });

            modelBuilder.Entity("CarManagement.Core.Vehicles.Entities.Vehicle", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<string>("Brand")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("character varying(50)");

                    b.Property<DateTimeOffset>("CreatedAt")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("Discriminator")
                        .IsRequired()
                        .HasMaxLength(13)
                        .HasColumnType("character varying(13)");

                    b.Property<double>("EngineCapacity")
                        .HasColumnType("double precision");

                    b.Property<int>("EnginePower")
                        .HasColumnType("integer");

                    b.Property<string>("FuelType")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("LicensePlate")
                        .IsRequired()
                        .HasMaxLength(9)
                        .HasColumnType("character varying(9)");

                    b.Property<string>("Model")
                        .IsRequired()
                        .HasMaxLength(150)
                        .HasColumnType("character varying(150)");

                    b.Property<Guid>("UserId")
                        .HasColumnType("uuid");

                    b.Property<string>("VIN")
                        .IsRequired()
                        .HasMaxLength(17)
                        .HasColumnType("character varying(17)");

                    b.Property<int>("Year")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("LicensePlate")
                        .IsUnique();

                    b.HasIndex("UserId");

                    b.HasIndex("VIN")
                        .IsUnique();

                    b.ToTable("Vehicles", (string)null);

                    b.HasDiscriminator().HasValue("Vehicle");

                    b.UseTphMappingStrategy();
                });

            modelBuilder.Entity("CarManagement.Core.Vehicles.Entities.Car", b =>
                {
                    b.HasBaseType("CarManagement.Core.Vehicles.Entities.Vehicle");

                    b.Property<int>("BodyType")
                        .HasColumnType("integer");

                    b.Property<int?>("GearboxType")
                        .HasColumnType("integer");

                    b.Property<int>("NumberOfDoors")
                        .HasColumnType("integer");

                    b.HasDiscriminator().HasValue("Car");
                });

            modelBuilder.Entity("CarManagement.Core.Vehicles.Entities.Motorcycle", b =>
                {
                    b.HasBaseType("CarManagement.Core.Vehicles.Entities.Vehicle");

                    b.Property<int?>("CoolingSystem")
                        .HasColumnType("integer");

                    b.Property<int?>("DriveType")
                        .HasColumnType("integer");

                    b.Property<int?>("MotorcycleType")
                        .HasColumnType("integer");

                    b.Property<int?>("NumberOfCylinders")
                        .HasColumnType("integer");

                    b.Property<int?>("NumberOfGears")
                        .HasColumnType("integer");

                    b.Property<int?>("SuspensionType")
                        .HasColumnType("integer");

                    b.HasDiscriminator().HasValue("Motorcycle");
                });

            modelBuilder.Entity("CarManagement.Core.Users.Entities.User", b =>
                {
                    b.HasOne("CarManagement.Core.Users.Entities.Role", "Role")
                        .WithMany("Users")
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Role");
                });

            modelBuilder.Entity("CarManagement.Core.Vehicles.Entities.Vehicle", b =>
                {
                    b.HasOne("CarManagement.Core.Users.Entities.User", "User")
                        .WithMany("Vehicles")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("User");
                });

            modelBuilder.Entity("CarManagement.Core.Users.Entities.Role", b =>
                {
                    b.Navigation("Users");
                });

            modelBuilder.Entity("CarManagement.Core.Users.Entities.User", b =>
                {
                    b.Navigation("Vehicles");
                });
#pragma warning restore 612, 618
        }
    }
}

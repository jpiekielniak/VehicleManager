﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;
using VehicleManager.Infrastructure.EF;

#nullable disable

namespace VehicleManager.Infrastructure.EF.Migrations
{
    [DbContext(typeof(VehicleManagerDbContext))]
    [Migration("20241023181759_RemoveRequiredPropertyInUser")]
    partial class RemoveRequiredPropertyInUser
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.8")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("VehicleManager.Core.Users.Entities.User", b =>
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

                    b.Property<string>("FirstName")
                        .HasMaxLength(150)
                        .HasColumnType("character varying(150)");

                    b.Property<string>("LastName")
                        .HasMaxLength(150)
                        .HasColumnType("character varying(150)");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("PhoneNumber")
                        .HasColumnType("text");

                    b.Property<int>("Role")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("Email")
                        .IsUnique();

                    b.ToTable("Users", (string)null);
                });

            modelBuilder.Entity("VehicleManager.Core.Vehicles.Entities.Cost", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<decimal>("Amount")
                        .HasColumnType("numeric");

                    b.Property<Guid>("ServiceId")
                        .HasColumnType("uuid");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.HasIndex("ServiceId");

                    b.ToTable("Costs", (string)null);
                });

            modelBuilder.Entity("VehicleManager.Core.Vehicles.Entities.Inspection", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<int>("InspectionType")
                        .HasColumnType("integer");

                    b.Property<DateTimeOffset?>("PerformDate")
                        .HasColumnType("timestamp with time zone");

                    b.Property<DateTimeOffset>("ScheduledDate")
                        .HasColumnType("timestamp with time zone");

                    b.Property<Guid>("ServiceBookId")
                        .HasColumnType("uuid");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.HasIndex("ServiceBookId");

                    b.ToTable("Inspections", (string)null);
                });

            modelBuilder.Entity("VehicleManager.Core.Vehicles.Entities.Insurance", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<string>("PolicyNumber")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Provider")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<DateTimeOffset>("ValidFrom")
                        .HasColumnType("timestamp with time zone");

                    b.Property<DateTimeOffset>("ValidTo")
                        .HasColumnType("timestamp with time zone");

                    b.Property<Guid>("VehicleId")
                        .HasColumnType("uuid");

                    b.HasKey("Id");

                    b.HasIndex("VehicleId");

                    b.ToTable("Insurances", (string)null);
                });

            modelBuilder.Entity("VehicleManager.Core.Vehicles.Entities.Service", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<DateTimeOffset>("Date")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("Description")
                        .HasColumnType("text");

                    b.Property<Guid>("ServiceBookId")
                        .HasColumnType("uuid");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.HasIndex("ServiceBookId");

                    b.ToTable("Services", (string)null);
                });

            modelBuilder.Entity("VehicleManager.Core.Vehicles.Entities.ServiceBook", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<Guid>("VehicleId")
                        .HasColumnType("uuid");

                    b.HasKey("Id");

                    b.HasIndex("VehicleId")
                        .IsUnique();

                    b.ToTable("ServiceBooks", (string)null);
                });

            modelBuilder.Entity("VehicleManager.Core.Vehicles.Entities.Vehicle", b =>
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

                    b.Property<double>("EngineCapacity")
                        .HasColumnType("double precision");

                    b.Property<int>("EnginePower")
                        .HasColumnType("integer");

                    b.Property<int>("FuelType")
                        .HasColumnType("integer");

                    b.Property<int>("GearboxType")
                        .HasColumnType("integer");

                    b.Property<string>("LicensePlate")
                        .IsRequired()
                        .HasMaxLength(9)
                        .HasColumnType("character varying(9)");

                    b.Property<string>("Model")
                        .IsRequired()
                        .HasMaxLength(150)
                        .HasColumnType("character varying(150)");

                    b.Property<Guid>("ServiceBookId")
                        .HasColumnType("uuid");

                    b.Property<Guid>("UserId")
                        .HasColumnType("uuid");

                    b.Property<string>("VIN")
                        .IsRequired()
                        .HasMaxLength(17)
                        .HasColumnType("character varying(17)");

                    b.Property<int>("VehicleType")
                        .HasColumnType("integer");

                    b.Property<int>("Year")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("Vehicles", (string)null);
                });

            modelBuilder.Entity("VehicleManager.Core.Vehicles.Entities.Cost", b =>
                {
                    b.HasOne("VehicleManager.Core.Vehicles.Entities.Service", "Service")
                        .WithMany("Costs")
                        .HasForeignKey("ServiceId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Service");
                });

            modelBuilder.Entity("VehicleManager.Core.Vehicles.Entities.Inspection", b =>
                {
                    b.HasOne("VehicleManager.Core.Vehicles.Entities.ServiceBook", "ServiceBook")
                        .WithMany("Inspections")
                        .HasForeignKey("ServiceBookId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("ServiceBook");
                });

            modelBuilder.Entity("VehicleManager.Core.Vehicles.Entities.Insurance", b =>
                {
                    b.HasOne("VehicleManager.Core.Vehicles.Entities.Vehicle", "Vehicle")
                        .WithMany("Insurances")
                        .HasForeignKey("VehicleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Vehicle");
                });

            modelBuilder.Entity("VehicleManager.Core.Vehicles.Entities.Service", b =>
                {
                    b.HasOne("VehicleManager.Core.Vehicles.Entities.ServiceBook", "ServiceBook")
                        .WithMany("Services")
                        .HasForeignKey("ServiceBookId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("ServiceBook");
                });

            modelBuilder.Entity("VehicleManager.Core.Vehicles.Entities.ServiceBook", b =>
                {
                    b.HasOne("VehicleManager.Core.Vehicles.Entities.Vehicle", "Vehicle")
                        .WithOne("ServiceBook")
                        .HasForeignKey("VehicleManager.Core.Vehicles.Entities.ServiceBook", "VehicleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Vehicle");
                });

            modelBuilder.Entity("VehicleManager.Core.Vehicles.Entities.Vehicle", b =>
                {
                    b.HasOne("VehicleManager.Core.Users.Entities.User", "User")
                        .WithMany("Vehicles")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("User");
                });

            modelBuilder.Entity("VehicleManager.Core.Users.Entities.User", b =>
                {
                    b.Navigation("Vehicles");
                });

            modelBuilder.Entity("VehicleManager.Core.Vehicles.Entities.Service", b =>
                {
                    b.Navigation("Costs");
                });

            modelBuilder.Entity("VehicleManager.Core.Vehicles.Entities.ServiceBook", b =>
                {
                    b.Navigation("Inspections");

                    b.Navigation("Services");
                });

            modelBuilder.Entity("VehicleManager.Core.Vehicles.Entities.Vehicle", b =>
                {
                    b.Navigation("Insurances");

                    b.Navigation("ServiceBook");
                });
#pragma warning restore 612, 618
        }
    }
}

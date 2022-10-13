﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using ResourceControlingAPI.Data;

#nullable disable

namespace ResourceControlingAPI.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    partial class ApplicationDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.10")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("ResourceControlingAPI.Models.Address", b =>
                {
                    b.Property<int>("AddressId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("AddressId"), 1L, 1);

                    b.Property<string>("City")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("FlatNumber")
                        .HasColumnType("int");

                    b.Property<int>("HouseNumber")
                        .HasColumnType("int");

                    b.Property<string>("StreetName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("AddressId");

                    b.ToTable("Addresses");
                });

            modelBuilder.Entity("ResourceControlingAPI.Models.Device", b =>
                {
                    b.Property<int>("DeviceId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("DeviceId"), 1L, 1);

                    b.Property<int>("AddressId")
                        .HasColumnType("int");

                    b.Property<int>("MeterId")
                        .HasColumnType("int");

                    b.HasKey("DeviceId");

                    b.HasIndex("AddressId");

                    b.HasIndex("MeterId")
                        .IsUnique();

                    b.ToTable("Devices");
                });

            modelBuilder.Entity("ResourceControlingAPI.Models.Meter", b =>
                {
                    b.Property<int>("MeterId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("MeterId"), 1L, 1);

                    b.Property<double>("MaximumAvailableValue")
                        .HasColumnType("float");

                    b.Property<int>("Number")
                        .HasColumnType("int");

                    b.Property<string>("Purpose")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("MeterId");

                    b.ToTable("Meters");
                });

            modelBuilder.Entity("ResourceControlingAPI.Models.MeterReading", b =>
                {
                    b.Property<int>("MeterReadingId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("MeterReadingId"), 1L, 1);

                    b.Property<DateTime?>("DateTimeReading")
                        .IsRequired()
                        .HasColumnType("datetime2");

                    b.Property<int>("MeterId")
                        .HasColumnType("int");

                    b.Property<int>("ReadingNumbers")
                        .HasColumnType("int");

                    b.HasKey("MeterReadingId");

                    b.HasIndex("MeterId");

                    b.ToTable("MeterReadings");
                });

            modelBuilder.Entity("ResourceControlingAPI.Models.Order", b =>
                {
                    b.Property<int>("OrderId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("OrderId"), 1L, 1);

                    b.Property<string>("City")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime?>("DateOfOrder")
                        .IsRequired()
                        .HasColumnType("datetime2");

                    b.Property<int>("NumberOfDevices")
                        .HasColumnType("int");

                    b.Property<string>("POstalOficeName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("PostalOficeNumber")
                        .HasColumnType("int");

                    b.Property<int>("RenterId")
                        .HasColumnType("int");

                    b.Property<int>("WarehouseId")
                        .HasColumnType("int");

                    b.HasKey("OrderId");

                    b.HasIndex("RenterId");

                    b.HasIndex("WarehouseId");

                    b.ToTable("Orders");
                });

            modelBuilder.Entity("ResourceControlingAPI.Models.Renter", b =>
                {
                    b.Property<int>("RenterID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("RenterID"), 1L, 1);

                    b.Property<int>("AddressId")
                        .HasColumnType("int");

                    b.Property<string>("EmailAddress")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("IsSubscribed")
                        .HasColumnType("bit");

                    b.Property<string>("Login")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PhoneNumber")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("SecondName")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("RenterID");

                    b.HasIndex("AddressId");

                    b.ToTable("Renters");
                });

            modelBuilder.Entity("ResourceControlingAPI.Models.Warehouse", b =>
                {
                    b.Property<int>("WarehouseId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("WarehouseId"), 1L, 1);

                    b.Property<int>("AvailableDevices")
                        .HasColumnType("int");

                    b.Property<int>("DevicePrice")
                        .HasColumnType("int");

                    b.HasKey("WarehouseId");

                    b.ToTable("Warehouses");
                });

            modelBuilder.Entity("ResourceControlingAPI.Models.Device", b =>
                {
                    b.HasOne("ResourceControlingAPI.Models.Address", "Address")
                        .WithMany("Devices")
                        .HasForeignKey("AddressId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("ResourceControlingAPI.Models.Meter", "Meter")
                        .WithOne("Device")
                        .HasForeignKey("ResourceControlingAPI.Models.Device", "MeterId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Address");

                    b.Navigation("Meter");
                });

            modelBuilder.Entity("ResourceControlingAPI.Models.MeterReading", b =>
                {
                    b.HasOne("ResourceControlingAPI.Models.Meter", "Meter")
                        .WithMany("meterReadings")
                        .HasForeignKey("MeterId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Meter");
                });

            modelBuilder.Entity("ResourceControlingAPI.Models.Order", b =>
                {
                    b.HasOne("ResourceControlingAPI.Models.Renter", "Renter")
                        .WithMany("Orders")
                        .HasForeignKey("RenterId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("ResourceControlingAPI.Models.Warehouse", "Warehouse")
                        .WithMany()
                        .HasForeignKey("WarehouseId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Renter");

                    b.Navigation("Warehouse");
                });

            modelBuilder.Entity("ResourceControlingAPI.Models.Renter", b =>
                {
                    b.HasOne("ResourceControlingAPI.Models.Address", "Address")
                        .WithMany("Renters")
                        .HasForeignKey("AddressId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Address");
                });

            modelBuilder.Entity("ResourceControlingAPI.Models.Address", b =>
                {
                    b.Navigation("Devices");

                    b.Navigation("Renters");
                });

            modelBuilder.Entity("ResourceControlingAPI.Models.Meter", b =>
                {
                    b.Navigation("Device")
                        .IsRequired();

                    b.Navigation("meterReadings");
                });

            modelBuilder.Entity("ResourceControlingAPI.Models.Renter", b =>
                {
                    b.Navigation("Orders");
                });
#pragma warning restore 612, 618
        }
    }
}

﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using VendorMVC.Data;

#nullable disable

namespace VendorMVC.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20230808065905_VendorAddressTableupdate")]
    partial class VendorAddressTableupdate
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.9")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("VendorMVC.Entities.City", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("StateID")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("StateID");

                    b.ToTable("Cities");
                });

            modelBuilder.Entity("VendorMVC.Entities.Country", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("Code")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("flag")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Country");
                });

            modelBuilder.Entity("VendorMVC.Entities.State", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("CountryID")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("CountryID");

                    b.ToTable("State");
                });

            modelBuilder.Entity("VendorMVC.Entities.Vendor", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int?>("CityID")
                        .IsRequired()
                        .HasColumnType("int");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Notes")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PhoneNumber")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("imageurl")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("CityID");

                    b.ToTable("Vendor");
                });

            modelBuilder.Entity("VendorMVC.Entities.VendorAddress", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("AddressLine1")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("AddressLine2")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("CityID")
                        .HasColumnType("int");

                    b.Property<string>("LandMark")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("PinCode")
                        .HasColumnType("int");

                    b.Property<int?>("VendorID")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("CityID");

                    b.HasIndex("VendorID");

                    b.ToTable("vendorAddresses");
                });

            modelBuilder.Entity("VendorMVC.Entities.City", b =>
                {
                    b.HasOne("VendorMVC.Entities.State", "State")
                        .WithMany()
                        .HasForeignKey("StateID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("State");
                });

            modelBuilder.Entity("VendorMVC.Entities.State", b =>
                {
                    b.HasOne("VendorMVC.Entities.Country", "Country")
                        .WithMany()
                        .HasForeignKey("CountryID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Country");
                });

            modelBuilder.Entity("VendorMVC.Entities.Vendor", b =>
                {
                    b.HasOne("VendorMVC.Entities.City", "City")
                        .WithMany()
                        .HasForeignKey("CityID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("City");
                });

            modelBuilder.Entity("VendorMVC.Entities.VendorAddress", b =>
                {
                    b.HasOne("VendorMVC.Entities.City", "City")
                        .WithMany()
                        .HasForeignKey("CityID");

                    b.HasOne("VendorMVC.Entities.Vendor", "Vendor")
                        .WithMany()
                        .HasForeignKey("VendorID");

                    b.Navigation("City");

                    b.Navigation("Vendor");
                });
#pragma warning restore 612, 618
        }
    }
}

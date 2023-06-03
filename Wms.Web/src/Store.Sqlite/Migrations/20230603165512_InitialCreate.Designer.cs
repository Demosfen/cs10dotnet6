﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Wms.Web.Store.Sqlite;

#nullable disable

namespace Wms.Web.Store.Sqlite.Migrations
{
    [DbContext(typeof(WarehouseDbContext))]
    [Migration("20230603165512_InitialCreate")]
    partial class InitialCreate
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder.HasAnnotation("ProductVersion", "7.0.5");

            modelBuilder.Entity("Wms.Web.Store.Entities.Concrete.Box", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("TEXT");

                    b.Property<DateTime?>("DeletedAt")
                        .HasColumnType("TEXT");

                    b.Property<double>("Depth")
                        .HasColumnType("REAL");

                    b.Property<DateTime?>("ExpiryDate")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<double>("Height")
                        .HasColumnType("REAL");

                    b.Property<Guid>("PaletteId")
                        .HasColumnType("TEXT");

                    b.Property<DateTime?>("ProductionDate")
                        .HasColumnType("TEXT");

                    b.Property<DateTime?>("UpdatedAt")
                        .HasColumnType("TEXT");

                    b.Property<double>("Volume")
                        .HasColumnType("REAL");

                    b.Property<double>("Weight")
                        .HasColumnType("REAL");

                    b.Property<double>("Width")
                        .HasColumnType("REAL");

                    b.HasKey("Id");

                    b.HasIndex("PaletteId");

                    b.ToTable("Boxes", (string)null);
                });

            modelBuilder.Entity("Wms.Web.Store.Entities.Concrete.Palette", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("TEXT");

                    b.Property<DateTime?>("DeletedAt")
                        .HasColumnType("TEXT");

                    b.Property<double>("Depth")
                        .HasColumnType("REAL");

                    b.Property<DateTime?>("ExpiryDate")
                        .HasColumnType("TEXT");

                    b.Property<double>("Height")
                        .HasColumnType("REAL");

                    b.Property<DateTime?>("UpdatedAt")
                        .HasColumnType("TEXT");

                    b.Property<double>("Volume")
                        .HasColumnType("REAL");

                    b.Property<Guid>("WarehouseId")
                        .HasColumnType("TEXT");

                    b.Property<double>("Weight")
                        .HasColumnType("REAL");

                    b.Property<double>("Width")
                        .HasColumnType("REAL");

                    b.HasKey("Id");

                    b.HasIndex("WarehouseId");

                    b.ToTable("Palettes", (string)null);
                });

            modelBuilder.Entity("Wms.Web.Store.Entities.Concrete.Warehouse", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("TEXT");

                    b.Property<DateTime?>("DeletedAt")
                        .HasColumnType("TEXT");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<DateTime?>("UpdatedAt")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.HasIndex("Name")
                        .IsUnique();

                    b.ToTable("Warehouses", (string)null);
                });

            modelBuilder.Entity("Wms.Web.Store.Entities.Concrete.Box", b =>
                {
                    b.HasOne("Wms.Web.Store.Entities.Concrete.Palette", null)
                        .WithMany("Boxes")
                        .HasForeignKey("PaletteId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();
                });

            modelBuilder.Entity("Wms.Web.Store.Entities.Concrete.Palette", b =>
                {
                    b.HasOne("Wms.Web.Store.Entities.Concrete.Warehouse", null)
                        .WithMany("Palettes")
                        .HasForeignKey("WarehouseId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();
                });

            modelBuilder.Entity("Wms.Web.Store.Entities.Concrete.Palette", b =>
                {
                    b.Navigation("Boxes");
                });

            modelBuilder.Entity("Wms.Web.Store.Entities.Concrete.Warehouse", b =>
                {
                    b.Navigation("Palettes");
                });
#pragma warning restore 612, 618
        }
    }
}

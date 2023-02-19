﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using WMS.WarehouseDbContext;

#nullable disable

namespace WMS.WarehouseDbContext.Migrations
{
    [DbContext(typeof(WarehouseDbContext))]
    partial class WarehouseDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder.HasAnnotation("ProductVersion", "7.0.3");

            modelBuilder.Entity("WMS.WarehouseDbContext.Entities.Box", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT");

                    b.Property<double>("Depth")
                        .HasColumnType("REAL");

                    b.Property<DateTime?>("ExpiryDate")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<double>("Height")
                        .HasColumnType("REAL");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("INTEGER");

                    b.Property<Guid>("PaletteId")
                        .HasColumnType("TEXT");

                    b.Property<DateTime?>("ProductionDate")
                        .HasColumnType("TEXT");

                    b.Property<double>("Volume")
                        .HasColumnType("REAL");

                    b.Property<double>("Weight")
                        .HasColumnType("REAL");

                    b.Property<double>("Width")
                        .HasColumnType("REAL");

                    b.HasKey("Id");

                    b.HasIndex("PaletteId");

                    b.ToTable("Boxes");
                });

            modelBuilder.Entity("WMS.WarehouseDbContext.Entities.Palette", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT");

                    b.Property<double>("Depth")
                        .HasColumnType("REAL");

                    b.Property<DateTime?>("ExpiryDate")
                        .HasColumnType("TEXT");

                    b.Property<double>("Height")
                        .HasColumnType("REAL");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("INTEGER");

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

            modelBuilder.Entity("WMS.WarehouseDbContext.Entities.Warehouse", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("Warehouses", (string)null);
                });

            modelBuilder.Entity("WMS.WarehouseDbContext.Entities.Box", b =>
                {
                    b.HasOne("WMS.WarehouseDbContext.Entities.Palette", "Palette")
                        .WithMany("Boxes")
                        .HasForeignKey("PaletteId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Palette");
                });

            modelBuilder.Entity("WMS.WarehouseDbContext.Entities.Palette", b =>
                {
                    b.HasOne("WMS.WarehouseDbContext.Entities.Warehouse", "Warehouse")
                        .WithMany("Palettes")
                        .HasForeignKey("WarehouseId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Warehouse");
                });

            modelBuilder.Entity("WMS.WarehouseDbContext.Entities.Palette", b =>
                {
                    b.Navigation("Boxes");
                });

            modelBuilder.Entity("WMS.WarehouseDbContext.Entities.Warehouse", b =>
                {
                    b.Navigation("Palettes");
                });
#pragma warning restore 612, 618
        }
    }
}

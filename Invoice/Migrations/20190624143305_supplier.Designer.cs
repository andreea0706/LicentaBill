﻿// <auto-generated />
using Invoice.Data.AppDataContext;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.EntityFrameworkCore.Storage.Internal;
using System;

namespace Invoice.Migrations
{
    [DbContext(typeof(InvoiceDbContext))]
    [Migration("20190624143305_supplier")]
    partial class supplier
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.0.2-rtm-10011")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("Invoice.Core.Entity.CustomerModel", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Address");

                    b.Property<DateTime>("CreateDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("Email");

                    b.Property<string>("FirmName");

                    b.Property<bool>("IsDelete");

                    b.Property<string>("Name")
                        .IsRequired();

                    b.Property<string>("Notes");

                    b.Property<string>("Phone")
                        .IsRequired();

                    b.Property<DateTime>("UpdateDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("cui");

                    b.Property<bool>("scpTVA");

                    b.HasKey("Id");

                    b.ToTable("Customer");
                });

            modelBuilder.Entity("Invoice.Core.Entity.ProductModel", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Code")
                        .IsRequired();

                    b.Property<DateTime>("CreateDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("Description");

                    b.Property<bool>("IsDelete");

                    b.Property<string>("Name")
                        .IsRequired();

                    b.Property<double>("Price");

                    b.Property<int>("Stoc");

                    b.Property<DateTime>("UpdateDate")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.ToTable("Product");
                });

            modelBuilder.Entity("Invoice.Core.Entity.SalesItemsModel", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<double>("Amount");

                    b.Property<DateTime>("CreateDate")
                        .HasColumnType("datetime2");

                    b.Property<bool>("IsDelete");

                    b.Property<string>("Name");

                    b.Property<double>("Price");

                    b.Property<int>("Quantity");

                    b.Property<int>("SalesId");

                    b.Property<DateTime>("UpdateDate")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.HasIndex("SalesId");

                    b.ToTable("SalesItems");
                });

            modelBuilder.Entity("Invoice.Core.Entity.SalesModel", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("CreateDate")
                        .HasColumnType("datetime2");

                    b.Property<int>("CustomerId");

                    b.Property<double?>("Discount");

                    b.Property<double>("GrandTotal");

                    b.Property<bool>("IsDelete");

                    b.Property<string>("Notes");

                    b.Property<string>("PaymentMethod");

                    b.Property<string>("SaleCode");

                    b.Property<DateTime>("SalesDate");

                    b.Property<string>("Status");

                    b.Property<double>("Total");

                    b.Property<DateTime>("UpdateDate")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.HasIndex("CustomerId");

                    b.ToTable("Sales");
                });

            modelBuilder.Entity("Invoice.Core.Entity.StoreSettingModel", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Address")
                        .IsRequired();

                    b.Property<DateTime>("CreateDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("Currency")
                        .IsRequired();

                    b.Property<string>("Email")
                        .IsRequired();

                    b.Property<bool>("IsDelete");

                    b.Property<string>("Logo");

                    b.Property<string>("Phone")
                        .IsRequired();

                    b.Property<string>("StoreName")
                        .IsRequired();

                    b.Property<DateTime>("UpdateDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("Web")
                        .IsRequired();

                    b.HasKey("Id");

                    b.ToTable("StoreSetting");
                });

            modelBuilder.Entity("Invoice.Core.Entity.SupplierModel", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Address");

                    b.Property<DateTime>("CreateDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("Email");

                    b.Property<string>("FirmName");

                    b.Property<bool>("IsDelete");

                    b.Property<string>("Name")
                        .IsRequired();

                    b.Property<string>("Notes");

                    b.Property<string>("Phone")
                        .IsRequired();

                    b.Property<DateTime>("UpdateDate")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.ToTable("Supplier");
                });

            modelBuilder.Entity("Invoice.Core.Entity.UserModel", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Address");

                    b.Property<string>("ConfirmPassword");

                    b.Property<DateTime>("CreateDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("Email");

                    b.Property<string>("FirmName");

                    b.Property<bool>("IsDelete");

                    b.Property<string>("Password")
                        .HasMaxLength(10);

                    b.Property<DateTime>("UpdateDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("cui");

                    b.Property<bool>("scpTVA");

                    b.HasKey("Id");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("Invoice.Core.Entity.SalesItemsModel", b =>
                {
                    b.HasOne("Invoice.Core.Entity.SalesModel", "SalesModel")
                        .WithMany("Items")
                        .HasForeignKey("SalesId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Invoice.Core.Entity.SalesModel", b =>
                {
                    b.HasOne("Invoice.Core.Entity.CustomerModel", "CustomerModel")
                        .WithMany()
                        .HasForeignKey("CustomerId")
                        .OnDelete(DeleteBehavior.Cascade);
                });
#pragma warning restore 612, 618
        }
    }
}

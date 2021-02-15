﻿// <auto-generated />
using System;
using CrudApiAspNetCoreSql.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace CrudApiAspNetCoreSql.Migrations
{
    [DbContext(typeof(AppDbContext))]
    partial class AppDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .UseIdentityColumns()
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.3");

            modelBuilder.Entity("CrudApiAspNetCoreSql.Models.Category", b =>
                {
                    b.Property<int>("CategoryId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .UseIdentityColumn();

                    b.Property<DateTime>("CategoryCreateDate")
                        .ValueGeneratedOnAddOrUpdate()
                        .HasColumnType("datetime2");

                    b.Property<string>("CategoryName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("CategoryShortName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("CategorySpecialInstructions")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("CategoryUrl")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("CategoryId");

                    b.ToTable("Categories");
                });

            modelBuilder.Entity("CrudApiAspNetCoreSql.Models.MenuItem", b =>
                {
                    b.Property<int>("MenuItemID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .UseIdentityColumn();

                    b.Property<int>("MenuItemCategoryIdFk")
                        .HasColumnType("int");

                    b.Property<string>("MenuItemDescription")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("MenuItemLargePortionName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("MenuItemName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<decimal>("MenuItemPriceLarge")
                        .HasColumnType("decimal(18,2)");

                    b.Property<decimal>("MenuItemPriceSmall")
                        .HasColumnType("decimal(18,2)");

                    b.Property<string>("MenuItemShortName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("MenuItemSmallPortionName")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("MenuItemID");

                    b.HasIndex("MenuItemCategoryIdFk");

                    b.ToTable("MenuItems");
                });

            modelBuilder.Entity("CrudApiAspNetCoreSql.Models.User", b =>
                {
                    b.Property<int>("UserId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .UseIdentityColumn();

                    b.Property<DateTime>("UserCreateDate")
                        .ValueGeneratedOnAddOrUpdate()
                        .HasColumnType("datetime2");

                    b.Property<string>("UserEmail")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UserFullName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UserName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UserPassword")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("UserId");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("CrudApiAspNetCoreSql.Models.MenuItem", b =>
                {
                    b.HasOne("CrudApiAspNetCoreSql.Models.Category", "MenuItemCategory")
                        .WithMany("CategoryMenuItemsList")
                        .HasForeignKey("MenuItemCategoryIdFk")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("MenuItemCategory");
                });

            modelBuilder.Entity("CrudApiAspNetCoreSql.Models.Category", b =>
                {
                    b.Navigation("CategoryMenuItemsList");
                });
#pragma warning restore 612, 618
        }
    }
}

﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using ProductAPI.Models;

#nullable disable

namespace ProductAPI.Migrations
{
    [DbContext(typeof(dbProductsContext))]
    [Migration("20240907152914_AddBookTable")]
    partial class AddBookTable
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.8")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("ProductAPI.Models.Category", b =>
                {
                    b.Property<string>("CatId")
                        .HasMaxLength(50)
                        .IsUnicode(false)
                        .HasColumnType("varchar(50)");

                    b.Property<string>("CatName")
                        .IsRequired()
                        .HasMaxLength(50)
                        .IsUnicode(false)
                        .HasColumnType("varchar(50)");

                    b.HasKey("CatId")
                        .HasName("PK__Category__6A1C8AFAB0F5DD60");

                    b.ToTable("Category");
                });

            modelBuilder.Entity("ProductAPI.Models.sProduct", b =>
                {
                    b.Property<string>("PId")
                        .HasMaxLength(10)
                        .IsUnicode(false)
                        .HasColumnType("varchar(10)");

                    b.Property<string>("CatId")
                        .HasMaxLength(50)
                        .IsUnicode(false)
                        .HasColumnType("varchar(50)");

                    b.Property<string>("PName")
                        .HasMaxLength(50)
                        .IsUnicode(false)
                        .HasColumnType("varchar(50)");

                    b.Property<int?>("QTY")
                        .HasColumnType("int");

                    b.HasKey("PId")
                        .HasName("PK__sProduct__C5775540A21A559D");

                    b.HasIndex("CatId");

                    b.ToTable("sProduct");
                });

            modelBuilder.Entity("ProductAPI.Models.sProduct", b =>
                {
                    b.HasOne("ProductAPI.Models.Category", "Cat")
                        .WithMany("sProduct")
                        .HasForeignKey("CatId")
                        .HasConstraintName("FK_Category");

                    b.Navigation("Cat");
                });

            modelBuilder.Entity("ProductAPI.Models.Category", b =>
                {
                    b.Navigation("sProduct");
                });
#pragma warning restore 612, 618
        }
    }
}

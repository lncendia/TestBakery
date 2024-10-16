﻿// <auto-generated />
using System;
using Bakery.Infrastructure.Storage.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Bakery.Infrastructure.Storage.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20241016132542_Initial")]
    partial class Initial
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.0")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("Bakery.Domain.Buns.Bun", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<DateTime>("BakeTime")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("BunType")
                        .IsRequired()
                        .HasMaxLength(200)
                        .HasColumnType("character varying(200)");

                    b.Property<int>("ControlSellHours")
                        .HasColumnType("integer");

                    b.Property<decimal>("InitialCost")
                        .HasColumnType("numeric");

                    b.Property<int>("SellHours")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.ToTable("Buns", (string)null);

                    b.HasDiscriminator<string>("BunType").HasValue("Bun");

                    b.UseTphMappingStrategy();
                });

            modelBuilder.Entity("Bakery.Domain.Buns.Baguette", b =>
                {
                    b.HasBaseType("Bakery.Domain.Buns.Bun");

                    b.HasDiscriminator().HasValue("Baguette");
                });

            modelBuilder.Entity("Bakery.Domain.Buns.Croissant", b =>
                {
                    b.HasBaseType("Bakery.Domain.Buns.Bun");

                    b.HasDiscriminator().HasValue("Croissant");
                });

            modelBuilder.Entity("Bakery.Domain.Buns.Loaf", b =>
                {
                    b.HasBaseType("Bakery.Domain.Buns.Bun");

                    b.HasDiscriminator().HasValue("Loaf");
                });

            modelBuilder.Entity("Bakery.Domain.Buns.Pretzel", b =>
                {
                    b.HasBaseType("Bakery.Domain.Buns.Bun");

                    b.HasDiscriminator().HasValue("Pretzel");
                });

            modelBuilder.Entity("Bakery.Domain.Buns.Smetannik", b =>
                {
                    b.HasBaseType("Bakery.Domain.Buns.Bun");

                    b.HasDiscriminator().HasValue("Smetannik");
                });
#pragma warning restore 612, 618
        }
    }
}

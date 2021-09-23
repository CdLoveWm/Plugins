﻿// <auto-generated />
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Plugin.Demo.Hangfire.Context;

namespace Plugin.Demo.Hangfire.Migrations
{
    [DbContext(typeof(DataContext))]
    [Migration("20210923012042_initdb")]
    partial class initdb
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 64)
                .HasAnnotation("ProductVersion", "5.0.10");

            modelBuilder.Entity("Plugin.Demo.Hangfire.Models.JobConfig", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("Corn")
                        .HasColumnType("longtext");

                    b.HasKey("Id");

                    b.ToTable("JobConfig");
                });
#pragma warning restore 612, 618
        }
    }
}
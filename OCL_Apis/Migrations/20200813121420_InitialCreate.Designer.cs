﻿// <auto-generated />
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using OCL_Apis.Models;

namespace OCL_Apis.Migrations
{
    [DbContext(typeof(StockManagerContext))]
    [Migration("20200813121420_InitialCreate")]
    partial class InitialCreate
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "3.1.6")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("OCL_Apis.Models.Resource", b =>
                {
                    b.Property<long>("Id")
                        .HasColumnType("bigint");

                    b.Property<int>("Bad")
                        .HasColumnType("int");

                    b.Property<string>("Batch")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Brand")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("CanType")
                        .HasColumnType("int");

                    b.Property<int>("Good")
                        .HasColumnType("int");

                    b.Property<string>("Note")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Order")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Rejection")
                        .HasColumnType("int");

                    b.Property<int>("ResourceStatus")
                        .HasColumnType("int");

                    b.Property<int>("Tinplate")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("Resources");
                });
#pragma warning restore 612, 618
        }
    }
}

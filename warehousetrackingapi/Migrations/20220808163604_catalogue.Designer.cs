﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using warehousetrackingapi.Models;

#nullable disable

namespace TrackingDemoApi.Migrations
{
    [DbContext(typeof(Context))]
    [Migration("20220808163604_catalogue")]
    partial class catalogue
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.2")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("warehousetrackingapi.Models.AssetModel", b =>
                {
                    b.Property<Guid>("Guid")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Active")
                        .HasMaxLength(10)
                        .HasColumnType("varchar(10)");

                    b.Property<string>("AssetImage")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("AssetImagePath")
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid?>("Category")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Createdby")
                        .HasMaxLength(50)
                        .HasColumnType("varchar(50)");

                    b.Property<DateTime>("Datecreated")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("Dateupdated")
                        .HasColumnType("datetime2");

                    b.Property<string>("Description")
                        .HasMaxLength(500)
                        .HasColumnType("varchar(500)");

                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<bool>("IsAllowedToGoOut")
                        .HasColumnType("bit");

                    b.Property<bool>("IsConsumed")
                        .HasColumnType("bit");

                    b.Property<string>("Itemname")
                        .HasMaxLength(200)
                        .HasColumnType("varchar(200)");

                    b.Property<Guid>("Location")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Manufacturer")
                        .HasMaxLength(200)
                        .HasColumnType("varchar(200)");

                    b.Property<string>("Model")
                        .HasMaxLength(200)
                        .HasColumnType("varchar(200)");

                    b.Property<string>("Status")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Tag")
                        .HasMaxLength(200)
                        .HasColumnType("varchar(200)");

                    b.Property<Guid?>("Type")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Updatedby")
                        .HasMaxLength(50)
                        .HasColumnType("varchar(50)");

                    b.HasKey("Guid");

                    b.ToTable("Assets");
                });

            modelBuilder.Entity("warehousetrackingapi.Models.CategoryModel", b =>
                {
                    b.Property<Guid>("Guid")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Active")
                        .HasMaxLength(10)
                        .HasColumnType("varchar(10)");

                    b.Property<string>("Category")
                        .HasMaxLength(200)
                        .HasColumnType("varchar(200)");

                    b.Property<string>("Createdby")
                        .HasMaxLength(50)
                        .HasColumnType("varchar(50)");

                    b.Property<DateTime>("Datecreated")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("Dateupdated")
                        .HasColumnType("datetime2");

                    b.Property<string>("Description")
                        .HasMaxLength(200)
                        .HasColumnType("varchar(200)");

                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("Updatedby")
                        .HasMaxLength(50)
                        .HasColumnType("varchar(50)");

                    b.HasKey("Guid");

                    b.ToTable("Categories");
                });

            modelBuilder.Entity("warehousetrackingapi.Models.CheckInOutModel", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("Createdby")
                        .HasMaxLength(50)
                        .HasColumnType("varchar(50)");

                    b.Property<DateTime>("Datecreated")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("Dateupdated")
                        .HasColumnType("datetime2");

                    b.Property<string>("Employee")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Mode")
                        .HasMaxLength(50)
                        .HasColumnType("varchar(50)");

                    b.Property<string>("Tag")
                        .HasMaxLength(200)
                        .HasColumnType("varchar(200)");

                    b.Property<long>("TransactionNo")
                        .HasColumnType("bigint");

                    b.Property<string>("Updatedby")
                        .HasMaxLength(50)
                        .HasColumnType("varchar(50)");

                    b.HasKey("Id");

                    b.ToTable("CheckInOutTransactions");
                });

            modelBuilder.Entity("warehousetrackingapi.Models.EmployeeModel", b =>
                {
                    b.Property<Guid>("Guid")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Active")
                        .HasMaxLength(10)
                        .HasColumnType("varchar(10)");

                    b.Property<string>("Createdby")
                        .HasMaxLength(50)
                        .HasColumnType("varchar(50)");

                    b.Property<DateTime>("Datecreated")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("Dateupdated")
                        .HasColumnType("datetime2");

                    b.Property<string>("Email")
                        .HasMaxLength(200)
                        .HasColumnType("varchar(200)");

                    b.Property<string>("Employeeid")
                        .HasMaxLength(200)
                        .HasColumnType("varchar(200)");

                    b.Property<string>("Firstname")
                        .HasMaxLength(200)
                        .HasColumnType("varchar(200)");

                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("Lastname")
                        .HasMaxLength(200)
                        .HasColumnType("varchar(200)");

                    b.Property<string>("Middlename")
                        .HasMaxLength(200)
                        .HasColumnType("varchar(200)");

                    b.Property<string>("Tag")
                        .HasMaxLength(200)
                        .HasColumnType("varchar(200)");

                    b.Property<string>("Updatedby")
                        .HasMaxLength(50)
                        .HasColumnType("varchar(50)");

                    b.HasKey("Guid");

                    b.ToTable("Employees");
                });

            modelBuilder.Entity("warehousetrackingapi.Models.LocationModel", b =>
                {
                    b.Property<Guid>("Guid")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Active")
                        .HasMaxLength(10)
                        .HasColumnType("varchar(10)");

                    b.Property<string>("Createdby")
                        .HasMaxLength(50)
                        .HasColumnType("varchar(50)");

                    b.Property<DateTime>("Datecreated")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("Dateupdated")
                        .HasColumnType("datetime2");

                    b.Property<string>("Description")
                        .HasMaxLength(500)
                        .HasColumnType("varchar(500)");

                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("Location")
                        .HasMaxLength(500)
                        .HasColumnType("varchar(500)");

                    b.Property<string>("Updatedby")
                        .HasMaxLength(50)
                        .HasColumnType("varchar(50)");

                    b.HasKey("Guid");

                    b.ToTable("Location");
                });

            modelBuilder.Entity("warehousetrackingapi.Models.LogModel", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("Createdby")
                        .HasMaxLength(50)
                        .HasColumnType("varchar(50)");

                    b.Property<DateTime>("Datecreated")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("Dateupdated")
                        .HasColumnType("datetime2");

                    b.Property<string>("Employee")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Mode")
                        .HasMaxLength(50)
                        .HasColumnType("varchar(50)");

                    b.Property<string>("Tag")
                        .HasMaxLength(200)
                        .HasColumnType("varchar(200)");

                    b.Property<long>("TransactionNo")
                        .HasColumnType("bigint");

                    b.Property<string>("Updatedby")
                        .HasMaxLength(50)
                        .HasColumnType("varchar(50)");

                    b.HasKey("Id");

                    b.ToTable("Logs");
                });

            modelBuilder.Entity("warehousetrackingapi.Models.ORSessionItemsModel", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<int>("AssetId")
                        .HasColumnType("int");

                    b.Property<DateTime>("Datecreated")
                        .HasColumnType("datetime2");

                    b.Property<int>("ORSessionId")
                        .HasColumnType("int");

                    b.Property<string>("Tag")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("ORSessionItems");
                });

            modelBuilder.Entity("warehousetrackingapi.Models.ORSessionModel", b =>
                {
                    b.Property<Guid>("Guid")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Createdby")
                        .HasMaxLength(50)
                        .HasColumnType("varchar(50)");

                    b.Property<DateTime>("Datecreated")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("Dateupdated")
                        .HasColumnType("datetime2");

                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("ORNo")
                        .HasMaxLength(100)
                        .HasColumnType("varchar(100)");

                    b.Property<string>("PatientNo")
                        .HasMaxLength(50)
                        .HasColumnType("varchar(50)");

                    b.Property<int>("Reader")
                        .HasColumnType("int");

                    b.Property<string>("Status")
                        .HasMaxLength(50)
                        .HasColumnType("varchar(50)");

                    b.Property<string>("Updatedby")
                        .HasMaxLength(50)
                        .HasColumnType("varchar(50)");

                    b.HasKey("Guid");

                    b.ToTable("ORSession");
                });

            modelBuilder.Entity("warehousetrackingapi.Models.ReaderConfigModel", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("Action")
                        .HasMaxLength(10)
                        .HasColumnType("varchar(10)");

                    b.Property<string>("Active")
                        .HasMaxLength(10)
                        .HasColumnType("varchar(10)");

                    b.Property<string>("Antenna")
                        .HasMaxLength(200)
                        .HasColumnType("varchar(200)");

                    b.Property<string>("Hostname")
                        .HasMaxLength(100)
                        .HasColumnType("varchar(100)");

                    b.Property<string>("IpAddress")
                        .HasMaxLength(100)
                        .HasColumnType("varchar(100)");

                    b.Property<Guid>("Location")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Reader")
                        .HasMaxLength(200)
                        .HasColumnType("varchar(200)");

                    b.HasKey("Id");

                    b.ToTable("ReaderConfig");
                });

            modelBuilder.Entity("warehousetrackingapi.Models.ReaderModel", b =>
                {
                    b.Property<Guid>("Guid")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Active")
                        .HasMaxLength(10)
                        .HasColumnType("varchar(10)");

                    b.Property<string>("Createdby")
                        .HasMaxLength(50)
                        .HasColumnType("varchar(50)");

                    b.Property<DateTime>("Datecreated")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("Dateupdated")
                        .HasColumnType("datetime2");

                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("Ipaddress")
                        .HasMaxLength(200)
                        .HasColumnType("varchar(200)");

                    b.Property<string>("Port")
                        .HasMaxLength(200)
                        .HasColumnType("varchar(200)");

                    b.Property<string>("Readername")
                        .HasMaxLength(200)
                        .HasColumnType("varchar(200)");

                    b.Property<string>("Updatedby")
                        .HasMaxLength(50)
                        .HasColumnType("varchar(50)");

                    b.HasKey("Guid");

                    b.ToTable("Readers");
                });

            modelBuilder.Entity("warehousetrackingapi.Models.StationModel", b =>
                {
                    b.Property<Guid>("Guid")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Active")
                        .HasMaxLength(10)
                        .HasColumnType("varchar(10)");

                    b.Property<string>("Createdby")
                        .HasMaxLength(50)
                        .HasColumnType("varchar(50)");

                    b.Property<DateTime>("Datecreated")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("Dateupdated")
                        .HasColumnType("datetime2");

                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<Guid>("Reader")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Station")
                        .HasMaxLength(200)
                        .HasColumnType("varchar(200)");

                    b.Property<string>("Updatedby")
                        .HasMaxLength(50)
                        .HasColumnType("varchar(50)");

                    b.HasKey("Guid");

                    b.ToTable("Stations");
                });

            modelBuilder.Entity("warehousetrackingapi.Models.TypeModel", b =>
                {
                    b.Property<Guid>("Guid")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Active")
                        .HasMaxLength(10)
                        .HasColumnType("varchar(10)");

                    b.Property<string>("Createdby")
                        .HasMaxLength(50)
                        .HasColumnType("varchar(50)");

                    b.Property<DateTime>("Datecreated")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("Dateupdated")
                        .HasColumnType("datetime2");

                    b.Property<string>("Description")
                        .HasMaxLength(200)
                        .HasColumnType("varchar(200)");

                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("Type")
                        .HasMaxLength(200)
                        .HasColumnType("varchar(200)");

                    b.Property<string>("Updatedby")
                        .HasMaxLength(50)
                        .HasColumnType("varchar(50)");

                    b.HasKey("Guid");

                    b.ToTable("Types");
                });

            modelBuilder.Entity("warehousetrackingapi.Models.WorkOrderItemsModel", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<Guid>("AssetId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("DateCheckout")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("DateReturned")
                        .HasColumnType("datetime2");

                    b.Property<string>("EmployeeId")
                        .HasMaxLength(50)
                        .HasColumnType("varchar(50)");

                    b.Property<string>("ItemStatus")
                        .HasMaxLength(50)
                        .HasColumnType("varchar(50)");

                    b.Property<string>("ReturnedBy")
                        .HasMaxLength(50)
                        .HasColumnType("varchar(50)");

                    b.Property<string>("Tag")
                        .HasMaxLength(200)
                        .HasColumnType("varchar(200)");

                    b.Property<string>("WOStatus")
                        .HasMaxLength(50)
                        .HasColumnType("varchar(50)");

                    b.Property<int>("WorkOrderId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("WorkOrderItems");
                });

            modelBuilder.Entity("warehousetrackingapi.Models.WorkOrderModel", b =>
                {
                    b.Property<Guid>("Guid")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Createdby")
                        .HasMaxLength(50)
                        .HasColumnType("varchar(50)");

                    b.Property<DateTime>("Datecreated")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("Dateupdated")
                        .HasColumnType("datetime2");

                    b.Property<string>("Description")
                        .HasMaxLength(200)
                        .HasColumnType("varchar(200)");

                    b.Property<string>("EmployeeId")
                        .HasMaxLength(200)
                        .HasColumnType("varchar(200)");

                    b.Property<DateTime>("LastCheckOut")
                        .HasColumnType("datetime2");

                    b.Property<string>("Status")
                        .HasMaxLength(50)
                        .HasColumnType("varchar(50)");

                    b.Property<string>("Updatedby")
                        .HasMaxLength(50)
                        .HasColumnType("varchar(50)");

                    b.Property<string>("WorkOrderType")
                        .HasMaxLength(200)
                        .HasColumnType("varchar(200)");

                    b.Property<string>("Workorderdate")
                        .IsRequired()
                        .HasMaxLength(200)
                        .HasColumnType("varchar(200)");

                    b.Property<int>("Workorderid")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Workorderid"), 1L, 1);

                    b.HasKey("Guid");

                    b.ToTable("WorkOrders");
                });
#pragma warning restore 612, 618
        }
    }
}

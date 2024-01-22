﻿// <auto-generated />
using System;
using Bulldog.Infrastructure.EF;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace Bulldog.Infrastructure.Migrations
{
    [DbContext(typeof(BulldogDbContext))]
    [Migration("20240120165324_initial")]
    partial class initial
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.13")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("Bulldog.Core.Domain.AvailableDate", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<int>("DayOfWeek")
                        .HasColumnType("int");

                    b.Property<Guid>("EmployeeId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<bool>("IsOpen")
                        .HasColumnType("bit");

                    b.HasKey("Id");

                    b.HasIndex("EmployeeId");

                    b.ToTable("AvailableDates");
                });

            modelBuilder.Entity("Bulldog.Core.Domain.AvailableHour", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("EmployeeId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("Hour")
                        .HasColumnType("datetime2");

                    b.Property<bool>("IsAvailable")
                        .HasColumnType("bit");

                    b.HasKey("Id");

                    b.HasIndex("EmployeeId");

                    b.ToTable("AvailableHours");
                });

            modelBuilder.Entity("Bulldog.Core.Domain.Break", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("AvailableDateId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<TimeSpan>("EndTime")
                        .HasColumnType("time");

                    b.Property<TimeSpan>("StartTime")
                        .HasColumnType("time");

                    b.HasKey("Id");

                    b.HasIndex("AvailableDateId");

                    b.ToTable("Breaks");
                });

            modelBuilder.Entity("Bulldog.Core.Domain.Employee", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid>("UserId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.ToTable("Employees");
                });

            modelBuilder.Entity("Bulldog.Core.Domain.Reservation", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("Date")
                        .HasColumnType("datetime2");

                    b.Property<Guid>("EmployeeId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("ServiceId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<bool>("Status")
                        .HasColumnType("bit");

                    b.HasKey("Id");

                    b.ToTable("Reservations");
                });

            modelBuilder.Entity("Bulldog.Core.Domain.Service", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<int>("Duration")
                        .HasColumnType("int");

                    b.Property<Guid>("EmployeeId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<decimal>("Price")
                        .HasColumnType("decimal(18,2)");

                    b.HasKey("Id");

                    b.HasIndex("EmployeeId");

                    b.ToTable("Services");
                });

            modelBuilder.Entity("Bulldog.Core.Domain.User", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime2");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Salt")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Username")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("Bulldog.Core.Domain.AvailableDate", b =>
                {
                    b.HasOne("Bulldog.Core.Domain.Employee", null)
                        .WithMany("AvailableDates")
                        .HasForeignKey("EmployeeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.OwnsOne("Bulldog.Core.Domain.WorkingHours", "WorkingHours", b1 =>
                        {
                            b1.Property<Guid>("AvailableDateId")
                                .HasColumnType("uniqueidentifier");

                            b1.Property<TimeSpan>("EndTime")
                                .HasColumnType("time");

                            b1.Property<TimeSpan>("StartTime")
                                .HasColumnType("time");

                            b1.HasKey("AvailableDateId");

                            b1.ToTable("AvailableDates");

                            b1.WithOwner()
                                .HasForeignKey("AvailableDateId");
                        });

                    b.Navigation("WorkingHours");
                });

            modelBuilder.Entity("Bulldog.Core.Domain.AvailableHour", b =>
                {
                    b.HasOne("Bulldog.Core.Domain.Employee", null)
                        .WithMany("AvailableHours")
                        .HasForeignKey("EmployeeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Bulldog.Core.Domain.Break", b =>
                {
                    b.HasOne("Bulldog.Core.Domain.AvailableDate", null)
                        .WithMany("Breaks")
                        .HasForeignKey("AvailableDateId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Bulldog.Core.Domain.Service", b =>
                {
                    b.HasOne("Bulldog.Core.Domain.Employee", null)
                        .WithMany("Services")
                        .HasForeignKey("EmployeeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Bulldog.Core.Domain.AvailableDate", b =>
                {
                    b.Navigation("Breaks");
                });

            modelBuilder.Entity("Bulldog.Core.Domain.Employee", b =>
                {
                    b.Navigation("AvailableDates");

                    b.Navigation("AvailableHours");

                    b.Navigation("Services");
                });
#pragma warning restore 612, 618
        }
    }
}
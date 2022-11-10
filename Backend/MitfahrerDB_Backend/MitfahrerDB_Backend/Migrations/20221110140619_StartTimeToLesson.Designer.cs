﻿// <auto-generated />
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using MitfahrerDB_Backend;

#nullable disable

namespace MitfahrerDB_Backend.Migrations
{
    [DbContext(typeof(DataBaseContext))]
    [Migration("20221110140619_StartTimeToLesson")]
    partial class StartTimeToLesson
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder.HasAnnotation("ProductVersion", "6.0.10");

            modelBuilder.Entity("MitfahrerDB_Backend.Models.Gender", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("Genders");
                });

            modelBuilder.Entity("MitfahrerDB_Backend.Models.Location", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Latitude")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("Longitude")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("Locations");
                });

            modelBuilder.Entity("MitfahrerDB_Backend.Models.Trip", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Address")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<int>("AvailableSeats")
                        .HasColumnType("INTEGER");

                    b.Property<int>("DriverId")
                        .HasColumnType("INTEGER");

                    b.Property<int>("Lesson")
                        .HasColumnType("INTEGER");

                    b.Property<int>("LocationEndId")
                        .HasColumnType("INTEGER");

                    b.Property<int>("LocationStartId")
                        .HasColumnType("INTEGER");

                    b.Property<bool>("SameGender")
                        .HasColumnType("INTEGER");

                    b.Property<int>("ToGSO")
                        .HasColumnType("INTEGER");

                    b.Property<string>("WeekDay")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.HasIndex("DriverId");

                    b.HasIndex("LocationEndId");

                    b.HasIndex("LocationStartId");

                    b.ToTable("Trips");
                });

            modelBuilder.Entity("MitfahrerDB_Backend.Models.User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<int>("GenderId")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Mail")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("Passwort")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("Phone")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.HasIndex("GenderId");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("MitfahrerDB_Backend.Models.UserTrip", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<int>("TripId")
                        .HasColumnType("INTEGER");

                    b.Property<int>("UserId")
                        .HasColumnType("INTEGER");

                    b.HasKey("Id");

                    b.HasIndex("TripId");

                    b.HasIndex("UserId");

                    b.ToTable("UserTrips");
                });

            modelBuilder.Entity("MitfahrerDB_Backend.Models.Trip", b =>
                {
                    b.HasOne("MitfahrerDB_Backend.Models.User", "Driver")
                        .WithMany()
                        .HasForeignKey("DriverId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("MitfahrerDB_Backend.Models.Location", "LocationEnd")
                        .WithMany()
                        .HasForeignKey("LocationEndId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("MitfahrerDB_Backend.Models.Location", "LocationStart")
                        .WithMany()
                        .HasForeignKey("LocationStartId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Driver");

                    b.Navigation("LocationEnd");

                    b.Navigation("LocationStart");
                });

            modelBuilder.Entity("MitfahrerDB_Backend.Models.User", b =>
                {
                    b.HasOne("MitfahrerDB_Backend.Models.Gender", "Gender")
                        .WithMany()
                        .HasForeignKey("GenderId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Gender");
                });

            modelBuilder.Entity("MitfahrerDB_Backend.Models.UserTrip", b =>
                {
                    b.HasOne("MitfahrerDB_Backend.Models.Trip", "Trip")
                        .WithMany()
                        .HasForeignKey("TripId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("MitfahrerDB_Backend.Models.User", "User")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Trip");

                    b.Navigation("User");
                });
#pragma warning restore 612, 618
        }
    }
}

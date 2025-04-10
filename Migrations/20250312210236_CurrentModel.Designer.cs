﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using WeatherDataService.Data;

#nullable disable

namespace WeatherDataService.Migrations
{
    [DbContext(typeof(WeatherContext))]
    [Migration("20250312210236_CurrentModel")]
    partial class CurrentModel
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder.HasAnnotation("ProductVersion", "9.0.3");

            modelBuilder.Entity("WeatherDataService.Models.WeatherData", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<DateTime>("LastUpdated")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("WeatherData");
                });

            modelBuilder.Entity("WeatherDataService.Models.WeatherData", b =>
                {
                    b.OwnsOne("WeatherDataService.Models.Current", "current", b1 =>
                        {
                            b1.Property<int>("WeatherDataId")
                                .HasColumnType("INTEGER");

                            b1.Property<double>("temperature_2m")
                                .HasColumnType("REAL");

                            b1.Property<int>("weather_code")
                                .HasColumnType("INTEGER");

                            b1.HasKey("WeatherDataId");

                            b1.ToTable("WeatherData");

                            b1.WithOwner()
                                .HasForeignKey("WeatherDataId");
                        });

                    b.Navigation("current");
                });
#pragma warning restore 612, 618
        }
    }
}

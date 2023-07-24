﻿// <auto-generated />
using InfoCity.API.DbContexts;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace InfoCity.API.Migrations
{
    [DbContext(typeof(CityInfoContext))]
    [Migration("20230724164813_CityInfoDBInitialMigration")]
    partial class CityInfoDBInitialMigration
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "5.0.10");

            modelBuilder.Entity("InfoCity.API.Entities.City", b =>
                {
                    b.Property<int>("CityId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Description")
                        .HasMaxLength(200)
                        .HasColumnType("TEXT");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("TEXT");

                    b.HasKey("CityId");

                    b.ToTable("cities");
                });

            modelBuilder.Entity("InfoCity.API.Entities.PointOfInterest", b =>
                {
                    b.Property<int>("PointInterestId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<int>("CityId")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Description")
                        .HasColumnType("TEXT");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("TEXT");

                    b.HasKey("PointInterestId");

                    b.HasIndex("CityId");

                    b.ToTable("pointOfInterests");
                });

            modelBuilder.Entity("InfoCity.API.Entities.PointOfInterest", b =>
                {
                    b.HasOne("InfoCity.API.Entities.City", "City")
                        .WithMany("PointInterests")
                        .HasForeignKey("CityId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("City");
                });

            modelBuilder.Entity("InfoCity.API.Entities.City", b =>
                {
                    b.Navigation("PointInterests");
                });
#pragma warning restore 612, 618
        }
    }
}

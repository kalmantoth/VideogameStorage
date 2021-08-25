﻿// <auto-generated />
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using VideogameStorage.Models;

namespace VideogameStorage.Migrations
{
    [DbContext(typeof(VideogameContext))]
    [Migration("20210824153341_ModifiedReleaseDateRange")]
    partial class ModifiedReleaseDateRange
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "5.0.9");

            modelBuilder.Entity("VideogameStorage.Models.Videogame", b =>
                {
                    b.Property<int>("VideogameId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER")
                        .HasColumnName("videogame_id");

                    b.Property<bool>("ConsoleExclusive")
                        .HasColumnType("boolean")
                        .HasColumnName("console_exclusive");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(100)")
                        .HasColumnName("name");

                    b.Property<float>("Rating")
                        .HasColumnType("float(1)")
                        .HasColumnName("rating");

                    b.Property<int>("ReleaseDate")
                        .HasColumnType("int")
                        .HasColumnName("release_date");

                    b.Property<string>("Type")
                        .IsRequired()
                        .HasColumnType("nvarchar(100)")
                        .HasColumnName("type");

                    b.HasKey("VideogameId");

                    b.ToTable("Videogame");
                });
#pragma warning restore 612, 618
        }
    }
}

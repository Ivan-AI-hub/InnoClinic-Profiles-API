﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using ProfilesAPI.Persistence;

#nullable disable

namespace ProfilesAPI.Persistence.Migrations
{
    [DbContext(typeof(ProfilesContext))]
    [Migration("20230619155927_edit-profiles-structure")]
    partial class editprofilesstructure
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.16")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("ProfilesAPI.Domain.HumanInfo", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("BirthDay")
                        .HasColumnType("datetime2");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("MiddleName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("Email")
                        .IsUnique();

                    b.ToTable("HumansInfo");
                });

            modelBuilder.Entity("ProfilesAPI.Domain.Profile", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<int?>("CareerStartYear")
                        .HasColumnType("int");

                    b.Property<Guid>("InfoId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("PhoneNumber")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Role")
                        .HasColumnType("int");

                    b.Property<string>("Specialization")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("Status")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("InfoId");

                    b.ToTable("Profiles");
                });

            modelBuilder.Entity("ProfilesAPI.Domain.HumanInfo", b =>
                {
                    b.OwnsOne("ProfilesAPI.Domain.Picture", "Photo", b1 =>
                        {
                            b1.Property<Guid>("HumanInfoId")
                                .HasColumnType("uniqueidentifier");

                            b1.Property<string>("Name")
                                .IsRequired()
                                .HasColumnType("nvarchar(max)");

                            b1.HasKey("HumanInfoId");

                            b1.ToTable("HumansInfo");

                            b1.WithOwner()
                                .HasForeignKey("HumanInfoId");
                        });

                    b.Navigation("Photo");
                });

            modelBuilder.Entity("ProfilesAPI.Domain.Profile", b =>
                {
                    b.HasOne("ProfilesAPI.Domain.HumanInfo", "Info")
                        .WithMany()
                        .HasForeignKey("InfoId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.OwnsOne("ProfilesAPI.Domain.Office", "Office", b1 =>
                        {
                            b1.Property<Guid>("ProfileId")
                                .HasColumnType("uniqueidentifier");

                            b1.Property<Guid>("Id")
                                .HasColumnType("uniqueidentifier");

                            b1.HasKey("ProfileId");

                            b1.ToTable("Profiles");

                            b1.WithOwner()
                                .HasForeignKey("ProfileId");
                        });

                    b.Navigation("Info");

                    b.Navigation("Office");
                });
#pragma warning restore 612, 618
        }
    }
}
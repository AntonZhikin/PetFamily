﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;
using PetFamily.Infrastructure;

#nullable disable

namespace PetFamily.Infrastructure.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20240815133508_Volunteer")]
    partial class Volunteer
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.8")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("PetFamily.Domain.Pet.Pet", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid")
                        .HasColumnName("id");

                    b.Property<string>("Address")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("character varying(100)")
                        .HasColumnName("address");

                    b.Property<string>("Breed")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("character varying(100)")
                        .HasColumnName("breed");

                    b.Property<string>("Color")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("character varying(100)")
                        .HasColumnName("color");

                    b.Property<DateOnly>("DateCreate")
                        .HasMaxLength(100)
                        .HasColumnType("date")
                        .HasColumnName("date_create");

                    b.Property<DateOnly>("DateOfBirth")
                        .HasMaxLength(100)
                        .HasColumnType("date")
                        .HasColumnName("date_of_birth");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasMaxLength(2000)
                        .HasColumnType("character varying(2000)")
                        .HasColumnName("description");

                    b.Property<float>("Height")
                        .HasMaxLength(100)
                        .HasColumnType("real")
                        .HasColumnName("height");

                    b.Property<bool>("IsNeutered")
                        .HasMaxLength(100)
                        .HasColumnType("boolean")
                        .HasColumnName("is_neutered");

                    b.Property<bool>("IsVaccine")
                        .HasMaxLength(100)
                        .HasColumnType("boolean")
                        .HasColumnName("is_vaccine");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("character varying(100)")
                        .HasColumnName("name");

                    b.Property<string>("PetHealthInfo")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("character varying(100)")
                        .HasColumnName("pet_health_info");

                    b.Property<string>("PhoneNumber")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("phone_number");

                    b.Property<string>("Species")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("character varying(100)")
                        .HasColumnName("species");

                    b.Property<int>("Status")
                        .HasMaxLength(100)
                        .HasColumnType("integer")
                        .HasColumnName("status");

                    b.Property<float>("Weight")
                        .HasMaxLength(100)
                        .HasColumnType("real")
                        .HasColumnName("weight");

                    b.Property<Guid?>("vol_id")
                        .HasColumnType("uuid")
                        .HasColumnName("vol_id");

                    b.HasKey("Id")
                        .HasName("pk_pets");

                    b.HasIndex("vol_id")
                        .HasDatabaseName("ix_pets_vol_id");

                    b.ToTable("pets", (string)null);
                });

            modelBuilder.Entity("PetFamily.Domain.Pet.PetPhoto", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid")
                        .HasColumnName("id");

                    b.Property<bool>("IsMain")
                        .HasMaxLength(100)
                        .HasColumnType("boolean")
                        .HasColumnName("is_main");

                    b.Property<string>("Path")
                        .IsRequired()
                        .HasMaxLength(2000)
                        .HasColumnType("character varying(2000)")
                        .HasColumnName("path");

                    b.Property<Guid?>("pet_id")
                        .HasColumnType("uuid")
                        .HasColumnName("pet_id");

                    b.HasKey("Id")
                        .HasName("pk_petphotos");

                    b.HasIndex("pet_id")
                        .HasDatabaseName("ix_petphotos_pet_id");

                    b.ToTable("petphotos", (string)null);
                });

            modelBuilder.Entity("PetFamily.Domain.Pet.Requisite", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid")
                        .HasColumnName("id");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("character varying(100)")
                        .HasColumnName("name");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasMaxLength(2000)
                        .HasColumnType("character varying(2000)")
                        .HasColumnName("title");

                    b.Property<Guid?>("pet_id")
                        .HasColumnType("uuid")
                        .HasColumnName("pet_id");

                    b.HasKey("Id")
                        .HasName("pk_requisites");

                    b.HasIndex("pet_id")
                        .HasDatabaseName("ix_requisites_pet_id");

                    b.ToTable("requisites", (string)null);
                });

            modelBuilder.Entity("PetFamily.Domain.Volunteer.RequisiteForHelp", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid")
                        .HasColumnName("id");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasMaxLength(2000)
                        .HasColumnType("character varying(2000)")
                        .HasColumnName("description");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("character varying(100)")
                        .HasColumnName("name");

                    b.Property<Guid?>("vol_id")
                        .HasColumnType("uuid")
                        .HasColumnName("vol_id");

                    b.HasKey("Id")
                        .HasName("pk_requisiteforhelp");

                    b.HasIndex("vol_id")
                        .HasDatabaseName("ix_requisiteforhelp_vol_id");

                    b.ToTable("requisiteforhelp", (string)null);
                });

            modelBuilder.Entity("PetFamily.Domain.Volunteer.SocialMedia", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid")
                        .HasColumnName("id");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("character varying(100)")
                        .HasColumnName("name");

                    b.Property<string>("Path")
                        .IsRequired()
                        .HasMaxLength(2000)
                        .HasColumnType("character varying(2000)")
                        .HasColumnName("path");

                    b.Property<Guid?>("vol_id")
                        .HasColumnType("uuid")
                        .HasColumnName("vol_id");

                    b.HasKey("Id")
                        .HasName("pk_socialmedia");

                    b.HasIndex("vol_id")
                        .HasDatabaseName("ix_socialmedia_vol_id");

                    b.ToTable("socialmedia", (string)null);
                });

            modelBuilder.Entity("PetFamily.Domain.Volunteer.Volunteer", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid")
                        .HasColumnName("id");

                    b.Property<int>("CountPetFoundHome")
                        .HasMaxLength(100)
                        .HasColumnType("integer")
                        .HasColumnName("count_pet_found_home");

                    b.Property<int>("CountPetHealing")
                        .HasMaxLength(100)
                        .HasColumnType("integer")
                        .HasColumnName("count_pet_healing");

                    b.Property<int>("CountPetInHome")
                        .HasMaxLength(100)
                        .HasColumnType("integer")
                        .HasColumnName("count_pet_in_home");

                    b.Property<string>("Descriptions")
                        .IsRequired()
                        .HasMaxLength(2000)
                        .HasColumnType("character varying(2000)")
                        .HasColumnName("descriptions");

                    b.Property<int>("ExperienceYears")
                        .HasMaxLength(100)
                        .HasColumnType("integer")
                        .HasColumnName("experience_years");

                    b.Property<string>("FullName")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("character varying(100)")
                        .HasColumnName("full_name");

                    b.Property<string>("PhoneNumber")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("character varying(100)")
                        .HasColumnName("phone_number");

                    b.HasKey("Id")
                        .HasName("pk_voluunter");

                    b.ToTable("voluunter", (string)null);
                });

            modelBuilder.Entity("PetFamily.Domain.Pet.Pet", b =>
                {
                    b.HasOne("PetFamily.Domain.Volunteer.Volunteer", null)
                        .WithMany("Pets")
                        .HasForeignKey("vol_id")
                        .HasConstraintName("fk_pets_voluunter_vol_id");
                });

            modelBuilder.Entity("PetFamily.Domain.Pet.PetPhoto", b =>
                {
                    b.HasOne("PetFamily.Domain.Pet.Pet", null)
                        .WithMany("PetPhotos")
                        .HasForeignKey("pet_id")
                        .HasConstraintName("fk_petphotos_pets_pet_id");
                });

            modelBuilder.Entity("PetFamily.Domain.Pet.Requisite", b =>
                {
                    b.HasOne("PetFamily.Domain.Pet.Pet", null)
                        .WithMany("Requisites")
                        .HasForeignKey("pet_id")
                        .HasConstraintName("fk_requisites_pets_pet_id");
                });

            modelBuilder.Entity("PetFamily.Domain.Volunteer.RequisiteForHelp", b =>
                {
                    b.HasOne("PetFamily.Domain.Volunteer.Volunteer", null)
                        .WithMany("RequisiteForHelps")
                        .HasForeignKey("vol_id")
                        .HasConstraintName("fk_requisiteforhelp_voluunter_vol_id");
                });

            modelBuilder.Entity("PetFamily.Domain.Volunteer.SocialMedia", b =>
                {
                    b.HasOne("PetFamily.Domain.Volunteer.Volunteer", null)
                        .WithMany("SocialMedias")
                        .HasForeignKey("vol_id")
                        .HasConstraintName("fk_socialmedia_voluunter_vol_id");
                });

            modelBuilder.Entity("PetFamily.Domain.Pet.Pet", b =>
                {
                    b.Navigation("PetPhotos");

                    b.Navigation("Requisites");
                });

            modelBuilder.Entity("PetFamily.Domain.Volunteer.Volunteer", b =>
                {
                    b.Navigation("Pets");

                    b.Navigation("RequisiteForHelps");

                    b.Navigation("SocialMedias");
                });
#pragma warning restore 612, 618
        }
    }
}

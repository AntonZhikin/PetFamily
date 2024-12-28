﻿// <auto-generated />
using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;
using PetFamily.Pets.Infrastructure.DbContext;

#nullable disable

namespace PetFamily.Pets.Infrastructure.Migrations
{
    [DbContext(typeof(WriteDbContext))]
    [Migration("20241227181441_Initial")]
    partial class Initial
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.11")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("PetFamily.Pets.Domain.AggregateRoot.Volunteer", b =>
                {
                    b.Property<Guid>("Id")
                        .HasColumnType("uuid")
                        .HasColumnName("id");

                    b.Property<bool>("_isDeleted")
                        .HasColumnType("boolean")
                        .HasColumnName("is_deleted");

                    b.ComplexProperty<Dictionary<string, object>>("Description", "PetFamily.Pets.Domain.AggregateRoot.Volunteer.Description#Description", b1 =>
                        {
                            b1.IsRequired();

                            b1.Property<string>("Value")
                                .IsRequired()
                                .HasColumnType("text")
                                .HasColumnName("description");
                        });

                    b.ComplexProperty<Dictionary<string, object>>("ExperienceYear", "PetFamily.Pets.Domain.AggregateRoot.Volunteer.ExperienceYear#ExperienceYear", b1 =>
                        {
                            b1.IsRequired();

                            b1.Property<string>("Value")
                                .IsRequired()
                                .HasColumnType("text")
                                .HasColumnName("experienceyears");
                        });

                    b.ComplexProperty<Dictionary<string, object>>("FullName", "PetFamily.Pets.Domain.AggregateRoot.Volunteer.FullName#FullName", b1 =>
                        {
                            b1.IsRequired();

                            b1.Property<string>("Name")
                                .IsRequired()
                                .HasColumnType("text")
                                .HasColumnName("name");

                            b1.Property<string>("SecondName")
                                .IsRequired()
                                .HasColumnType("text")
                                .HasColumnName("secondName");

                            b1.Property<string>("Surname")
                                .IsRequired()
                                .HasColumnType("text")
                                .HasColumnName("surname");
                        });

                    b.ComplexProperty<Dictionary<string, object>>("PhoneNumber", "PetFamily.Pets.Domain.AggregateRoot.Volunteer.PhoneNumber#PhoneNumber", b1 =>
                        {
                            b1.IsRequired();

                            b1.Property<string>("Value")
                                .IsRequired()
                                .HasColumnType("text")
                                .HasColumnName("phonenumber");
                        });

                    b.HasKey("Id")
                        .HasName("pk_volunteers");

                    b.ToTable("volunteers", (string)null);
                });

            modelBuilder.Entity("PetFamily.Pets.Domain.Entity.Pet", b =>
                {
                    b.Property<Guid>("Id")
                        .HasColumnType("uuid")
                        .HasColumnName("id");

                    b.Property<DateTime>("DateCreate")
                        .HasMaxLength(2000)
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("date_create");

                    b.Property<DateTime>("DateOfBirth")
                        .HasMaxLength(2000)
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("date_of_birth");

                    b.Property<int>("HelpStatus")
                        .HasMaxLength(2000)
                        .HasColumnType("integer")
                        .HasColumnName("help_status");

                    b.Property<bool>("IsVaccine")
                        .HasMaxLength(2000)
                        .HasColumnType("boolean")
                        .HasColumnName("is_vaccine");

                    b.Property<string>("Photos")
                        .IsRequired()
                        .HasColumnType("jsonb")
                        .HasColumnName("photos");

                    b.Property<Guid?>("VolunteerId")
                        .HasColumnType("uuid")
                        .HasColumnName("volunteer_id");

                    b.Property<bool>("_isDeleted")
                        .HasColumnType("boolean")
                        .HasColumnName("is_deleted");

                    b.ComplexProperty<Dictionary<string, object>>("Address", "PetFamily.Pets.Domain.Entity.Pet.Address#Address", b1 =>
                        {
                            b1.IsRequired();

                            b1.Property<string>("City")
                                .IsRequired()
                                .HasMaxLength(2000)
                                .HasColumnType("character varying(2000)")
                                .HasColumnName("address_city");

                            b1.Property<string>("Street")
                                .IsRequired()
                                .HasMaxLength(2000)
                                .HasColumnType("character varying(2000)")
                                .HasColumnName("address_street");
                        });

                    b.ComplexProperty<Dictionary<string, object>>("Color", "PetFamily.Pets.Domain.Entity.Pet.Color#Color", b1 =>
                        {
                            b1.IsRequired();

                            b1.Property<string>("Value")
                                .IsRequired()
                                .HasMaxLength(2000)
                                .HasColumnType("character varying(2000)")
                                .HasColumnName("color");
                        });

                    b.ComplexProperty<Dictionary<string, object>>("Description", "PetFamily.Pets.Domain.Entity.Pet.Description#Description", b1 =>
                        {
                            b1.IsRequired();

                            b1.Property<string>("Value")
                                .IsRequired()
                                .HasMaxLength(2000)
                                .HasColumnType("character varying(2000)")
                                .HasColumnName("description");
                        });

                    b.ComplexProperty<Dictionary<string, object>>("Height", "PetFamily.Pets.Domain.Entity.Pet.Height#Height", b1 =>
                        {
                            b1.IsRequired();

                            b1.Property<float>("Value")
                                .HasMaxLength(2000)
                                .HasColumnType("real")
                                .HasColumnName("height");
                        });

                    b.ComplexProperty<Dictionary<string, object>>("IsNeutered", "PetFamily.Pets.Domain.Entity.Pet.IsNeutered#IsNautered", b1 =>
                        {
                            b1.IsRequired();

                            b1.Property<bool>("Value")
                                .HasMaxLength(2000)
                                .HasColumnType("boolean")
                                .HasColumnName("is_neutered");
                        });

                    b.ComplexProperty<Dictionary<string, object>>("Name", "PetFamily.Pets.Domain.Entity.Pet.Name#Name", b1 =>
                        {
                            b1.IsRequired();

                            b1.Property<string>("Value")
                                .IsRequired()
                                .ValueGeneratedOnUpdateSometimes()
                                .HasMaxLength(2000)
                                .HasColumnType("character varying(2000)")
                                .HasColumnName("name");
                        });

                    b.ComplexProperty<Dictionary<string, object>>("PetHealthInfo", "PetFamily.Pets.Domain.Entity.Pet.PetHealthInfo#PetHealthInfo", b1 =>
                        {
                            b1.IsRequired();

                            b1.Property<string>("Value")
                                .IsRequired()
                                .HasMaxLength(2000)
                                .HasColumnType("character varying(2000)")
                                .HasColumnName("petHealthInfo");
                        });

                    b.ComplexProperty<Dictionary<string, object>>("PhoneNumber", "PetFamily.Pets.Domain.Entity.Pet.PhoneNumber#PhoneNumber", b1 =>
                        {
                            b1.IsRequired();

                            b1.Property<string>("Value")
                                .IsRequired()
                                .HasMaxLength(2000)
                                .HasColumnType("character varying(2000)")
                                .HasColumnName("phone_number");
                        });

                    b.ComplexProperty<Dictionary<string, object>>("Position", "PetFamily.Pets.Domain.Entity.Pet.Position#Position", b1 =>
                        {
                            b1.IsRequired();

                            b1.Property<int>("Value")
                                .HasColumnType("integer")
                                .HasColumnName("position");
                        });

                    b.ComplexProperty<Dictionary<string, object>>("SpeciesDetails", "PetFamily.Pets.Domain.Entity.Pet.SpeciesDetails#SpeciesDetails", b1 =>
                        {
                            b1.IsRequired();

                            b1.Property<Guid>("BreedId")
                                .HasColumnType("uuid")
                                .HasColumnName("breed_id");

                            b1.Property<Guid>("SpeciesId")
                                .HasColumnType("uuid")
                                .HasColumnName("species_id");
                        });

                    b.ComplexProperty<Dictionary<string, object>>("Weight", "PetFamily.Pets.Domain.Entity.Pet.Weight#Weight", b1 =>
                        {
                            b1.IsRequired();

                            b1.Property<float>("Value")
                                .HasMaxLength(2000)
                                .HasColumnType("real")
                                .HasColumnName("weight");
                        });

                    b.HasKey("Id")
                        .HasName("pk_pets");

                    b.HasIndex("VolunteerId")
                        .HasDatabaseName("ix_pets_volunteer_id");

                    b.ToTable("pets", (string)null);
                });

            modelBuilder.Entity("PetFamily.Pets.Domain.AggregateRoot.Volunteer", b =>
                {
                    b.OwnsOne("PetFamily.Pets.Domain.ValueObjects.AssistanceDetailList", "AssistanceDetailList", b1 =>
                        {
                            b1.Property<Guid>("VolunteerId")
                                .HasColumnType("uuid");

                            b1.HasKey("VolunteerId");

                            b1.ToTable("volunteers");

                            b1.ToJson("assistance_detail_list");

                            b1.WithOwner()
                                .HasForeignKey("VolunteerId")
                                .HasConstraintName("fk_volunteers_volunteers_id");

                            b1.OwnsMany("PetFamily.Pets.Domain.ValueObjects.AssistanceDetail", "AssistanceDetails", b2 =>
                                {
                                    b2.Property<Guid>("AssistanceDetailListVolunteerId")
                                        .HasColumnType("uuid");

                                    b2.Property<int>("Id")
                                        .ValueGeneratedOnAdd()
                                        .HasColumnType("integer");

                                    b2.Property<string>("Description")
                                        .IsRequired()
                                        .HasMaxLength(2000)
                                        .HasColumnType("character varying(2000)");

                                    b2.Property<string>("Name")
                                        .IsRequired()
                                        .HasMaxLength(2000)
                                        .HasColumnType("character varying(2000)");

                                    b2.HasKey("AssistanceDetailListVolunteerId", "Id");

                                    b2.ToTable("volunteers");

                                    b2.ToJson("assistance_detail_list");

                                    b2.WithOwner()
                                        .HasForeignKey("AssistanceDetailListVolunteerId")
                                        .HasConstraintName("fk_volunteers_volunteers_assistance_detail_list_volunteer_id");
                                });

                            b1.Navigation("AssistanceDetails");
                        });

                    b.OwnsOne("PetFamily.Pets.Domain.ValueObjects.SocialNetworkList", "SocialNetworkList", b1 =>
                        {
                            b1.Property<Guid>("VolunteerId")
                                .HasColumnType("uuid");

                            b1.HasKey("VolunteerId");

                            b1.ToTable("volunteers");

                            b1.ToJson("social_network_list");

                            b1.WithOwner()
                                .HasForeignKey("VolunteerId")
                                .HasConstraintName("fk_volunteers_volunteers_id");

                            b1.OwnsMany("PetFamily.Pets.Domain.ValueObjects.SocialNetwork", "SocialNetworks", b2 =>
                                {
                                    b2.Property<Guid>("SocialNetworkListVolunteerId")
                                        .HasColumnType("uuid");

                                    b2.Property<int>("Id")
                                        .ValueGeneratedOnAdd()
                                        .HasColumnType("integer");

                                    b2.Property<string>("Name")
                                        .IsRequired()
                                        .HasMaxLength(2000)
                                        .HasColumnType("character varying(2000)");

                                    b2.Property<string>("Path")
                                        .IsRequired()
                                        .HasMaxLength(2000)
                                        .HasColumnType("character varying(2000)");

                                    b2.HasKey("SocialNetworkListVolunteerId", "Id");

                                    b2.ToTable("volunteers");

                                    b2.ToJson("social_network_list");

                                    b2.WithOwner()
                                        .HasForeignKey("SocialNetworkListVolunteerId")
                                        .HasConstraintName("fk_volunteers_volunteers_social_network_list_volunteer_id");
                                });

                            b1.Navigation("SocialNetworks");
                        });

                    b.Navigation("AssistanceDetailList")
                        .IsRequired();

                    b.Navigation("SocialNetworkList")
                        .IsRequired();
                });

            modelBuilder.Entity("PetFamily.Pets.Domain.Entity.Pet", b =>
                {
                    b.HasOne("PetFamily.Pets.Domain.AggregateRoot.Volunteer", null)
                        .WithMany("Pets")
                        .HasForeignKey("VolunteerId")
                        .HasConstraintName("fk_pets_volunteers_volunteer_id");

                    b.OwnsOne("PetFamily.Pets.Domain.ValueObjects.RequisiteList", "Requisites", b1 =>
                        {
                            b1.Property<Guid>("PetId")
                                .HasColumnType("uuid");

                            b1.HasKey("PetId");

                            b1.ToTable("pets");

                            b1.ToJson("requisite");

                            b1.WithOwner()
                                .HasForeignKey("PetId")
                                .HasConstraintName("fk_pets_pets_id");

                            b1.OwnsMany("PetFamily.Pets.Domain.ValueObjects.Requisite", "Requisites", b2 =>
                                {
                                    b2.Property<Guid>("RequisiteListPetId")
                                        .HasColumnType("uuid");

                                    b2.Property<int>("Id")
                                        .ValueGeneratedOnAdd()
                                        .HasColumnType("integer");

                                    b2.Property<string>("Description")
                                        .IsRequired()
                                        .HasMaxLength(2000)
                                        .HasColumnType("character varying(2000)")
                                        .HasColumnName("title");

                                    b2.Property<string>("Name")
                                        .IsRequired()
                                        .ValueGeneratedOnUpdateSometimes()
                                        .HasMaxLength(2000)
                                        .HasColumnType("character varying(2000)")
                                        .HasColumnName("name");

                                    b2.HasKey("RequisiteListPetId", "Id");

                                    b2.ToTable("pets");

                                    b2.ToJson("requisite");

                                    b2.WithOwner()
                                        .HasForeignKey("RequisiteListPetId")
                                        .HasConstraintName("fk_pets_pets_requisite_list_pet_id");
                                });

                            b1.Navigation("Requisites");
                        });

                    b.Navigation("Requisites")
                        .IsRequired();
                });

            modelBuilder.Entity("PetFamily.Pets.Domain.AggregateRoot.Volunteer", b =>
                {
                    b.Navigation("Pets");
                });
#pragma warning restore 612, 618
        }
    }
}
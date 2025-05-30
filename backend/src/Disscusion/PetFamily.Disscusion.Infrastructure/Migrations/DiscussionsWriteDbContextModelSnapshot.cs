﻿// <auto-generated />
using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;
using PetFamily.Disscusion.Infrastructure.DbContext;

#nullable disable

namespace PetFamily.Disscusion.Infrastructure.Migrations
{
    [DbContext(typeof(DiscussionsWriteDbContext))]
    partial class DiscussionsWriteDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasDefaultSchema("discussions")
                .HasAnnotation("ProductVersion", "8.0.11")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("PetFamily.Disscusion.Domain.AggregateRoot.Discussion", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid")
                        .HasColumnName("id");

                    b.Property<Guid>("RequestId")
                        .HasColumnType("uuid")
                        .HasColumnName("request_id");

                    b.Property<string>("Status")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("status");

                    b.ComplexProperty<Dictionary<string, object>>("DiscussionUsers", "PetFamily.Disscusion.Domain.AggregateRoot.Discussion.DiscussionUsers#DiscussionUsers", b1 =>
                        {
                            b1.IsRequired();

                            b1.Property<Guid>("ApplicantUserId")
                                .HasColumnType("uuid")
                                .HasColumnName("applicant_user_id");

                            b1.Property<Guid>("ReviewingUserId")
                                .HasColumnType("uuid")
                                .HasColumnName("reviewing_user_id");
                        });

                    b.HasKey("Id")
                        .HasName("pk_discussion");

                    b.ToTable("discussion", "discussions");
                });

            modelBuilder.Entity("PetFamily.Disscusion.Domain.Entity.Message", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid")
                        .HasColumnName("id");

                    b.Property<string>("Content")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("message_content");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("created_at");

                    b.Property<Guid>("DiscussionId")
                        .HasColumnType("uuid")
                        .HasColumnName("discussion_id");

                    b.Property<bool>("IsEdited")
                        .HasColumnType("boolean")
                        .HasColumnName("is_edited");

                    b.Property<Guid>("SenderId")
                        .HasColumnType("uuid")
                        .HasColumnName("sender_id");

                    b.HasKey("Id")
                        .HasName("pk_message");

                    b.HasIndex("DiscussionId")
                        .HasDatabaseName("ix_message_discussion_id");

                    b.ToTable("message", "discussions");
                });

            modelBuilder.Entity("PetFamily.Disscusion.Domain.Entity.Message", b =>
                {
                    b.HasOne("PetFamily.Disscusion.Domain.AggregateRoot.Discussion", null)
                        .WithMany("Messages")
                        .HasForeignKey("DiscussionId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("fk_message_discussions_discussion_id");
                });

            modelBuilder.Entity("PetFamily.Disscusion.Domain.AggregateRoot.Discussion", b =>
                {
                    b.Navigation("Messages");
                });
#pragma warning restore 612, 618
        }
    }
}

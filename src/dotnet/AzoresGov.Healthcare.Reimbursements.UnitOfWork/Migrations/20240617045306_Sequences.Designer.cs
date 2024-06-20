﻿// <auto-generated />
using System;
using AzoresGov.Healthcare.Reimbursements.UnitOfWork;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace AzoresGov.Healthcare.Reimbursements.UnitOfWork.Migrations
{
    [DbContext(typeof(HealthcareReimbursementsUnitOfWork))]
    [Migration("20240617045306_Sequences")]
    partial class Sequences
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.0")
                .HasAnnotation("Relational:MaxIdentifierLength", 64);

            modelBuilder.Entity("AzoresGov.Healthcare.Reimbursements.UnitOfWork.Entities.Parameter", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    b.Property<string>("JsonValue")
                        .IsRequired()
                        .HasMaxLength(4096)
                        .HasColumnType("varchar(4096)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(64)
                        .HasColumnType("varchar(64)");

                    b.Property<Guid>("PublicId")
                        .HasColumnType("char(36)");

                    b.Property<Guid>("RowVersionId")
                        .IsConcurrencyToken()
                        .HasColumnType("char(36)");

                    b.HasKey("Id");

                    b.HasAlternateKey("Name");

                    b.HasAlternateKey("PublicId");

                    b.ToTable("Parameters");
                });

            modelBuilder.Entity("AzoresGov.Healthcare.Reimbursements.UnitOfWork.Entities.Sequence", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(64)
                        .HasColumnType("varchar(64)");

                    b.Property<int>("NextValue")
                        .HasColumnType("int");

                    b.Property<Guid>("PublicId")
                        .HasColumnType("char(36)");

                    b.Property<Guid>("RowVersionId")
                        .IsConcurrencyToken()
                        .HasColumnType("char(36)");

                    b.HasKey("Id");

                    b.HasAlternateKey("Name");

                    b.HasAlternateKey("PublicId");

                    b.ToTable("Sequences");
                });

            modelBuilder.Entity("AzoresGov.Healthcare.Reimbursements.UnitOfWork.Entities.User", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    b.Property<string>("EmailAddress")
                        .IsRequired()
                        .HasMaxLength(128)
                        .HasColumnType("varchar(128)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(128)
                        .HasColumnType("varchar(128)");

                    b.Property<Guid>("PublicId")
                        .HasColumnType("char(36)");

                    b.Property<Guid>("RowVersionId")
                        .IsConcurrencyToken()
                        .HasColumnType("char(36)");

                    b.HasKey("Id");

                    b.HasAlternateKey("PublicId");

                    b.HasIndex("EmailAddress")
                        .IsUnique();

                    b.ToTable("Users");
                });

            modelBuilder.Entity("AzoresGov.Healthcare.Reimbursements.UnitOfWork.Entities.UserPassword", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    b.Property<string>("Hash")
                        .IsRequired()
                        .HasMaxLength(64)
                        .HasColumnType("varchar(64)");

                    b.Property<Guid>("PublicId")
                        .HasColumnType("char(36)");

                    b.Property<Guid>("RowVersionId")
                        .IsConcurrencyToken()
                        .HasColumnType("char(36)");

                    b.Property<long>("UserId")
                        .HasColumnType("bigint");

                    b.HasKey("Id");

                    b.HasAlternateKey("PublicId");

                    b.HasIndex("UserId")
                        .IsUnique();

                    b.ToTable("UserPasswords");
                });

            modelBuilder.Entity("AzoresGov.Healthcare.Reimbursements.UnitOfWork.Entities.UserRole", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    b.Property<int>("Nature")
                        .HasColumnType("int");

                    b.Property<Guid>("PublicId")
                        .HasColumnType("char(36)");

                    b.Property<Guid>("RowVersionId")
                        .IsConcurrencyToken()
                        .HasColumnType("char(36)");

                    b.Property<long>("UserId")
                        .HasColumnType("bigint");

                    b.HasKey("Id");

                    b.HasAlternateKey("PublicId");

                    b.HasIndex("UserId", "Nature")
                        .IsUnique();

                    b.ToTable("UserRoles");
                });

            modelBuilder.Entity("AzoresGov.Healthcare.Reimbursements.UnitOfWork.Entities.UserSession", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    b.Property<DateTimeOffset>("Creation")
                        .HasColumnType("datetime");

                    b.Property<DateTimeOffset?>("Expiration")
                        .HasColumnType("datetime");

                    b.Property<DateTimeOffset>("LastSeen")
                        .HasColumnType("datetime");

                    b.Property<Guid>("PublicId")
                        .HasColumnType("char(36)");

                    b.Property<string>("RemoteAddress")
                        .IsRequired()
                        .HasMaxLength(64)
                        .HasColumnType("varchar(64)");

                    b.Property<Guid>("RowVersionId")
                        .IsConcurrencyToken()
                        .HasColumnType("char(36)");

                    b.Property<string>("UserAgent")
                        .IsRequired()
                        .HasMaxLength(4092)
                        .HasColumnType("varchar(4092)");

                    b.Property<long>("UserId")
                        .HasColumnType("bigint");

                    b.HasKey("Id");

                    b.HasAlternateKey("PublicId");

                    b.HasIndex("UserId");

                    b.ToTable("UserSessions");
                });

            modelBuilder.Entity("AzoresGov.Healthcare.Reimbursements.UnitOfWork.Entities.UserPassword", b =>
                {
                    b.HasOne("AzoresGov.Healthcare.Reimbursements.UnitOfWork.Entities.User", "User")
                        .WithOne()
                        .HasForeignKey("AzoresGov.Healthcare.Reimbursements.UnitOfWork.Entities.UserPassword", "UserId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("User");
                });

            modelBuilder.Entity("AzoresGov.Healthcare.Reimbursements.UnitOfWork.Entities.UserRole", b =>
                {
                    b.HasOne("AzoresGov.Healthcare.Reimbursements.UnitOfWork.Entities.User", "User")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("User");
                });

            modelBuilder.Entity("AzoresGov.Healthcare.Reimbursements.UnitOfWork.Entities.UserSession", b =>
                {
                    b.HasOne("AzoresGov.Healthcare.Reimbursements.UnitOfWork.Entities.User", "User")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("User");
                });
#pragma warning restore 612, 618
        }
    }
}
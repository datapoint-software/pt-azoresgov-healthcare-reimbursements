﻿// <auto-generated />
using System;
using AzoresGov.Healthcare.Reimbursements.UnitOfWork;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

#pragma warning disable IDE0022

namespace AzoresGov.Healthcare.Reimbursements.UnitOfWork.Migrations
{
    [DbContext(typeof(HealthcareReimbursementsContext))]
    partial class HealthcareReimbursementsContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.8")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("AzoresGov.Healthcare.Reimbursements.UnitOfWork.Entities.EntityEntity", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<long>("Id"));

                    b.Property<string>("Code")
                        .IsRequired()
                        .HasMaxLength(16)
                        .HasColumnType("nvarchar(16)");

                    b.Property<int>("Kind")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<Guid>("PublicId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("RowVersionId")
                        .IsConcurrencyToken()
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.HasAlternateKey("PublicId");

                    b.HasIndex("Code")
                        .IsUnique();

                    b.ToTable("Entities", (string)null);
                });

            modelBuilder.Entity("AzoresGov.Healthcare.Reimbursements.UnitOfWork.Entities.EntityParentEntity", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<long>("Id"));

                    b.Property<long>("EntityId")
                        .HasColumnType("bigint");

                    b.Property<int>("Level")
                        .HasColumnType("int");

                    b.Property<long>("ParentEntityId")
                        .HasColumnType("bigint");

                    b.Property<Guid>("PublicId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("RowVersionId")
                        .IsConcurrencyToken()
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.HasAlternateKey("PublicId");

                    b.HasIndex("EntityId");

                    b.HasIndex("ParentEntityId");

                    b.ToTable("EntityParents", (string)null);
                });

            modelBuilder.Entity("AzoresGov.Healthcare.Reimbursements.UnitOfWork.Entities.ParameterEntity", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<long>("Id"));

                    b.Property<string>("JsonValue")
                        .IsRequired()
                        .HasMaxLength(4096)
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(64)
                        .HasColumnType("nvarchar(64)");

                    b.Property<Guid>("PublicId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("RowVersionId")
                        .IsConcurrencyToken()
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.HasAlternateKey("Name");

                    b.HasAlternateKey("PublicId");

                    b.ToTable("Parameters", (string)null);
                });

            modelBuilder.Entity("AzoresGov.Healthcare.Reimbursements.UnitOfWork.Entities.PermissionEntity", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<long>("Id"));

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(64)
                        .HasColumnType("nvarchar(64)");

                    b.Property<Guid>("PublicId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("RowVersionId")
                        .IsConcurrencyToken()
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.HasAlternateKey("Name");

                    b.HasAlternateKey("PublicId");

                    b.ToTable("Permissions", (string)null);
                });

            modelBuilder.Entity("AzoresGov.Healthcare.Reimbursements.UnitOfWork.Entities.RoleEntity", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<long>("Id"));

                    b.Property<string>("Description")
                        .HasMaxLength(4096)
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(128)
                        .HasColumnType("nvarchar(128)");

                    b.Property<Guid>("PublicId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("RowVersionId")
                        .IsConcurrencyToken()
                        .HasColumnType("uniqueidentifier");

                    b.Property<int>("Scope")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasAlternateKey("PublicId");

                    b.HasIndex("Name")
                        .IsUnique();

                    b.ToTable("Roles", (string)null);
                });

            modelBuilder.Entity("AzoresGov.Healthcare.Reimbursements.UnitOfWork.Entities.RolePermissionEntity", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<long>("Id"));

                    b.Property<bool>("Granted")
                        .HasColumnType("bit");

                    b.Property<long>("PermissionId")
                        .HasColumnType("bigint");

                    b.Property<Guid>("PublicId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<long>("RoleId")
                        .HasColumnType("bigint");

                    b.Property<Guid>("RowVersionId")
                        .IsConcurrencyToken()
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.HasAlternateKey("PublicId");

                    b.HasAlternateKey("RoleId", "PermissionId");

                    b.HasIndex("PermissionId");

                    b.HasIndex("RoleId");

                    b.ToTable("RolePermissions", (string)null);
                });

            modelBuilder.Entity("AzoresGov.Healthcare.Reimbursements.UnitOfWork.Entities.UserAgentEntity", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<long>("Id"));

                    b.Property<string>("Hash")
                        .IsRequired()
                        .HasMaxLength(32)
                        .HasColumnType("nvarchar(32)");

                    b.Property<Guid>("PublicId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("RowVersionId")
                        .IsConcurrencyToken()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Signature")
                        .IsRequired()
                        .HasMaxLength(4096)
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasAlternateKey("PublicId");

                    b.HasIndex("Hash")
                        .IsUnique();

                    b.ToTable("UserAgents", (string)null);
                });

            modelBuilder.Entity("AzoresGov.Healthcare.Reimbursements.UnitOfWork.Entities.UserEmailAddressEntity", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<long>("Id"));

                    b.Property<string>("EmailAddress")
                        .IsRequired()
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<Guid>("PublicId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("RowVersionId")
                        .IsConcurrencyToken()
                        .HasColumnType("uniqueidentifier");

                    b.Property<long>("UserId")
                        .HasColumnType("bigint");

                    b.HasKey("Id");

                    b.HasAlternateKey("PublicId");

                    b.HasIndex("EmailAddress")
                        .IsUnique();

                    b.HasIndex("UserId");

                    b.ToTable("UserEmailAddresses", (string)null);
                });

            modelBuilder.Entity("AzoresGov.Healthcare.Reimbursements.UnitOfWork.Entities.UserEntity", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<long>("Id"));

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<Guid>("PublicId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("RowVersionId")
                        .IsConcurrencyToken()
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.HasAlternateKey("PublicId");

                    b.HasIndex("Name");

                    b.ToTable("Users", (string)null);
                });

            modelBuilder.Entity("AzoresGov.Healthcare.Reimbursements.UnitOfWork.Entities.UserEntityEntity", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<long>("Id"));

                    b.Property<long>("EntityId")
                        .HasColumnType("bigint");

                    b.Property<Guid>("PublicId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("RowVersionId")
                        .IsConcurrencyToken()
                        .HasColumnType("uniqueidentifier");

                    b.Property<long>("UserId")
                        .HasColumnType("bigint");

                    b.HasKey("Id");

                    b.HasAlternateKey("PublicId");

                    b.HasAlternateKey("UserId", "EntityId");

                    b.HasIndex("EntityId");

                    b.HasIndex("UserId");

                    b.ToTable("UserEntities", (string)null);
                });

            modelBuilder.Entity("AzoresGov.Healthcare.Reimbursements.UnitOfWork.Entities.UserEntityPermissionEntity", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<long>("Id"));

                    b.Property<long>("EntityId")
                        .HasColumnType("bigint");

                    b.Property<bool>("Granted")
                        .HasColumnType("bit");

                    b.Property<long>("PermissionId")
                        .HasColumnType("bigint");

                    b.Property<Guid>("PublicId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("RowVersionId")
                        .IsConcurrencyToken()
                        .HasColumnType("uniqueidentifier");

                    b.Property<long>("UserId")
                        .HasColumnType("bigint");

                    b.HasKey("Id");

                    b.HasAlternateKey("PublicId");

                    b.HasAlternateKey("UserId", "EntityId", "PermissionId");

                    b.HasIndex("EntityId");

                    b.HasIndex("PermissionId");

                    b.HasIndex("UserId");

                    b.ToTable("UserEntityPermissions", (string)null);
                });

            modelBuilder.Entity("AzoresGov.Healthcare.Reimbursements.UnitOfWork.Entities.UserEntityRoleEntity", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<long>("Id"));

                    b.Property<long>("EntityId")
                        .HasColumnType("bigint");

                    b.Property<Guid>("PublicId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<long>("RoleId")
                        .HasColumnType("bigint");

                    b.Property<Guid>("RowVersionId")
                        .IsConcurrencyToken()
                        .HasColumnType("uniqueidentifier");

                    b.Property<long>("UserId")
                        .HasColumnType("bigint");

                    b.HasKey("Id");

                    b.HasAlternateKey("PublicId");

                    b.HasAlternateKey("UserId", "EntityId", "RoleId");

                    b.HasIndex("EntityId");

                    b.HasIndex("RoleId");

                    b.HasIndex("UserId");

                    b.ToTable("UserEntityRoles", (string)null);
                });

            modelBuilder.Entity("AzoresGov.Healthcare.Reimbursements.UnitOfWork.Entities.UserPasswordEntity", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<long>("Id"));

                    b.Property<string>("Hash")
                        .IsRequired()
                        .HasMaxLength(64)
                        .HasColumnType("nvarchar(64)");

                    b.Property<Guid>("PublicId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("RowVersionId")
                        .IsConcurrencyToken()
                        .HasColumnType("uniqueidentifier");

                    b.Property<long>("UserId")
                        .HasColumnType("bigint");

                    b.HasKey("Id");

                    b.HasAlternateKey("PublicId");

                    b.HasIndex("UserId");

                    b.ToTable("UserPasswords", (string)null);
                });

            modelBuilder.Entity("AzoresGov.Healthcare.Reimbursements.UnitOfWork.Entities.UserPermissionEntity", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<long>("Id"));

                    b.Property<bool>("Granted")
                        .HasColumnType("bit");

                    b.Property<long>("PermissionId")
                        .HasColumnType("bigint");

                    b.Property<Guid>("PublicId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("RowVersionId")
                        .IsConcurrencyToken()
                        .HasColumnType("uniqueidentifier");

                    b.Property<long>("UserId")
                        .HasColumnType("bigint");

                    b.HasKey("Id");

                    b.HasAlternateKey("PublicId");

                    b.HasAlternateKey("UserId", "PermissionId");

                    b.HasIndex("PermissionId");

                    b.HasIndex("UserId");

                    b.ToTable("UserPermissions", (string)null);
                });

            modelBuilder.Entity("AzoresGov.Healthcare.Reimbursements.UnitOfWork.Entities.UserRoleEntity", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<long>("Id"));

                    b.Property<Guid>("PublicId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<long>("RoleId")
                        .HasColumnType("bigint");

                    b.Property<Guid>("RowVersionId")
                        .IsConcurrencyToken()
                        .HasColumnType("uniqueidentifier");

                    b.Property<long>("UserId")
                        .HasColumnType("bigint");

                    b.HasKey("Id");

                    b.HasAlternateKey("PublicId");

                    b.HasAlternateKey("UserId", "RoleId");

                    b.HasIndex("RoleId");

                    b.HasIndex("UserId");

                    b.ToTable("UserRoles", (string)null);
                });

            modelBuilder.Entity("AzoresGov.Healthcare.Reimbursements.UnitOfWork.Entities.UserSessionEntity", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<long>("Id"));

                    b.Property<DateTimeOffset>("LastSeen")
                        .HasColumnType("datetimeoffset");

                    b.Property<string>("NetworkAddress")
                        .IsRequired()
                        .HasMaxLength(64)
                        .HasColumnType("nvarchar(64)");

                    b.Property<Guid>("PublicId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("RowVersionId")
                        .IsConcurrencyToken()
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTimeOffset>("Start")
                        .HasColumnType("datetimeoffset");

                    b.Property<long>("UserAgentId")
                        .HasColumnType("bigint");

                    b.Property<long>("UserId")
                        .HasColumnType("bigint");

                    b.HasKey("Id");

                    b.HasAlternateKey("PublicId");

                    b.HasIndex("UserAgentId");

                    b.HasIndex("UserId");

                    b.ToTable("UserSessions", (string)null);
                });

            modelBuilder.Entity("AzoresGov.Healthcare.Reimbursements.UnitOfWork.LogicalAreas.UserPermissionResultEntity", b =>
                {
                    b.Property<long>("UserId")
                        .HasColumnType("bigint");

                    b.Property<long>("PermissionId")
                        .HasColumnType("bigint");

                    b.Property<bool>("Granted")
                        .HasColumnType("bit");

                    b.HasKey("UserId", "PermissionId");

                    b.ToTable((string)null);

                    b.ToView("UserPermissionResults", (string)null);
                });

            modelBuilder.Entity("AzoresGov.Healthcare.Reimbursements.UnitOfWork.Entities.EntityParentEntity", b =>
                {
                    b.HasOne("AzoresGov.Healthcare.Reimbursements.UnitOfWork.Entities.EntityEntity", "Entity")
                        .WithMany()
                        .HasForeignKey("EntityId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("AzoresGov.Healthcare.Reimbursements.UnitOfWork.Entities.EntityEntity", "ParentEntity")
                        .WithMany()
                        .HasForeignKey("ParentEntityId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("Entity");

                    b.Navigation("ParentEntity");
                });

            modelBuilder.Entity("AzoresGov.Healthcare.Reimbursements.UnitOfWork.Entities.RolePermissionEntity", b =>
                {
                    b.HasOne("AzoresGov.Healthcare.Reimbursements.UnitOfWork.Entities.PermissionEntity", "Permission")
                        .WithMany()
                        .HasForeignKey("PermissionId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("AzoresGov.Healthcare.Reimbursements.UnitOfWork.Entities.RoleEntity", "Role")
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Permission");

                    b.Navigation("Role");
                });

            modelBuilder.Entity("AzoresGov.Healthcare.Reimbursements.UnitOfWork.Entities.UserEmailAddressEntity", b =>
                {
                    b.HasOne("AzoresGov.Healthcare.Reimbursements.UnitOfWork.Entities.UserEntity", "User")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("User");
                });

            modelBuilder.Entity("AzoresGov.Healthcare.Reimbursements.UnitOfWork.Entities.UserEntityEntity", b =>
                {
                    b.HasOne("AzoresGov.Healthcare.Reimbursements.UnitOfWork.Entities.EntityEntity", "Entity")
                        .WithMany()
                        .HasForeignKey("EntityId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("AzoresGov.Healthcare.Reimbursements.UnitOfWork.Entities.UserEntity", "User")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Entity");

                    b.Navigation("User");
                });

            modelBuilder.Entity("AzoresGov.Healthcare.Reimbursements.UnitOfWork.Entities.UserEntityPermissionEntity", b =>
                {
                    b.HasOne("AzoresGov.Healthcare.Reimbursements.UnitOfWork.Entities.EntityEntity", "Entity")
                        .WithMany()
                        .HasForeignKey("EntityId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("AzoresGov.Healthcare.Reimbursements.UnitOfWork.Entities.PermissionEntity", "Permission")
                        .WithMany()
                        .HasForeignKey("PermissionId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("AzoresGov.Healthcare.Reimbursements.UnitOfWork.Entities.UserEntity", "User")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Entity");

                    b.Navigation("Permission");

                    b.Navigation("User");
                });

            modelBuilder.Entity("AzoresGov.Healthcare.Reimbursements.UnitOfWork.Entities.UserEntityRoleEntity", b =>
                {
                    b.HasOne("AzoresGov.Healthcare.Reimbursements.UnitOfWork.Entities.EntityEntity", "Entity")
                        .WithMany()
                        .HasForeignKey("EntityId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("AzoresGov.Healthcare.Reimbursements.UnitOfWork.Entities.RoleEntity", "Role")
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("AzoresGov.Healthcare.Reimbursements.UnitOfWork.Entities.UserEntity", "User")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Entity");

                    b.Navigation("Role");

                    b.Navigation("User");
                });

            modelBuilder.Entity("AzoresGov.Healthcare.Reimbursements.UnitOfWork.Entities.UserPasswordEntity", b =>
                {
                    b.HasOne("AzoresGov.Healthcare.Reimbursements.UnitOfWork.Entities.UserEntity", "User")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("User");
                });

            modelBuilder.Entity("AzoresGov.Healthcare.Reimbursements.UnitOfWork.Entities.UserPermissionEntity", b =>
                {
                    b.HasOne("AzoresGov.Healthcare.Reimbursements.UnitOfWork.Entities.PermissionEntity", "Permission")
                        .WithMany()
                        .HasForeignKey("PermissionId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("AzoresGov.Healthcare.Reimbursements.UnitOfWork.Entities.UserEntity", "User")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Permission");

                    b.Navigation("User");
                });

            modelBuilder.Entity("AzoresGov.Healthcare.Reimbursements.UnitOfWork.Entities.UserRoleEntity", b =>
                {
                    b.HasOne("AzoresGov.Healthcare.Reimbursements.UnitOfWork.Entities.RoleEntity", "Role")
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("AzoresGov.Healthcare.Reimbursements.UnitOfWork.Entities.UserEntity", "User")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Role");

                    b.Navigation("User");
                });

            modelBuilder.Entity("AzoresGov.Healthcare.Reimbursements.UnitOfWork.Entities.UserSessionEntity", b =>
                {
                    b.HasOne("AzoresGov.Healthcare.Reimbursements.UnitOfWork.Entities.UserAgentEntity", "UserAgent")
                        .WithMany()
                        .HasForeignKey("UserAgentId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("AzoresGov.Healthcare.Reimbursements.UnitOfWork.Entities.UserEntity", "User")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("User");

                    b.Navigation("UserAgent");
                });
#pragma warning restore 612, 618
        }
    }
}

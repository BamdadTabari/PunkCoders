﻿// <auto-generated />
using System;
using DataProvider.EntityFramework.Configs;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace DataProvider.Migrations
{
    [DbContext(typeof(AppDbContext))]
    [Migration("20241129201750_Init2")]
    partial class Init2
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "9.0.0")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("DataProvider.EntityFramework.Entities.Blog.Post", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("AuthorId")
                        .HasColumnType("int");

                    b.Property<string>("Content")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime2");

                    b.Property<string>("Image")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("bit");

                    b.Property<bool>("IsPublished")
                        .HasColumnType("bit");

                    b.Property<int>("LikeCount")
                        .HasColumnType("int");

                    b.Property<int>("PostCategoryId")
                        .HasColumnType("int");

                    b.Property<string>("ShortDescription")
                        .IsRequired()
                        .HasMaxLength(500)
                        .HasColumnType("nvarchar(500)");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasMaxLength(200)
                        .IsUnicode(true)
                        .HasColumnType("nvarchar(200)");

                    b.Property<DateTime>("UpdatedAt")
                        .HasColumnType("datetime2");

                    b.Property<int>("ViewCount")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("AuthorId");

                    b.HasIndex("PostCategoryId");

                    b.ToTable("Post");
                });

            modelBuilder.Entity("DataProvider.EntityFramework.Entities.Blog.PostCategory", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime2");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("bit");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(100)
                        .IsUnicode(true)
                        .HasColumnType("nvarchar(100)");

                    b.Property<DateTime>("UpdatedAt")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.ToTable("PostCategory");
                });

            modelBuilder.Entity("DataProvider.EntityFramework.Entities.Blog.PostComment", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("AuthorName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime2");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("bit");

                    b.Property<int>("PostId")
                        .HasColumnType("int");

                    b.Property<string>("Text")
                        .IsRequired()
                        .HasMaxLength(800)
                        .HasColumnType("nvarchar(800)");

                    b.Property<DateTime>("UpdatedAt")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.HasIndex("PostId");

                    b.ToTable("PostComment");
                });

            modelBuilder.Entity("DataProvider.EntityFramework.Entities.Identity.BlacklistedToken", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<DateTime>("BlacklistedOn")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("ExpiryDate")
                        .HasColumnType("datetime2");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("bit");

                    b.Property<string>("Token")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("UpdatedAt")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.ToTable("BlacklistedToken");
                });

            modelBuilder.Entity("DataProvider.EntityFramework.Entities.Identity.Role", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime2");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("bit");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasMaxLength(140)
                        .HasColumnType("nvarchar(140)");

                    b.Property<DateTime>("UpdatedAt")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.ToTable("Role");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            CreatedAt = new DateTime(2024, 11, 29, 12, 17, 50, 281, DateTimeKind.Local).AddTicks(5129),
                            IsDeleted = false,
                            Title = "Owner",
                            UpdatedAt = new DateTime(2024, 11, 29, 12, 17, 50, 281, DateTimeKind.Local).AddTicks(5141)
                        },
                        new
                        {
                            Id = 2,
                            CreatedAt = new DateTime(2024, 11, 29, 12, 17, 50, 281, DateTimeKind.Local).AddTicks(5156),
                            IsDeleted = false,
                            Title = "Admin",
                            UpdatedAt = new DateTime(2024, 11, 29, 12, 17, 50, 281, DateTimeKind.Local).AddTicks(5158)
                        },
                        new
                        {
                            Id = 3,
                            CreatedAt = new DateTime(2024, 11, 29, 12, 17, 50, 281, DateTimeKind.Local).AddTicks(5161),
                            IsDeleted = false,
                            Title = "Writer",
                            UpdatedAt = new DateTime(2024, 11, 29, 12, 17, 50, 281, DateTimeKind.Local).AddTicks(5162)
                        },
                        new
                        {
                            Id = 4,
                            CreatedAt = new DateTime(2024, 11, 29, 12, 17, 50, 281, DateTimeKind.Local).AddTicks(5164),
                            IsDeleted = false,
                            Title = "Reader",
                            UpdatedAt = new DateTime(2024, 11, 29, 12, 17, 50, 281, DateTimeKind.Local).AddTicks(5165)
                        });
                });

            modelBuilder.Entity("DataProvider.EntityFramework.Entities.Identity.User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .IsRequired()
                        .HasMaxLength(32)
                        .HasColumnType("nvarchar(32)");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime2");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("FailedLoginCount")
                        .HasColumnType("int");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("bit");

                    b.Property<bool>("IsEmailConfirmed")
                        .HasColumnType("bit");

                    b.Property<bool>("IsLockedOut")
                        .HasColumnType("bit");

                    b.Property<bool>("IsMobileConfirmed")
                        .HasColumnType("bit");

                    b.Property<DateTime?>("LastLoginDate")
                        .HasColumnType("datetime2");

                    b.Property<DateTime?>("LastPasswordChangeTime")
                        .HasColumnType("datetime2");

                    b.Property<DateTime?>("LockoutEndTime")
                        .HasColumnType("datetime2");

                    b.Property<string>("Mobile")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PasswordHash")
                        .IsRequired()
                        .HasMaxLength(128)
                        .HasColumnType("nvarchar(128)");

                    b.Property<string>("SecurityStamp")
                        .IsRequired()
                        .HasMaxLength(32)
                        .HasColumnType("nchar(32)")
                        .IsFixedLength();

                    b.Property<string>("State")
                        .IsRequired()
                        .HasMaxLength(9)
                        .HasColumnType("nvarchar(9)");

                    b.Property<DateTime>("UpdatedAt")
                        .HasColumnType("datetime2");

                    b.Property<string>("Username")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.HasKey("Id");

                    b.HasIndex("Username")
                        .IsUnique();

                    b.ToTable("User");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            ConcurrencyStamp = "GJTGHVCXAUB6HNP2G792MM1BLZSFQ4ZC",
                            CreatedAt = new DateTime(2024, 11, 29, 12, 17, 50, 307, DateTimeKind.Local).AddTicks(3382),
                            Email = "bamdadtabari@outlook.com",
                            FailedLoginCount = 0,
                            IsDeleted = false,
                            IsEmailConfirmed = false,
                            IsLockedOut = false,
                            IsMobileConfirmed = false,
                            LastPasswordChangeTime = new DateTime(2024, 11, 29, 12, 17, 50, 307, DateTimeKind.Local).AddTicks(2909),
                            Mobile = "09301724389",
                            PasswordHash = "K+OT46JO1mOyM2ssV1kk5UtqYjRwioEBrMe6N6pZgak=.U2joiPVNNgMNGliJSILOow==",
                            SecurityStamp = "NUQJOS31XX2T3ZGVLUSR6WUA7ZXFVUSD",
                            State = "Active",
                            UpdatedAt = new DateTime(2024, 11, 29, 12, 17, 50, 307, DateTimeKind.Local).AddTicks(3391),
                            Username = "Illegible_Owner"
                        });
                });

            modelBuilder.Entity("DataProvider.EntityFramework.Entities.Identity.UserRole", b =>
                {
                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.Property<int>("RoleId")
                        .HasColumnType("int");

                    b.Property<int>("Id")
                        .HasColumnType("int");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime2");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("bit");

                    b.Property<DateTime>("UpdatedAt")
                        .HasColumnType("datetime2");

                    b.HasKey("UserId", "RoleId", "Id");

                    b.HasIndex("RoleId");

                    b.ToTable("UserRole");

                    b.HasData(
                        new
                        {
                            UserId = 1,
                            RoleId = 1,
                            Id = 0,
                            CreatedAt = new DateTime(2024, 11, 29, 12, 17, 50, 278, DateTimeKind.Local).AddTicks(5726),
                            IsDeleted = false,
                            UpdatedAt = new DateTime(2024, 11, 29, 12, 17, 50, 280, DateTimeKind.Local).AddTicks(7286)
                        });
                });

            modelBuilder.Entity("DataProvider.EntityFramework.Entities.Blog.Post", b =>
                {
                    b.HasOne("DataProvider.EntityFramework.Entities.Identity.User", "Author")
                        .WithMany("Posts")
                        .HasForeignKey("AuthorId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("DataProvider.EntityFramework.Entities.Blog.PostCategory", "PostCategory")
                        .WithMany("Posts")
                        .HasForeignKey("PostCategoryId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("Author");

                    b.Navigation("PostCategory");
                });

            modelBuilder.Entity("DataProvider.EntityFramework.Entities.Blog.PostComment", b =>
                {
                    b.HasOne("DataProvider.EntityFramework.Entities.Blog.Post", "Post")
                        .WithMany("PostComments")
                        .HasForeignKey("PostId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Post");
                });

            modelBuilder.Entity("DataProvider.EntityFramework.Entities.Identity.UserRole", b =>
                {
                    b.HasOne("DataProvider.EntityFramework.Entities.Identity.Role", "Role")
                        .WithMany("UserRoles")
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("DataProvider.EntityFramework.Entities.Identity.User", "User")
                        .WithMany("UserRoles")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Role");

                    b.Navigation("User");
                });

            modelBuilder.Entity("DataProvider.EntityFramework.Entities.Blog.Post", b =>
                {
                    b.Navigation("PostComments");
                });

            modelBuilder.Entity("DataProvider.EntityFramework.Entities.Blog.PostCategory", b =>
                {
                    b.Navigation("Posts");
                });

            modelBuilder.Entity("DataProvider.EntityFramework.Entities.Identity.Role", b =>
                {
                    b.Navigation("UserRoles");
                });

            modelBuilder.Entity("DataProvider.EntityFramework.Entities.Identity.User", b =>
                {
                    b.Navigation("Posts");

                    b.Navigation("UserRoles");
                });
#pragma warning restore 612, 618
        }
    }
}
﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using TestsStore.Api.Infrastructure;

namespace TestsStore.Api.Infrastructure.Migrations
{
    [DbContext(typeof(TestsStoreContext))]
    [Migration("20180913201209_Initial")]
    partial class Initial
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.1.3-rtm-32065")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("TestsStore.Api.Model.Build", b =>
                {
                    b.Property<Guid>("Id");

                    b.Property<DateTime>("EndTime");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(100);

                    b.Property<Guid>("ProjectId");

                    b.Property<DateTime>("StartTime");

                    b.Property<Guid>("StatusId");

                    b.HasKey("Id");

                    b.HasIndex("ProjectId");

                    b.HasIndex("StatusId");

                    b.ToTable("Build");
                });

            modelBuilder.Entity("TestsStore.Api.Model.Project", b =>
                {
                    b.Property<Guid>("Id");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(100);

                    b.HasKey("Id");

                    b.HasIndex("Name")
                        .IsUnique();

                    b.ToTable("Project");
                });

            modelBuilder.Entity("TestsStore.Api.Model.Status", b =>
                {
                    b.Property<Guid>("Id")
                        .HasDefaultValue(new Guid("a07ce5c3-8b7b-4fd6-b1e7-53ac28ed4e64"));

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(50);

                    b.HasKey("Id");

                    b.ToTable("Status");
                });

            modelBuilder.Entity("TestsStore.Api.Model.Test", b =>
                {
                    b.Property<Guid>("Id");

                    b.Property<string>("FullName")
                        .IsRequired()
                        .HasMaxLength(1000);

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(300);

                    b.Property<Guid>("ProjectId");

                    b.HasKey("Id");

                    b.HasIndex("ProjectId");

                    b.ToTable("Test");
                });

            modelBuilder.Entity("TestsStore.Api.Model.TestResult", b =>
                {
                    b.Property<Guid>("Id");

                    b.Property<Guid>("BuildId");

                    b.Property<TimeSpan>("Duration");

                    b.Property<string>("ErrorMessage")
                        .HasMaxLength(5000);

                    b.Property<string>("Messages")
                        .HasMaxLength(5000);

                    b.Property<string>("StackTrace")
                        .HasMaxLength(5000);

                    b.Property<Guid>("StatusId");

                    b.Property<Guid>("TestId");

                    b.HasKey("Id");

                    b.HasIndex("BuildId");

                    b.HasIndex("StatusId");

                    b.HasIndex("TestId");

                    b.ToTable("TestResult");
                });

            modelBuilder.Entity("TestsStore.Api.Model.Build", b =>
                {
                    b.HasOne("TestsStore.Api.Model.Project", "Project")
                        .WithMany("Builds")
                        .HasForeignKey("ProjectId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("TestsStore.Api.Model.Status", "Status")
                        .WithMany()
                        .HasForeignKey("StatusId")
                        .OnDelete(DeleteBehavior.Restrict);
                });

            modelBuilder.Entity("TestsStore.Api.Model.Test", b =>
                {
                    b.HasOne("TestsStore.Api.Model.Project", "Project")
                        .WithMany("Tests")
                        .HasForeignKey("ProjectId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("TestsStore.Api.Model.TestResult", b =>
                {
                    b.HasOne("TestsStore.Api.Model.Build", "Build")
                        .WithMany("TestResults")
                        .HasForeignKey("BuildId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("TestsStore.Api.Model.Status", "Status")
                        .WithMany()
                        .HasForeignKey("StatusId")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.HasOne("TestsStore.Api.Model.Test", "Test")
                        .WithMany("TestResults")
                        .HasForeignKey("TestId")
                        .OnDelete(DeleteBehavior.Restrict);
                });
#pragma warning restore 612, 618
        }
    }
}

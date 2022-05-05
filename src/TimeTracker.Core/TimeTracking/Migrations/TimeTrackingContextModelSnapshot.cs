﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using TimeTracker.Core.TimeTracking.Persistence;

#nullable disable

namespace TimeTracker.Core.TimeTracking.Migrations
{
    [DbContext(typeof(TimeTrackingContext))]
    partial class TimeTrackingContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasDefaultSchema("time-tracker")
                .HasAnnotation("ProductVersion", "6.0.2")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("TimeTracker.Core.TimeTracking.Models.Entities.Activity", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<int>("CategoryId")
                        .HasColumnType("int");

                    b.Property<string>("CreatedBy")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("CreatedTime")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("Date")
                        .HasColumnType("datetime2");

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("DurationInSeconds")
                        .HasColumnType("int");

                    b.Property<string>("IndividualId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("LastModifiedBy")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("LastModifiedTime")
                        .HasColumnType("datetime2");

                    b.Property<int>("MandateId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("CategoryId");

                    b.HasIndex("IndividualId");

                    b.HasIndex("MandateId");

                    b.ToTable("Activities", "time-tracker");
                });

            modelBuilder.Entity("TimeTracker.Core.TimeTracking.Models.Entities.ActivityLabel", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("ColorCode")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("IndividualId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Id");

                    b.HasIndex("IndividualId");

                    b.HasIndex("Name")
                        .IsUnique();

                    b.ToTable("ActivityLabels", "time-tracker");
                });

            modelBuilder.Entity("TimeTracker.Core.TimeTracking.Models.Entities.Category", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("ColorCode")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("IconUrl")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("IndividualId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Priority")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("IndividualId");

                    b.HasIndex("Name")
                        .IsUnique();

                    b.ToTable("Categories", "time-tracker");
                });

            modelBuilder.Entity("TimeTracker.Core.TimeTracking.Models.Entities.Mandate", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("ColorCode")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("IndividualId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Id");

                    b.HasIndex("IndividualId");

                    b.HasIndex("Name")
                        .IsUnique();

                    b.ToTable("Mandates", "time-tracker");
                });

            modelBuilder.Entity("TimeTracker.Core.TimeTracking.Models.Entities.Tag", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<int>("ActivityId")
                        .HasColumnType("int");

                    b.Property<int>("ActivityLabelId")
                        .HasColumnType("int");

                    b.Property<string>("IndividualId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Id");

                    b.HasIndex("ActivityId");

                    b.HasIndex("ActivityLabelId");

                    b.HasIndex("IndividualId");

                    b.ToTable("Tag", "time-tracker");
                });

            modelBuilder.Entity("TimeTracker.Core.TimeTracking.Models.Entities.Activity", b =>
                {
                    b.HasOne("TimeTracker.Core.TimeTracking.Models.Entities.Category", "Category")
                        .WithMany()
                        .HasForeignKey("CategoryId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("TimeTracker.Core.TimeTracking.Models.Entities.Mandate", "Mandate")
                        .WithMany()
                        .HasForeignKey("MandateId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Category");

                    b.Navigation("Mandate");
                });

            modelBuilder.Entity("TimeTracker.Core.TimeTracking.Models.Entities.Tag", b =>
                {
                    b.HasOne("TimeTracker.Core.TimeTracking.Models.Entities.Activity", "Activity")
                        .WithMany("Tags")
                        .HasForeignKey("ActivityId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("TimeTracker.Core.TimeTracking.Models.Entities.ActivityLabel", "ActivityLabel")
                        .WithMany("Tags")
                        .HasForeignKey("ActivityLabelId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Activity");

                    b.Navigation("ActivityLabel");
                });

            modelBuilder.Entity("TimeTracker.Core.TimeTracking.Models.Entities.Activity", b =>
                {
                    b.Navigation("Tags");
                });

            modelBuilder.Entity("TimeTracker.Core.TimeTracking.Models.Entities.ActivityLabel", b =>
                {
                    b.Navigation("Tags");
                });
#pragma warning restore 612, 618
        }
    }
}

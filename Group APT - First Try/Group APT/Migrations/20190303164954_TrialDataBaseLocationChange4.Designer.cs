﻿// <auto-generated />
using System;
using Group_APT.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Group_APT.Migrations
{
    [DbContext(typeof(ExaminationContext))]
    [Migration("20190303164954_TrialDataBaseLocationChange4")]
    partial class TrialDataBaseLocationChange4
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.2.2-servicing-10034")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("Group_APT.Models.Examination", b =>
                {
                    b.Property<string>("Paper_Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Location_Id");

                    b.Property<int>("QuestionAmount");

                    b.Property<string>("SelectionType");

                    b.Property<DateTime>("Time");

                    b.HasKey("Paper_Id");

                    b.ToTable("Examinations");
                });

            modelBuilder.Entity("Group_APT.Models.Lecturer", b =>
                {
                    b.Property<string>("UniversityLecturerId")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("LecturerId");

                    b.Property<string>("Name");

                    b.Property<string>("Surname");

                    b.HasKey("UniversityLecturerId");

                    b.ToTable("Lecturers");
                });

            modelBuilder.Entity("Group_APT.Models.Student", b =>
                {
                    b.Property<string>("UniversityStudentId")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Name");

                    b.Property<string>("StudentId");

                    b.Property<string>("StudentImageLocation");

                    b.Property<string>("Surname");

                    b.HasKey("UniversityStudentId");

                    b.ToTable("Students");
                });

            modelBuilder.Entity("Group_APT.Models.StudentUnit_Relationship", b =>
                {
                    b.Property<int>("RelationId")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Code");

                    b.Property<string>("UniversityStudentId");

                    b.HasKey("RelationId");

                    b.HasIndex("Code");

                    b.HasIndex("UniversityStudentId");

                    b.ToTable("StudentUnitRelationships");
                });

            modelBuilder.Entity("Group_APT.Models.Unit", b =>
                {
                    b.Property<string>("Code")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Name");

                    b.HasKey("Code");

                    b.ToTable("Units");
                });

            modelBuilder.Entity("Group_APT.Models.StudentUnit_Relationship", b =>
                {
                    b.HasOne("Group_APT.Models.Unit", "UnitRelation")
                        .WithMany()
                        .HasForeignKey("Code");

                    b.HasOne("Group_APT.Models.Student", "StudentRelation")
                        .WithMany()
                        .HasForeignKey("UniversityStudentId");
                });
#pragma warning restore 612, 618
        }
    }
}
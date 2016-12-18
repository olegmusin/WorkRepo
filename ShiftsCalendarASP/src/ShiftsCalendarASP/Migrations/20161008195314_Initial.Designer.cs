using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using ShiftsCalendarASP.Models;

namespace ShiftsCalendarASP.Migrations
{
    [DbContext(typeof(ShiftsCalendarContext))]
    [Migration("20161008195314_Initial")]
    partial class Initial
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
            modelBuilder
                .HasAnnotation("ProductVersion", "1.0.1")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("ShiftsCalendar.Models.Project", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Address")
                        .IsRequired();

                    b.Property<string>("Name");

                    b.Property<int>("NumberOfWorkers");

                    b.HasKey("Id");

                    b.ToTable("Projects");
                });

            modelBuilder.Entity("ShiftsCalendar.Models.Shift", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("Date");

                    b.Property<int?>("ProjectId");

                    b.HasKey("Id");

                    b.HasIndex("ProjectId");

                    b.ToTable("Shifts");
                });

            modelBuilder.Entity("ShiftsCalendar.Models.Worker", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Name");

                    b.Property<decimal>("Salary");

                    b.Property<string>("Specialty");

                    b.HasKey("Id");

                    b.ToTable("Workers");
                });

            modelBuilder.Entity("ShiftsCalendar.Models.WorkerShift", b =>
                {
                    b.Property<int>("ShiftId");

                    b.Property<int>("WorkerId");

                    b.HasKey("ShiftId", "WorkerId");

                    b.HasIndex("ShiftId");

                    b.HasIndex("WorkerId");

                    b.ToTable("WorkerShift");
                });

            modelBuilder.Entity("ShiftsCalendar.Models.Shift", b =>
                {
                    b.HasOne("ShiftsCalendar.Models.Project", "Project")
                        .WithMany("Shifts")
                        .HasForeignKey("ProjectId");
                });

            modelBuilder.Entity("ShiftsCalendar.Models.WorkerShift", b =>
                {
                    b.HasOne("ShiftsCalendar.Models.Shift", "Shift")
                        .WithMany("Workers")
                        .HasForeignKey("ShiftId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("ShiftsCalendar.Models.Worker", "Worker")
                        .WithMany("Shifts")
                        .HasForeignKey("WorkerId")
                        .OnDelete(DeleteBehavior.Cascade);
                });
        }
    }
}

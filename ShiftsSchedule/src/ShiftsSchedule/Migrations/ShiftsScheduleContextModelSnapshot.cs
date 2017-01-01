using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using ShiftsSchedule.Data;

namespace ShiftsSchedule.Migrations
{
    [DbContext(typeof(ShiftsScheduleContext))]
    partial class ShiftsScheduleContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
            modelBuilder
                .HasAnnotation("ProductVersion", "1.0.1")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("ShiftsSchedule.Models.Project", b =>
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

            modelBuilder.Entity("ShiftsSchedule.Models.Shift", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("Date");

                    b.Property<bool>("IsCanceled");

                    b.Property<int?>("ProjectId");

                    b.HasKey("Id");

                    b.HasIndex("ProjectId");

                    b.ToTable("Shifts");
                });

            modelBuilder.Entity("ShiftsSchedule.Models.Worker", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Name");

                    b.Property<decimal>("Salary");

                    b.Property<string>("Specialty");

                    b.HasKey("Id");

                    b.ToTable("Workers");
                });

            modelBuilder.Entity("ShiftsSchedule.Models.WorkerShift", b =>
                {
                    b.Property<int>("ShiftId");

                    b.Property<int>("WorkerId");

                    b.HasKey("ShiftId", "WorkerId");

                    b.HasIndex("ShiftId");

                    b.HasIndex("WorkerId");

                    b.ToTable("WorkerShift");
                });

            modelBuilder.Entity("ShiftsSchedule.Models.Shift", b =>
                {
                    b.HasOne("ShiftsSchedule.Models.Project", "Project")
                        .WithMany("Shifts")
                        .HasForeignKey("ProjectId");
                });

            modelBuilder.Entity("ShiftsSchedule.Models.WorkerShift", b =>
                {
                    b.HasOne("ShiftsSchedule.Models.Shift", "Shift")
                        .WithMany("Workers")
                        .HasForeignKey("ShiftId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("ShiftsSchedule.Models.Worker", "Worker")
                        .WithMany("Shifts")
                        .HasForeignKey("WorkerId")
                        .OnDelete(DeleteBehavior.Cascade);
                });
        }
    }
}

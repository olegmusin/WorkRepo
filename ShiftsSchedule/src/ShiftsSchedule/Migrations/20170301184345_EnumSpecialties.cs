using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Metadata;

namespace ShiftsSchedule.Migrations
{
    public partial class EnumSpecialties : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Specialty",
                table: "Workers");

            migrationBuilder.AddColumn<int>(
                name: "SpecialtyId",
                table: "Workers",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Specialties",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(nullable: true),
                    ShiftId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Specialties", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Specialties_Shifts_ShiftId",
                        column: x => x.ShiftId,
                        principalTable: "Shifts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Workers_SpecialtyId",
                table: "Workers",
                column: "SpecialtyId");

            migrationBuilder.CreateIndex(
                name: "IX_Specialties_ShiftId",
                table: "Specialties",
                column: "ShiftId");

            migrationBuilder.AddForeignKey(
                name: "FK_Workers_Specialties_SpecialtyId",
                table: "Workers",
                column: "SpecialtyId",
                principalTable: "Specialties",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Workers_Specialties_SpecialtyId",
                table: "Workers");

            migrationBuilder.DropTable(
                name: "Specialties");

            migrationBuilder.DropIndex(
                name: "IX_Workers_SpecialtyId",
                table: "Workers");

            migrationBuilder.DropColumn(
                name: "SpecialtyId",
                table: "Workers");

            migrationBuilder.AddColumn<string>(
                name: "Specialty",
                table: "Workers",
                nullable: true);
        }
    }
}

using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ScooterRental.Data.Migrations
{
    public partial class Init : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "RentalCompanies",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RentalCompanies", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "RentalDetail",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    ScooterId = table.Column<int>(type: "INTEGER", nullable: false),
                    StartTime = table.Column<DateTime>(type: "TEXT", nullable: false),
                    EndTime = table.Column<DateTime>(type: "TEXT", nullable: true),
                    PricePerMinute = table.Column<decimal>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RentalDetail", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Scooters",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    PricePerMinute = table.Column<decimal>(type: "TEXT", nullable: false),
                    IsRented = table.Column<bool>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Scooters", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "CompanyRentalDetails",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    RentalCompanyId = table.Column<int>(type: "INTEGER", nullable: true),
                    RentalDetailId = table.Column<int>(type: "INTEGER", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CompanyRentalDetails", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CompanyRentalDetails_RentalCompanies_RentalCompanyId",
                        column: x => x.RentalCompanyId,
                        principalTable: "RentalCompanies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_CompanyRentalDetails_RentalDetail_RentalDetailId",
                        column: x => x.RentalDetailId,
                        principalTable: "RentalDetail",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "CompaniesScooters",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    RentalCompanyId = table.Column<int>(type: "INTEGER", nullable: true),
                    ScooterId = table.Column<int>(type: "INTEGER", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CompaniesScooters", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CompaniesScooters_RentalCompanies_RentalCompanyId",
                        column: x => x.RentalCompanyId,
                        principalTable: "RentalCompanies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_CompaniesScooters_Scooters_ScooterId",
                        column: x => x.ScooterId,
                        principalTable: "Scooters",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CompaniesScooters_RentalCompanyId",
                table: "CompaniesScooters",
                column: "RentalCompanyId");

            migrationBuilder.CreateIndex(
                name: "IX_CompaniesScooters_ScooterId",
                table: "CompaniesScooters",
                column: "ScooterId");

            migrationBuilder.CreateIndex(
                name: "IX_CompanyRentalDetails_RentalCompanyId",
                table: "CompanyRentalDetails",
                column: "RentalCompanyId");

            migrationBuilder.CreateIndex(
                name: "IX_CompanyRentalDetails_RentalDetailId",
                table: "CompanyRentalDetails",
                column: "RentalDetailId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CompaniesScooters");

            migrationBuilder.DropTable(
                name: "CompanyRentalDetails");

            migrationBuilder.DropTable(
                name: "Scooters");

            migrationBuilder.DropTable(
                name: "RentalCompanies");

            migrationBuilder.DropTable(
                name: "RentalDetail");
        }
    }
}

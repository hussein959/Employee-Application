﻿using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace webWithSQL.Migrations
{
    /// <inheritdoc />
    public partial class InitialMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "dbo");

            migrationBuilder.CreateTable(
                name: "Department",
                schema: "dbo",
                columns: table => new
                {
                    dapartmentID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DepartmentName = table.Column<string>(type: "varchar(150)", nullable: false),
                    DepartmentAbbr = table.Column<string>(type: "varchar(5)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Department", x => x.dapartmentID);
                });

            migrationBuilder.CreateTable(
                name: "Employee",
                schema: "dbo",
                columns: table => new
                {
                    EmployeeID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    EmployeeNumber = table.Column<string>(type: "varchar(5)", maxLength: 5, nullable: false),
                    EmployeeName = table.Column<string>(type: "varchar(150)", maxLength: 100, nullable: false),
                    DOB = table.Column<DateOnly>(type: "date", nullable: false),
                    HiringDate = table.Column<DateOnly>(type: "date", nullable: false),
                    GrossSalary = table.Column<decimal>(type: "decimal(18,0)", nullable: false),
                    NetSalary = table.Column<decimal>(type: "decimal(18,0)", nullable: false),
                    Departmentid = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Employee", x => x.EmployeeID);
                    table.ForeignKey(
                        name: "FK_Employee_Department_Departmentid",
                        column: x => x.Departmentid,
                        principalSchema: "dbo",
                        principalTable: "Department",
                        principalColumn: "dapartmentID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Employee_Departmentid",
                schema: "dbo",
                table: "Employee",
                column: "Departmentid");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Employee",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "Department",
                schema: "dbo");
        }
    }
}

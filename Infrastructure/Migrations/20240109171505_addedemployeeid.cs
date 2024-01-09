using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Bulldog.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class addedemployeeid : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Services_Employees_EmployeeId",
                table: "Services");

            migrationBuilder.AlterColumn<Guid>(
                name: "EmployeeId",
                table: "Services",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Services_Employees_EmployeeId",
                table: "Services",
                column: "EmployeeId",
                principalTable: "Employees",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Services_Employees_EmployeeId",
                table: "Services");

            migrationBuilder.AlterColumn<Guid>(
                name: "EmployeeId",
                table: "Services",
                type: "uniqueidentifier",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");

            migrationBuilder.AddForeignKey(
                name: "FK_Services_Employees_EmployeeId",
                table: "Services",
                column: "EmployeeId",
                principalTable: "Employees",
                principalColumn: "Id");
        }
    }
}

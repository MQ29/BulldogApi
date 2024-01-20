using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Bulldog.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class availablehours : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AvailableHours",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Hour = table.Column<DateTime>(type: "datetime2", nullable: false),
                    AvailableDateId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AvailableHours", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AvailableHours_AvailableDates_AvailableDateId",
                        column: x => x.AvailableDateId,
                        principalTable: "AvailableDates",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AvailableHours_AvailableDateId",
                table: "AvailableHours",
                column: "AvailableDateId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AvailableHours");
        }
    }
}

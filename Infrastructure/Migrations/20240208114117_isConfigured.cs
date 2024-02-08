using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Bulldog.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class isConfigured : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsConfigured",
                table: "Companies",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsConfigured",
                table: "AspNetUsers",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsConfigured",
                table: "Companies");

            migrationBuilder.DropColumn(
                name: "IsConfigured",
                table: "AspNetUsers");
        }
    }
}

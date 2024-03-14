using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AmeliyatDefteri.Migrations
{
    /// <inheritdoc />
    public partial class detay : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Detay",
                table: "Hastalar");

            migrationBuilder.AddColumn<string>(
                name: "Detay",
                table: "Zamanlar",
                type: "TEXT",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Detay",
                table: "Zamanlar");

            migrationBuilder.AddColumn<string>(
                name: "Detay",
                table: "Hastalar",
                type: "TEXT",
                nullable: true);
        }
    }
}

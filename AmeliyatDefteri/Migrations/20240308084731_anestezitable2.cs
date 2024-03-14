using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AmeliyatDefteri.Migrations
{
    /// <inheritdoc />
    public partial class anestezitable2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "AnesteziId",
                table: "Zamanlar",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Zamanlar_AnesteziId",
                table: "Zamanlar",
                column: "AnesteziId");

            migrationBuilder.AddForeignKey(
                name: "FK_Zamanlar_Anesteziler_AnesteziId",
                table: "Zamanlar",
                column: "AnesteziId",
                principalTable: "Anesteziler",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Zamanlar_Anesteziler_AnesteziId",
                table: "Zamanlar");

            migrationBuilder.DropIndex(
                name: "IX_Zamanlar_AnesteziId",
                table: "Zamanlar");

            migrationBuilder.DropColumn(
                name: "AnesteziId",
                table: "Zamanlar");
        }
    }
}

using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AmeliyatDefteri.Migrations
{
    /// <inheritdoc />
    public partial class removehasta : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Zamanlar_Hastalar_HastaId",
                table: "Zamanlar");

            migrationBuilder.DropTable(
                name: "Hastalar");

            migrationBuilder.DropIndex(
                name: "IX_Zamanlar_HastaId",
                table: "Zamanlar");

            migrationBuilder.DropColumn(
                name: "HastaId",
                table: "Zamanlar");

            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "Zamanlar",
                type: "TEXT",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Telefon",
                table: "Zamanlar",
                type: "TEXT",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Name",
                table: "Zamanlar");

            migrationBuilder.DropColumn(
                name: "Telefon",
                table: "Zamanlar");

            migrationBuilder.AddColumn<int>(
                name: "HastaId",
                table: "Zamanlar",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "Hastalar",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(type: "TEXT", nullable: false),
                    Telefon = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Hastalar", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Zamanlar_HastaId",
                table: "Zamanlar",
                column: "HastaId");

            migrationBuilder.AddForeignKey(
                name: "FK_Zamanlar_Hastalar_HastaId",
                table: "Zamanlar",
                column: "HastaId",
                principalTable: "Hastalar",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}

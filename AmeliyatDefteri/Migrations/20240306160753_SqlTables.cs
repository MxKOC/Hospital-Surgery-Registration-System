using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AmeliyatDefteri.Migrations
{
    /// <inheritdoc />
    public partial class SqlTables : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<bool>(
                name: "Anestezi",
                table: "Hastalar",
                type: "INTEGER",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "TEXT");

            migrationBuilder.CreateTable(
                name: "AmeliyatDoktor",
                columns: table => new
                {
                    AmeliyatlarId = table.Column<int>(type: "INTEGER", nullable: false),
                    DoktorlarId = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AmeliyatDoktor", x => new { x.AmeliyatlarId, x.DoktorlarId });
                    table.ForeignKey(
                        name: "FK_AmeliyatDoktor_Ameliyatlar_AmeliyatlarId",
                        column: x => x.AmeliyatlarId,
                        principalTable: "Ameliyatlar",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AmeliyatDoktor_Doktorlar_DoktorlarId",
                        column: x => x.DoktorlarId,
                        principalTable: "Doktorlar",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AmeliyatHasta",
                columns: table => new
                {
                    AmeliyatlarId = table.Column<int>(type: "INTEGER", nullable: false),
                    HastalarId = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AmeliyatHasta", x => new { x.AmeliyatlarId, x.HastalarId });
                    table.ForeignKey(
                        name: "FK_AmeliyatHasta_Ameliyatlar_AmeliyatlarId",
                        column: x => x.AmeliyatlarId,
                        principalTable: "Ameliyatlar",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AmeliyatHasta_Hastalar_HastalarId",
                        column: x => x.HastalarId,
                        principalTable: "Hastalar",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AmeliyatDoktor_DoktorlarId",
                table: "AmeliyatDoktor",
                column: "DoktorlarId");

            migrationBuilder.CreateIndex(
                name: "IX_AmeliyatHasta_HastalarId",
                table: "AmeliyatHasta",
                column: "HastalarId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AmeliyatDoktor");

            migrationBuilder.DropTable(
                name: "AmeliyatHasta");

            migrationBuilder.AlterColumn<string>(
                name: "Anestezi",
                table: "Hastalar",
                type: "TEXT",
                nullable: false,
                oldClrType: typeof(bool),
                oldType: "INTEGER");
        }
    }
}

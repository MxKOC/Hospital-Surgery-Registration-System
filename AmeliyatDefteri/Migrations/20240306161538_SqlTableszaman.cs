using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AmeliyatDefteri.Migrations
{
    /// <inheritdoc />
    public partial class SqlTableszaman : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Zamanlar",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    DoktorId = table.Column<int>(type: "INTEGER", nullable: false),
                    HastaId = table.Column<int>(type: "INTEGER", nullable: false),
                    AmeliyatId = table.Column<int>(type: "INTEGER", nullable: false),
                    AmeliyatGünü = table.Column<DateTime>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Zamanlar", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Zamanlar_Ameliyatlar_AmeliyatId",
                        column: x => x.AmeliyatId,
                        principalTable: "Ameliyatlar",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Zamanlar_Doktorlar_DoktorId",
                        column: x => x.DoktorId,
                        principalTable: "Doktorlar",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Zamanlar_Hastalar_HastaId",
                        column: x => x.HastaId,
                        principalTable: "Hastalar",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Zamanlar_AmeliyatId",
                table: "Zamanlar",
                column: "AmeliyatId");

            migrationBuilder.CreateIndex(
                name: "IX_Zamanlar_DoktorId",
                table: "Zamanlar",
                column: "DoktorId");

            migrationBuilder.CreateIndex(
                name: "IX_Zamanlar_HastaId",
                table: "Zamanlar",
                column: "HastaId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Zamanlar");
        }
    }
}

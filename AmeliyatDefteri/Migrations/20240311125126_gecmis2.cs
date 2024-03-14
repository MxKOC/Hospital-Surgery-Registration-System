using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AmeliyatDefteri.Migrations
{
    /// <inheritdoc />
    public partial class gecmis2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Gecmisler",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Kullanici = table.Column<string>(type: "TEXT", nullable: false),
                    IslemTarihi = table.Column<DateTime>(type: "TEXT", nullable: false),
                    Olay = table.Column<string>(type: "TEXT", nullable: false),
                    Name = table.Column<string>(type: "TEXT", nullable: false),
                    Telefon = table.Column<string>(type: "TEXT", nullable: true),
                    Detay = table.Column<string>(type: "TEXT", nullable: true),
                    AmeliyatGünü = table.Column<DateTime>(type: "TEXT", nullable: false),
                    Ameliyat = table.Column<string>(type: "TEXT", nullable: false),
                    Doktor = table.Column<string>(type: "TEXT", nullable: false),
                    Anestezi = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Gecmisler", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Gecmisler");
        }
    }
}

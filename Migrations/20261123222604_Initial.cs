using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Blog.Migrations
{
    /// <inheritdoc />
    public partial class Initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Articles",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Titre = table.Column<string>(type: "TEXT", nullable: false),
                    Resume = table.Column<string>(type: "TEXT", nullable: false),
                    DatePublication = table.Column<DateTime>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Articles", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "Articles",
                columns: new[] { "Id", "DatePublication", "Resume", "Titre" },
                values: new object[,]
                {
                    { 1, new DateTime(2026, 11, 2, 0, 0, 0, 0, DateTimeKind.Unspecified), "Le premier article de notre blog.", "Bienvenue sur le blog" },
                    { 2, new DateTime(2026, 11, 9, 0, 0, 0, 0, DateTimeKind.Unspecified), "Pourquoi ce framework est devenu un standard.", "Découvrir ASP.NET Core" },
                    { 3, new DateTime(2026, 11, 16, 0, 0, 0, 0, DateTimeKind.Unspecified), "Modèle, vue, contrôleur : qui fait quoi ?", "Le modèle MVC" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Articles");
        }
    }
}

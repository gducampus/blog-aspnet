using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Blog.Migrations
{
    /// <inheritdoc />
    public partial class AjoutAuteurs : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "AuteurId",
                table: "Articles",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "Auteurs",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Nom = table.Column<string>(type: "TEXT", nullable: false),
                    Email = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Auteurs", x => x.Id);
                });

            migrationBuilder.UpdateData(
                table: "Articles",
                keyColumn: "Id",
                keyValue: 1,
                column: "AuteurId",
                value: 1);

            migrationBuilder.UpdateData(
                table: "Articles",
                keyColumn: "Id",
                keyValue: 2,
                column: "AuteurId",
                value: 2);

            migrationBuilder.UpdateData(
                table: "Articles",
                keyColumn: "Id",
                keyValue: 3,
                column: "AuteurId",
                value: 1);

            migrationBuilder.InsertData(
                table: "Auteurs",
                columns: new[] { "Id", "Email", "Nom" },
                values: new object[,]
                {
                    { 1, "lea@blog.fr", "Léa Martin" },
                    { 2, "karim@blog.fr", "Karim Benali" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Articles_AuteurId",
                table: "Articles",
                column: "AuteurId");

            migrationBuilder.AddForeignKey(
                name: "FK_Articles_Auteurs_AuteurId",
                table: "Articles",
                column: "AuteurId",
                principalTable: "Auteurs",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Articles_Auteurs_AuteurId",
                table: "Articles");

            migrationBuilder.DropTable(
                name: "Auteurs");

            migrationBuilder.DropIndex(
                name: "IX_Articles_AuteurId",
                table: "Articles");

            migrationBuilder.DropColumn(
                name: "AuteurId",
                table: "Articles");
        }
    }
}

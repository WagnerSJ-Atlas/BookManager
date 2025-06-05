using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CadastroLivros.Infra.Migrations
{
    /// <inheritdoc />
    public partial class CriarTabelaBooks : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Books",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(maxLength: 200, nullable: false),
                    Author = table.Column<string>(maxLength: 100, nullable: false),
                    PublicationDate = table.Column<DateTime>(nullable: false),
                    Category = table.Column<string>(maxLength: 50, nullable: false),
                    Publisher = table.Column<string>(maxLength: 100, nullable: false),
                    ISBN13 = table.Column<string>(maxLength: 13, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Books", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}

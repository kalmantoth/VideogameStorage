using Microsoft.EntityFrameworkCore.Migrations;

namespace VideogameStorage.Migrations
{
    public partial class ModifiedReleaseDateRange : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Videogame",
                columns: table => new
                {
                    videogame_id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    name = table.Column<string>(type: "nvarchar(100)", nullable: false),
                    type = table.Column<string>(type: "nvarchar(100)", nullable: false),
                    release_date = table.Column<int>(type: "int", nullable: false),
                    rating = table.Column<float>(type: "float(1)", nullable: false),
                    console_exclusive = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Videogame", x => x.videogame_id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Videogame");
        }
    }
}

using Microsoft.EntityFrameworkCore.Migrations;

namespace SlidEnglish.Migrations
{
    public partial class AddSinonyms : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "WordSinonym",
                columns: table => new
                {
                    WordId = table.Column<int>(nullable: false),
                    SinonymId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WordSinonym", x => new { x.WordId, x.SinonymId });
                    table.ForeignKey(
                        name: "FK_WordSinonym_Words_SinonymId",
                        column: x => x.SinonymId,
                        principalTable: "Words",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_WordSinonym_Words_WordId",
                        column: x => x.WordId,
                        principalTable: "Words",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_WordSinonym_SinonymId",
                table: "WordSinonym",
                column: "SinonymId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "WordSinonym");
        }
    }
}

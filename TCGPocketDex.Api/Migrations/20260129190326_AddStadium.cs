using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TCGPocketDex.Api.Migrations
{
    /// <inheritdoc />
    public partial class AddStadium : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "CardStadium",
                schema: "data",
                columns: table => new
                {
                    CardId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CardStadium", x => x.CardId);
                    table.ForeignKey(
                        name: "FK_CardStadium_Card_CardId",
                        column: x => x.CardId,
                        principalSchema: "data",
                        principalTable: "Card",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CardStadium",
                schema: "data");
        }
    }
}

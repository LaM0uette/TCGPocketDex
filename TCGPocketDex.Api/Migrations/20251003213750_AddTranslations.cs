using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace TCGPocketDex.Api.Migrations
{
    /// <inheritdoc />
    public partial class AddTranslations : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "CardRarityTranslation",
                schema: "translation",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    CardRarityId = table.Column<int>(type: "integer", nullable: false),
                    Culture = table.Column<string>(type: "character varying(10)", maxLength: 10, nullable: false),
                    Name = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CardRarityTranslation", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CardRarityTranslation_CardRarity_CardRarityId",
                        column: x => x.CardRarityId,
                        principalSchema: "ref",
                        principalTable: "CardRarity",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CardSpecialTranslation",
                schema: "translation",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    CardSpecialId = table.Column<int>(type: "integer", nullable: false),
                    Culture = table.Column<string>(type: "character varying(10)", maxLength: 10, nullable: false),
                    Name = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CardSpecialTranslation", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CardSpecialTranslation_CardSpecial_CardSpecialId",
                        column: x => x.CardSpecialId,
                        principalSchema: "ref",
                        principalTable: "CardSpecial",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CardRarityTranslation_CardRarityId",
                schema: "translation",
                table: "CardRarityTranslation",
                column: "CardRarityId");

            migrationBuilder.CreateIndex(
                name: "IX_CardSpecialTranslation_CardSpecialId",
                schema: "translation",
                table: "CardSpecialTranslation",
                column: "CardSpecialId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CardRarityTranslation",
                schema: "translation");

            migrationBuilder.DropTable(
                name: "CardSpecialTranslation",
                schema: "translation");
        }
    }
}

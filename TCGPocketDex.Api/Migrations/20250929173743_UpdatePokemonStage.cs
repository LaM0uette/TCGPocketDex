using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace TCGPocketDex.Api.Migrations
{
    /// <inheritdoc />
    public partial class UpdatePokemonStage : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CardPokemons_PokemonStages_StageId",
                schema: "preprod",
                table: "CardPokemons");

            migrationBuilder.DropTable(
                name: "PokemonStages",
                schema: "preprod");

            migrationBuilder.DropIndex(
                name: "IX_CardPokemons_StageId",
                schema: "preprod",
                table: "CardPokemons");

            migrationBuilder.RenameColumn(
                name: "StageId",
                schema: "preprod",
                table: "CardPokemons",
                newName: "Stage");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Stage",
                schema: "preprod",
                table: "CardPokemons",
                newName: "StageId");

            migrationBuilder.CreateTable(
                name: "PokemonStages",
                schema: "preprod",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "character varying(30)", maxLength: 30, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PokemonStages", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CardPokemons_StageId",
                schema: "preprod",
                table: "CardPokemons",
                column: "StageId");

            migrationBuilder.AddForeignKey(
                name: "FK_CardPokemons_PokemonStages_StageId",
                schema: "preprod",
                table: "CardPokemons",
                column: "StageId",
                principalSchema: "preprod",
                principalTable: "PokemonStages",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}

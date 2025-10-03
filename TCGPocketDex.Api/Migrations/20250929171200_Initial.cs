using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace TCGPocketDex.Api.Migrations
{
    /// <inheritdoc />
    public partial class Initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "preprod");

            migrationBuilder.CreateTable(
                name: "CardRarities",
                schema: "preprod",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CardRarities", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "CardSets",
                schema: "preprod",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Code = table.Column<string>(type: "character varying(10)", maxLength: 10, nullable: false),
                    Series = table.Column<string>(type: "character varying(10)", maxLength: 10, nullable: false),
                    Name = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CardSets", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "PokemonAbilities",
                schema: "preprod",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PokemonAbilities", x => x.Id);
                });

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

            migrationBuilder.CreateTable(
                name: "PokemonTypes",
                schema: "preprod",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "character varying(30)", maxLength: 30, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PokemonTypes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Cards",
                schema: "preprod",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Kind = table.Column<int>(type: "integer", nullable: false),
                    Name = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    Description = table.Column<string>(type: "character varying(4000)", maxLength: 4000, nullable: true),
                    IsPromo = table.Column<bool>(type: "boolean", nullable: false),
                    CardRarityId = table.Column<int>(type: "integer", nullable: false),
                    CardSetId = table.Column<int>(type: "integer", nullable: true),
                    SerieNumber = table.Column<int>(type: "integer", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Cards", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Cards_CardRarities_CardRarityId",
                        column: x => x.CardRarityId,
                        principalSchema: "preprod",
                        principalTable: "CardRarities",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Cards_CardSets_CardSetId",
                        column: x => x.CardSetId,
                        principalSchema: "preprod",
                        principalTable: "CardSets",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.SetNull);
                });

            migrationBuilder.CreateTable(
                name: "CardSetTranslations",
                schema: "preprod",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    CardSetId = table.Column<int>(type: "integer", nullable: false),
                    Culture = table.Column<string>(type: "character varying(10)", maxLength: 10, nullable: false),
                    Name = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CardSetTranslations", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CardSetTranslations_CardSets_CardSetId",
                        column: x => x.CardSetId,
                        principalSchema: "preprod",
                        principalTable: "CardSets",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PokemonAbilityTranslations",
                schema: "preprod",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    PokemonAbilityId = table.Column<int>(type: "integer", nullable: false),
                    Culture = table.Column<string>(type: "character varying(10)", maxLength: 10, nullable: false),
                    Name = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PokemonAbilityTranslations", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PokemonAbilityTranslations_PokemonAbilities_PokemonAbilityId",
                        column: x => x.PokemonAbilityId,
                        principalSchema: "preprod",
                        principalTable: "PokemonAbilities",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PokemonTypeTranslations",
                schema: "preprod",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    PokemonTypeId = table.Column<int>(type: "integer", nullable: false),
                    Culture = table.Column<string>(type: "character varying(10)", maxLength: 10, nullable: false),
                    Name = table.Column<string>(type: "character varying(30)", maxLength: 30, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PokemonTypeTranslations", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PokemonTypeTranslations_PokemonTypes_PokemonTypeId",
                        column: x => x.PokemonTypeId,
                        principalSchema: "preprod",
                        principalTable: "PokemonTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CardFossils",
                schema: "preprod",
                columns: table => new
                {
                    CardId = table.Column<int>(type: "integer", nullable: false),
                    Hp = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CardFossils", x => x.CardId);
                    table.ForeignKey(
                        name: "FK_CardFossils_Cards_CardId",
                        column: x => x.CardId,
                        principalSchema: "preprod",
                        principalTable: "Cards",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CardItems",
                schema: "preprod",
                columns: table => new
                {
                    CardId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CardItems", x => x.CardId);
                    table.ForeignKey(
                        name: "FK_CardItems_Cards_CardId",
                        column: x => x.CardId,
                        principalSchema: "preprod",
                        principalTable: "Cards",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CardPokemons",
                schema: "preprod",
                columns: table => new
                {
                    CardId = table.Column<int>(type: "integer", nullable: false),
                    Specials = table.Column<int>(type: "integer", nullable: false),
                    StageId = table.Column<int>(type: "integer", nullable: false),
                    Hp = table.Column<int>(type: "integer", nullable: false),
                    TypeId = table.Column<int>(type: "integer", nullable: false),
                    WeaknessTypeId = table.Column<int>(type: "integer", nullable: true),
                    RetreatCost = table.Column<int>(type: "integer", nullable: false),
                    PokemonAbilityId = table.Column<int>(type: "integer", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CardPokemons", x => x.CardId);
                    table.ForeignKey(
                        name: "FK_CardPokemons_Cards_CardId",
                        column: x => x.CardId,
                        principalSchema: "preprod",
                        principalTable: "Cards",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CardPokemons_PokemonAbilities_PokemonAbilityId",
                        column: x => x.PokemonAbilityId,
                        principalSchema: "preprod",
                        principalTable: "PokemonAbilities",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.SetNull);
                    table.ForeignKey(
                        name: "FK_CardPokemons_PokemonStages_StageId",
                        column: x => x.StageId,
                        principalSchema: "preprod",
                        principalTable: "PokemonStages",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_CardPokemons_PokemonTypes_TypeId",
                        column: x => x.TypeId,
                        principalSchema: "preprod",
                        principalTable: "PokemonTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_CardPokemons_PokemonTypes_WeaknessTypeId",
                        column: x => x.WeaknessTypeId,
                        principalSchema: "preprod",
                        principalTable: "PokemonTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.SetNull);
                });

            migrationBuilder.CreateTable(
                name: "CardSupporters",
                schema: "preprod",
                columns: table => new
                {
                    CardId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CardSupporters", x => x.CardId);
                    table.ForeignKey(
                        name: "FK_CardSupporters_Cards_CardId",
                        column: x => x.CardId,
                        principalSchema: "preprod",
                        principalTable: "Cards",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CardTools",
                schema: "preprod",
                columns: table => new
                {
                    CardId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CardTools", x => x.CardId);
                    table.ForeignKey(
                        name: "FK_CardTools_Cards_CardId",
                        column: x => x.CardId,
                        principalSchema: "preprod",
                        principalTable: "Cards",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CardTranslations",
                schema: "preprod",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    CardId = table.Column<int>(type: "integer", nullable: false),
                    Culture = table.Column<string>(type: "character varying(10)", maxLength: 10, nullable: false),
                    Name = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    Description = table.Column<string>(type: "character varying(4000)", maxLength: 4000, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CardTranslations", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CardTranslations_Cards_CardId",
                        column: x => x.CardId,
                        principalSchema: "preprod",
                        principalTable: "Cards",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PokemonAttacks",
                schema: "preprod",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Damage = table.Column<int>(type: "integer", nullable: false),
                    Name = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    Description = table.Column<string>(type: "character varying(4000)", maxLength: 4000, nullable: true),
                    CardPokemonCardId = table.Column<int>(type: "integer", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PokemonAttacks", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PokemonAttacks_CardPokemons_CardPokemonCardId",
                        column: x => x.CardPokemonCardId,
                        principalSchema: "preprod",
                        principalTable: "CardPokemons",
                        principalColumn: "CardId");
                });

            migrationBuilder.CreateTable(
                name: "PokemonAttackCosts",
                schema: "preprod",
                columns: table => new
                {
                    PokemonAttackId = table.Column<int>(type: "integer", nullable: false),
                    PokemonTypeId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PokemonAttackCosts", x => new { x.PokemonAttackId, x.PokemonTypeId });
                    table.ForeignKey(
                        name: "FK_PokemonAttackCosts_PokemonAttacks_PokemonAttackId",
                        column: x => x.PokemonAttackId,
                        principalSchema: "preprod",
                        principalTable: "PokemonAttacks",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PokemonAttackCosts_PokemonTypes_PokemonTypeId",
                        column: x => x.PokemonTypeId,
                        principalSchema: "preprod",
                        principalTable: "PokemonTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PokemonAttackTranslations",
                schema: "preprod",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    PokemonAttackId = table.Column<int>(type: "integer", nullable: false),
                    Culture = table.Column<string>(type: "character varying(10)", maxLength: 10, nullable: false),
                    Name = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    Description = table.Column<string>(type: "character varying(4000)", maxLength: 4000, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PokemonAttackTranslations", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PokemonAttackTranslations_PokemonAttacks_PokemonAttackId",
                        column: x => x.PokemonAttackId,
                        principalSchema: "preprod",
                        principalTable: "PokemonAttacks",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CardPokemons_PokemonAbilityId",
                schema: "preprod",
                table: "CardPokemons",
                column: "PokemonAbilityId");

            migrationBuilder.CreateIndex(
                name: "IX_CardPokemons_StageId",
                schema: "preprod",
                table: "CardPokemons",
                column: "StageId");

            migrationBuilder.CreateIndex(
                name: "IX_CardPokemons_TypeId",
                schema: "preprod",
                table: "CardPokemons",
                column: "TypeId");

            migrationBuilder.CreateIndex(
                name: "IX_CardPokemons_WeaknessTypeId",
                schema: "preprod",
                table: "CardPokemons",
                column: "WeaknessTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_Cards_CardRarityId",
                schema: "preprod",
                table: "Cards",
                column: "CardRarityId");

            migrationBuilder.CreateIndex(
                name: "IX_Cards_CardSetId",
                schema: "preprod",
                table: "Cards",
                column: "CardSetId");

            migrationBuilder.CreateIndex(
                name: "IX_CardSetTranslations_CardSetId",
                schema: "preprod",
                table: "CardSetTranslations",
                column: "CardSetId");

            migrationBuilder.CreateIndex(
                name: "IX_CardTranslations_CardId",
                schema: "preprod",
                table: "CardTranslations",
                column: "CardId");

            migrationBuilder.CreateIndex(
                name: "IX_PokemonAbilityTranslations_PokemonAbilityId",
                schema: "preprod",
                table: "PokemonAbilityTranslations",
                column: "PokemonAbilityId");

            migrationBuilder.CreateIndex(
                name: "IX_PokemonAttackCosts_PokemonTypeId",
                schema: "preprod",
                table: "PokemonAttackCosts",
                column: "PokemonTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_PokemonAttacks_CardPokemonCardId",
                schema: "preprod",
                table: "PokemonAttacks",
                column: "CardPokemonCardId");

            migrationBuilder.CreateIndex(
                name: "IX_PokemonAttackTranslations_PokemonAttackId",
                schema: "preprod",
                table: "PokemonAttackTranslations",
                column: "PokemonAttackId");

            migrationBuilder.CreateIndex(
                name: "IX_PokemonTypeTranslations_PokemonTypeId",
                schema: "preprod",
                table: "PokemonTypeTranslations",
                column: "PokemonTypeId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CardFossils",
                schema: "preprod");

            migrationBuilder.DropTable(
                name: "CardItems",
                schema: "preprod");

            migrationBuilder.DropTable(
                name: "CardSetTranslations",
                schema: "preprod");

            migrationBuilder.DropTable(
                name: "CardSupporters",
                schema: "preprod");

            migrationBuilder.DropTable(
                name: "CardTools",
                schema: "preprod");

            migrationBuilder.DropTable(
                name: "CardTranslations",
                schema: "preprod");

            migrationBuilder.DropTable(
                name: "PokemonAbilityTranslations",
                schema: "preprod");

            migrationBuilder.DropTable(
                name: "PokemonAttackCosts",
                schema: "preprod");

            migrationBuilder.DropTable(
                name: "PokemonAttackTranslations",
                schema: "preprod");

            migrationBuilder.DropTable(
                name: "PokemonTypeTranslations",
                schema: "preprod");

            migrationBuilder.DropTable(
                name: "PokemonAttacks",
                schema: "preprod");

            migrationBuilder.DropTable(
                name: "CardPokemons",
                schema: "preprod");

            migrationBuilder.DropTable(
                name: "Cards",
                schema: "preprod");

            migrationBuilder.DropTable(
                name: "PokemonAbilities",
                schema: "preprod");

            migrationBuilder.DropTable(
                name: "PokemonStages",
                schema: "preprod");

            migrationBuilder.DropTable(
                name: "PokemonTypes",
                schema: "preprod");

            migrationBuilder.DropTable(
                name: "CardRarities",
                schema: "preprod");

            migrationBuilder.DropTable(
                name: "CardSets",
                schema: "preprod");
        }
    }
}

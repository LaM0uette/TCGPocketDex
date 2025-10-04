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
                name: "data");

            migrationBuilder.EnsureSchema(
                name: "translation");

            migrationBuilder.EnsureSchema(
                name: "ref");

            migrationBuilder.CreateTable(
                name: "CardCollection",
                schema: "data",
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
                    table.PrimaryKey("PK_CardCollection", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "CardRarity",
                schema: "ref",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CardRarity", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "CardSpecial",
                schema: "ref",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CardSpecial", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "CardType",
                schema: "ref",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CardType", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "PokemonAbility",
                schema: "data",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PokemonAbility", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "PokemonSpecial",
                schema: "ref",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "character varying(30)", maxLength: 30, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PokemonSpecial", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "PokemonStage",
                schema: "ref",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "character varying(30)", maxLength: 30, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PokemonStage", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "PokemonType",
                schema: "ref",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "character varying(30)", maxLength: 30, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PokemonType", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "CardCollectionTranslation",
                schema: "translation",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    CardCollectionId = table.Column<int>(type: "integer", nullable: false),
                    Culture = table.Column<string>(type: "character varying(10)", maxLength: 10, nullable: false),
                    Name = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CardCollectionTranslation", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CardCollectionTranslation_CardCollection_CardCollectionId",
                        column: x => x.CardCollectionId,
                        principalSchema: "data",
                        principalTable: "CardCollection",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Card",
                schema: "data",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    CardTypeId = table.Column<int>(type: "integer", nullable: false),
                    Name = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    Description = table.Column<string>(type: "character varying(4000)", maxLength: 4000, nullable: false),
                    CardRarityId = table.Column<int>(type: "integer", nullable: false),
                    CardCollectionId = table.Column<int>(type: "integer", nullable: false),
                    CollectionNumber = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Card", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Card_CardCollection_CardCollectionId",
                        column: x => x.CardCollectionId,
                        principalSchema: "data",
                        principalTable: "CardCollection",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.SetNull);
                    table.ForeignKey(
                        name: "FK_Card_CardRarity_CardRarityId",
                        column: x => x.CardRarityId,
                        principalSchema: "ref",
                        principalTable: "CardRarity",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Card_CardType_CardTypeId",
                        column: x => x.CardTypeId,
                        principalSchema: "ref",
                        principalTable: "CardType",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "CardTypeTranslation",
                schema: "translation",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    CardTypeId = table.Column<int>(type: "integer", nullable: false),
                    Culture = table.Column<string>(type: "character varying(10)", maxLength: 10, nullable: false),
                    Name = table.Column<string>(type: "character varying(30)", maxLength: 30, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CardTypeTranslation", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CardTypeTranslation_CardType_CardTypeId",
                        column: x => x.CardTypeId,
                        principalSchema: "ref",
                        principalTable: "CardType",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PokemonAbilityTranslation",
                schema: "translation",
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
                    table.PrimaryKey("PK_PokemonAbilityTranslation", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PokemonAbilityTranslation_PokemonAbility_PokemonAbilityId",
                        column: x => x.PokemonAbilityId,
                        principalSchema: "data",
                        principalTable: "PokemonAbility",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PokemonSpecialTranslation",
                schema: "translation",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    PokemonSpecialId = table.Column<int>(type: "integer", nullable: false),
                    Culture = table.Column<string>(type: "character varying(10)", maxLength: 10, nullable: false),
                    Name = table.Column<string>(type: "character varying(30)", maxLength: 30, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PokemonSpecialTranslation", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PokemonSpecialTranslation_PokemonSpecial_PokemonSpecialId",
                        column: x => x.PokemonSpecialId,
                        principalSchema: "ref",
                        principalTable: "PokemonSpecial",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PokemonStageTranslation",
                schema: "translation",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    PokemonStageId = table.Column<int>(type: "integer", nullable: false),
                    Culture = table.Column<string>(type: "character varying(10)", maxLength: 10, nullable: false),
                    Name = table.Column<string>(type: "character varying(30)", maxLength: 30, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PokemonStageTranslation", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PokemonStageTranslation_PokemonStage_PokemonStageId",
                        column: x => x.PokemonStageId,
                        principalSchema: "ref",
                        principalTable: "PokemonStage",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PokemonTypeTranslation",
                schema: "translation",
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
                    table.PrimaryKey("PK_PokemonTypeTranslation", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PokemonTypeTranslation_PokemonType_PokemonTypeId",
                        column: x => x.PokemonTypeId,
                        principalSchema: "ref",
                        principalTable: "PokemonType",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CardCardSpecials",
                schema: "data",
                columns: table => new
                {
                    CardId = table.Column<int>(type: "integer", nullable: false),
                    CardSpecialId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CardCardSpecials", x => new { x.CardId, x.CardSpecialId });
                    table.ForeignKey(
                        name: "FK_CardCardSpecials_CardSpecial_CardSpecialId",
                        column: x => x.CardSpecialId,
                        principalSchema: "ref",
                        principalTable: "CardSpecial",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CardCardSpecials_Card_CardId",
                        column: x => x.CardId,
                        principalSchema: "data",
                        principalTable: "Card",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CardFossil",
                schema: "data",
                columns: table => new
                {
                    CardId = table.Column<int>(type: "integer", nullable: false),
                    Hp = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CardFossil", x => x.CardId);
                    table.ForeignKey(
                        name: "FK_CardFossil_Card_CardId",
                        column: x => x.CardId,
                        principalSchema: "data",
                        principalTable: "Card",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CardItem",
                schema: "data",
                columns: table => new
                {
                    CardId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CardItem", x => x.CardId);
                    table.ForeignKey(
                        name: "FK_CardItem_Card_CardId",
                        column: x => x.CardId,
                        principalSchema: "data",
                        principalTable: "Card",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CardPokemon",
                schema: "data",
                columns: table => new
                {
                    CardId = table.Column<int>(type: "integer", nullable: false),
                    PokemonStageId = table.Column<int>(type: "integer", nullable: false),
                    Hp = table.Column<int>(type: "integer", nullable: false),
                    PokemonTypeId = table.Column<int>(type: "integer", nullable: false),
                    WeaknessPokemonTypeId = table.Column<int>(type: "integer", nullable: true),
                    RetreatCost = table.Column<int>(type: "integer", nullable: false),
                    PokemonAbilityId = table.Column<int>(type: "integer", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CardPokemon", x => x.CardId);
                    table.ForeignKey(
                        name: "FK_CardPokemon_Card_CardId",
                        column: x => x.CardId,
                        principalSchema: "data",
                        principalTable: "Card",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CardPokemon_PokemonAbility_PokemonAbilityId",
                        column: x => x.PokemonAbilityId,
                        principalSchema: "data",
                        principalTable: "PokemonAbility",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.SetNull);
                    table.ForeignKey(
                        name: "FK_CardPokemon_PokemonStage_PokemonStageId",
                        column: x => x.PokemonStageId,
                        principalSchema: "ref",
                        principalTable: "PokemonStage",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_CardPokemon_PokemonType_PokemonTypeId",
                        column: x => x.PokemonTypeId,
                        principalSchema: "ref",
                        principalTable: "PokemonType",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_CardPokemon_PokemonType_WeaknessPokemonTypeId",
                        column: x => x.WeaknessPokemonTypeId,
                        principalSchema: "ref",
                        principalTable: "PokemonType",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.SetNull);
                });

            migrationBuilder.CreateTable(
                name: "CardSupporter",
                schema: "data",
                columns: table => new
                {
                    CardId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CardSupporter", x => x.CardId);
                    table.ForeignKey(
                        name: "FK_CardSupporter_Card_CardId",
                        column: x => x.CardId,
                        principalSchema: "data",
                        principalTable: "Card",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CardTool",
                schema: "data",
                columns: table => new
                {
                    CardId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CardTool", x => x.CardId);
                    table.ForeignKey(
                        name: "FK_CardTool_Card_CardId",
                        column: x => x.CardId,
                        principalSchema: "data",
                        principalTable: "Card",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CardTranslation",
                schema: "translation",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    CardId = table.Column<int>(type: "integer", nullable: false),
                    Culture = table.Column<string>(type: "character varying(10)", maxLength: 10, nullable: false),
                    Name = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    Description = table.Column<string>(type: "character varying(4000)", maxLength: 4000, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CardTranslation", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CardTranslation_Card_CardId",
                        column: x => x.CardId,
                        principalSchema: "data",
                        principalTable: "Card",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CardPokemonPokemonSpecials",
                schema: "data",
                columns: table => new
                {
                    CardId = table.Column<int>(type: "integer", nullable: false),
                    PokemonSpecialId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CardPokemonPokemonSpecials", x => new { x.CardId, x.PokemonSpecialId });
                    table.ForeignKey(
                        name: "FK_CardPokemonPokemonSpecials_CardPokemon_CardId",
                        column: x => x.CardId,
                        principalSchema: "data",
                        principalTable: "CardPokemon",
                        principalColumn: "CardId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CardPokemonPokemonSpecials_PokemonSpecial_PokemonSpecialId",
                        column: x => x.PokemonSpecialId,
                        principalSchema: "ref",
                        principalTable: "PokemonSpecial",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PokemonAttack",
                schema: "data",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    Description = table.Column<string>(type: "character varying(4000)", maxLength: 4000, nullable: false),
                    Damage = table.Column<int>(type: "integer", nullable: false),
                    CardPokemonCardId = table.Column<int>(type: "integer", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PokemonAttack", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PokemonAttack_CardPokemon_CardPokemonCardId",
                        column: x => x.CardPokemonCardId,
                        principalSchema: "data",
                        principalTable: "CardPokemon",
                        principalColumn: "CardId");
                });

            migrationBuilder.CreateTable(
                name: "PokemonAttackCosts",
                schema: "data",
                columns: table => new
                {
                    PokemonAttackId = table.Column<int>(type: "integer", nullable: false),
                    PokemonTypeId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PokemonAttackCosts", x => new { x.PokemonAttackId, x.PokemonTypeId });
                    table.ForeignKey(
                        name: "FK_PokemonAttackCosts_PokemonAttack_PokemonAttackId",
                        column: x => x.PokemonAttackId,
                        principalSchema: "data",
                        principalTable: "PokemonAttack",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PokemonAttackCosts_PokemonType_PokemonTypeId",
                        column: x => x.PokemonTypeId,
                        principalSchema: "ref",
                        principalTable: "PokemonType",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PokemonAttackTranslation",
                schema: "translation",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    PokemonAttackId = table.Column<int>(type: "integer", nullable: false),
                    Culture = table.Column<string>(type: "character varying(10)", maxLength: 10, nullable: false),
                    Name = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    Description = table.Column<string>(type: "character varying(4000)", maxLength: 4000, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PokemonAttackTranslation", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PokemonAttackTranslation_PokemonAttack_PokemonAttackId",
                        column: x => x.PokemonAttackId,
                        principalSchema: "data",
                        principalTable: "PokemonAttack",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Card_CardCollectionId",
                schema: "data",
                table: "Card",
                column: "CardCollectionId");

            migrationBuilder.CreateIndex(
                name: "IX_Card_CardRarityId",
                schema: "data",
                table: "Card",
                column: "CardRarityId");

            migrationBuilder.CreateIndex(
                name: "IX_Card_CardTypeId",
                schema: "data",
                table: "Card",
                column: "CardTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_CardCardSpecials_CardSpecialId",
                schema: "data",
                table: "CardCardSpecials",
                column: "CardSpecialId");

            migrationBuilder.CreateIndex(
                name: "IX_CardCollectionTranslation_CardCollectionId",
                schema: "translation",
                table: "CardCollectionTranslation",
                column: "CardCollectionId");

            migrationBuilder.CreateIndex(
                name: "IX_CardPokemon_PokemonAbilityId",
                schema: "data",
                table: "CardPokemon",
                column: "PokemonAbilityId");

            migrationBuilder.CreateIndex(
                name: "IX_CardPokemon_PokemonStageId",
                schema: "data",
                table: "CardPokemon",
                column: "PokemonStageId");

            migrationBuilder.CreateIndex(
                name: "IX_CardPokemon_PokemonTypeId",
                schema: "data",
                table: "CardPokemon",
                column: "PokemonTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_CardPokemon_WeaknessPokemonTypeId",
                schema: "data",
                table: "CardPokemon",
                column: "WeaknessPokemonTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_CardPokemonPokemonSpecials_PokemonSpecialId",
                schema: "data",
                table: "CardPokemonPokemonSpecials",
                column: "PokemonSpecialId");

            migrationBuilder.CreateIndex(
                name: "IX_CardSpecial_Name",
                schema: "ref",
                table: "CardSpecial",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_CardTranslation_CardId",
                schema: "translation",
                table: "CardTranslation",
                column: "CardId");

            migrationBuilder.CreateIndex(
                name: "IX_CardType_Name",
                schema: "ref",
                table: "CardType",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_CardTypeTranslation_CardTypeId",
                schema: "translation",
                table: "CardTypeTranslation",
                column: "CardTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_PokemonAbilityTranslation_PokemonAbilityId",
                schema: "translation",
                table: "PokemonAbilityTranslation",
                column: "PokemonAbilityId");

            migrationBuilder.CreateIndex(
                name: "IX_PokemonAttack_CardPokemonCardId",
                schema: "data",
                table: "PokemonAttack",
                column: "CardPokemonCardId");

            migrationBuilder.CreateIndex(
                name: "IX_PokemonAttackCosts_PokemonTypeId",
                schema: "data",
                table: "PokemonAttackCosts",
                column: "PokemonTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_PokemonAttackTranslation_PokemonAttackId",
                schema: "translation",
                table: "PokemonAttackTranslation",
                column: "PokemonAttackId");

            migrationBuilder.CreateIndex(
                name: "IX_PokemonSpecial_Name",
                schema: "ref",
                table: "PokemonSpecial",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_PokemonSpecialTranslation_PokemonSpecialId",
                schema: "translation",
                table: "PokemonSpecialTranslation",
                column: "PokemonSpecialId");

            migrationBuilder.CreateIndex(
                name: "IX_PokemonStageTranslation_PokemonStageId",
                schema: "translation",
                table: "PokemonStageTranslation",
                column: "PokemonStageId");

            migrationBuilder.CreateIndex(
                name: "IX_PokemonTypeTranslation_PokemonTypeId",
                schema: "translation",
                table: "PokemonTypeTranslation",
                column: "PokemonTypeId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CardCardSpecials",
                schema: "data");

            migrationBuilder.DropTable(
                name: "CardCollectionTranslation",
                schema: "translation");

            migrationBuilder.DropTable(
                name: "CardFossil",
                schema: "data");

            migrationBuilder.DropTable(
                name: "CardItem",
                schema: "data");

            migrationBuilder.DropTable(
                name: "CardPokemonPokemonSpecials",
                schema: "data");

            migrationBuilder.DropTable(
                name: "CardSupporter",
                schema: "data");

            migrationBuilder.DropTable(
                name: "CardTool",
                schema: "data");

            migrationBuilder.DropTable(
                name: "CardTranslation",
                schema: "translation");

            migrationBuilder.DropTable(
                name: "CardTypeTranslation",
                schema: "translation");

            migrationBuilder.DropTable(
                name: "PokemonAbilityTranslation",
                schema: "translation");

            migrationBuilder.DropTable(
                name: "PokemonAttackCosts",
                schema: "data");

            migrationBuilder.DropTable(
                name: "PokemonAttackTranslation",
                schema: "translation");

            migrationBuilder.DropTable(
                name: "PokemonSpecialTranslation",
                schema: "translation");

            migrationBuilder.DropTable(
                name: "PokemonStageTranslation",
                schema: "translation");

            migrationBuilder.DropTable(
                name: "PokemonTypeTranslation",
                schema: "translation");

            migrationBuilder.DropTable(
                name: "CardSpecial",
                schema: "ref");

            migrationBuilder.DropTable(
                name: "PokemonAttack",
                schema: "data");

            migrationBuilder.DropTable(
                name: "PokemonSpecial",
                schema: "ref");

            migrationBuilder.DropTable(
                name: "CardPokemon",
                schema: "data");

            migrationBuilder.DropTable(
                name: "Card",
                schema: "data");

            migrationBuilder.DropTable(
                name: "PokemonAbility",
                schema: "data");

            migrationBuilder.DropTable(
                name: "PokemonStage",
                schema: "ref");

            migrationBuilder.DropTable(
                name: "PokemonType",
                schema: "ref");

            migrationBuilder.DropTable(
                name: "CardCollection",
                schema: "data");

            migrationBuilder.DropTable(
                name: "CardRarity",
                schema: "ref");

            migrationBuilder.DropTable(
                name: "CardType",
                schema: "ref");
        }
    }
}

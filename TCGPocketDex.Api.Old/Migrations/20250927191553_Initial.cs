using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace TCGPocketDex.Api.Old.Migrations
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
                name: "CardExtensions",
                schema: "preprod",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Code = table.Column<string>(type: "character varying(10)", maxLength: 10, nullable: false),
                    Series = table.Column<string>(type: "character varying(10)", maxLength: 10, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CardExtensions", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "CardRarities",
                schema: "preprod",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    ImageUrl = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CardRarities", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "PokemonAbilities",
                schema: "preprod",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PokemonAbilities", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "PokemonAttacks",
                schema: "preprod",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Damage = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PokemonAttacks", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "PokemonStages",
                schema: "preprod",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn)
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
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PokemonTypes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "PromoSeries",
                schema: "preprod",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Code = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PromoSeries", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Boosters",
                schema: "preprod",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    CardExtensionId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Boosters", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Boosters_CardExtensions_CardExtensionId",
                        column: x => x.CardExtensionId,
                        principalSchema: "preprod",
                        principalTable: "CardExtensions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CardExtensionTranslations",
                schema: "preprod",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    CardExtensionId = table.Column<int>(type: "integer", nullable: false),
                    Culture = table.Column<string>(type: "character varying(10)", maxLength: 10, nullable: false),
                    Name = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CardExtensionTranslations", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CardExtensionTranslations_CardExtensions_CardExtensionId",
                        column: x => x.CardExtensionId,
                        principalSchema: "preprod",
                        principalTable: "CardExtensions",
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
                name: "PokemonAttackTranslations",
                schema: "preprod",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    PokemonAttackId = table.Column<int>(type: "integer", nullable: false),
                    Culture = table.Column<string>(type: "character varying(10)", maxLength: 10, nullable: false),
                    Name = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    Description = table.Column<string>(type: "character varying(2000)", maxLength: 2000, nullable: true)
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

            migrationBuilder.CreateTable(
                name: "PokemonStageTranslations",
                schema: "preprod",
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
                    table.PrimaryKey("PK_PokemonStageTranslations", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PokemonStageTranslations_PokemonStages_PokemonStageId",
                        column: x => x.PokemonStageId,
                        principalSchema: "preprod",
                        principalTable: "PokemonStages",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PokemonAttackCosts",
                schema: "preprod",
                columns: table => new
                {
                    AttacksUsingTypeId = table.Column<int>(type: "integer", nullable: false),
                    CostsId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PokemonAttackCosts", x => new { x.AttacksUsingTypeId, x.CostsId });
                    table.ForeignKey(
                        name: "FK_PokemonAttackCosts_PokemonAttacks_AttacksUsingTypeId",
                        column: x => x.AttacksUsingTypeId,
                        principalSchema: "preprod",
                        principalTable: "PokemonAttacks",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PokemonAttackCosts_PokemonTypes_CostsId",
                        column: x => x.CostsId,
                        principalSchema: "preprod",
                        principalTable: "PokemonTypes",
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
                    Name = table.Column<string>(type: "character varying(30)", maxLength: 30, nullable: false),
                    ImageUrl = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false)
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
                name: "PromoSeriesTranslations",
                schema: "preprod",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    PromoSeriesId = table.Column<int>(type: "integer", nullable: false),
                    Culture = table.Column<string>(type: "character varying(10)", maxLength: 10, nullable: false),
                    Name = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PromoSeriesTranslations", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PromoSeriesTranslations_PromoSeries_PromoSeriesId",
                        column: x => x.PromoSeriesId,
                        principalSchema: "preprod",
                        principalTable: "PromoSeries",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "BoosterTranslations",
                schema: "preprod",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    BoosterId = table.Column<int>(type: "integer", nullable: false),
                    Culture = table.Column<string>(type: "character varying(10)", maxLength: 10, nullable: false),
                    Name = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BoosterTranslations", x => x.Id);
                    table.ForeignKey(
                        name: "FK_BoosterTranslations_Boosters_BoosterId",
                        column: x => x.BoosterId,
                        principalSchema: "preprod",
                        principalTable: "Boosters",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Cards",
                schema: "preprod",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    CardRarityId = table.Column<int>(type: "integer", nullable: false),
                    BoosterId = table.Column<int>(type: "integer", nullable: true),
                    PromoSeriesId = table.Column<int>(type: "integer", nullable: true),
                    CardExtensionId = table.Column<int>(type: "integer", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Cards", x => x.Id);
                    table.CheckConstraint("CK_Cards_Source", "(\"BoosterId\" IS NOT NULL AND \"PromoSeriesId\" IS NULL AND \"CardExtensionId\" IS NOT NULL) OR (\"BoosterId\" IS NULL AND \"PromoSeriesId\" IS NOT NULL AND \"CardExtensionId\" IS NULL)");
                    table.ForeignKey(
                        name: "FK_Cards_Boosters_BoosterId",
                        column: x => x.BoosterId,
                        principalSchema: "preprod",
                        principalTable: "Boosters",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.SetNull);
                    table.ForeignKey(
                        name: "FK_Cards_CardExtensions_CardExtensionId",
                        column: x => x.CardExtensionId,
                        principalSchema: "preprod",
                        principalTable: "CardExtensions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.SetNull);
                    table.ForeignKey(
                        name: "FK_Cards_CardRarities_CardRarityId",
                        column: x => x.CardRarityId,
                        principalSchema: "preprod",
                        principalTable: "CardRarities",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Cards_PromoSeries_PromoSeriesId",
                        column: x => x.PromoSeriesId,
                        principalSchema: "preprod",
                        principalTable: "PromoSeries",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.SetNull);
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
                    Description = table.Column<string>(type: "character varying(2000)", maxLength: 2000, nullable: true),
                    ImageUrl = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false)
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
                name: "FossilCards",
                schema: "preprod",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false),
                    Hp = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FossilCards", x => x.Id);
                    table.ForeignKey(
                        name: "FK_FossilCards_Cards_Id",
                        column: x => x.Id,
                        principalSchema: "preprod",
                        principalTable: "Cards",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ItemCards",
                schema: "preprod",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ItemCards", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ItemCards_Cards_Id",
                        column: x => x.Id,
                        principalSchema: "preprod",
                        principalTable: "Cards",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PokemonCards",
                schema: "preprod",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false),
                    IsEx = table.Column<bool>(type: "boolean", nullable: false),
                    IsMega = table.Column<bool>(type: "boolean", nullable: false),
                    StageId = table.Column<int>(type: "integer", nullable: false),
                    Hp = table.Column<int>(type: "integer", nullable: false),
                    TypeId = table.Column<int>(type: "integer", nullable: false),
                    RetreatCost = table.Column<int>(type: "integer", nullable: false),
                    PokemonAbilityId = table.Column<int>(type: "integer", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PokemonCards", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PokemonCards_Cards_Id",
                        column: x => x.Id,
                        principalSchema: "preprod",
                        principalTable: "Cards",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PokemonCards_PokemonAbilities_PokemonAbilityId",
                        column: x => x.PokemonAbilityId,
                        principalSchema: "preprod",
                        principalTable: "PokemonAbilities",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.SetNull);
                    table.ForeignKey(
                        name: "FK_PokemonCards_PokemonStages_StageId",
                        column: x => x.StageId,
                        principalSchema: "preprod",
                        principalTable: "PokemonStages",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PokemonCards_PokemonTypes_TypeId",
                        column: x => x.TypeId,
                        principalSchema: "preprod",
                        principalTable: "PokemonTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PokemonToolCards",
                schema: "preprod",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PokemonToolCards", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PokemonToolCards_Cards_Id",
                        column: x => x.Id,
                        principalSchema: "preprod",
                        principalTable: "Cards",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "SupporterCards",
                schema: "preprod",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SupporterCards", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SupporterCards_Cards_Id",
                        column: x => x.Id,
                        principalSchema: "preprod",
                        principalTable: "Cards",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PokemonAttackPokemonCard",
                schema: "preprod",
                columns: table => new
                {
                    AttacksId = table.Column<int>(type: "integer", nullable: false),
                    PokemonCardId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PokemonAttackPokemonCard", x => new { x.AttacksId, x.PokemonCardId });
                    table.ForeignKey(
                        name: "FK_PokemonAttackPokemonCard_PokemonAttacks_AttacksId",
                        column: x => x.AttacksId,
                        principalSchema: "preprod",
                        principalTable: "PokemonAttacks",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PokemonAttackPokemonCard_PokemonCards_PokemonCardId",
                        column: x => x.PokemonCardId,
                        principalSchema: "preprod",
                        principalTable: "PokemonCards",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PokemonWeaknesses",
                schema: "preprod",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    PokemonCardId = table.Column<int>(type: "integer", nullable: false),
                    TypeId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PokemonWeaknesses", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PokemonWeaknesses_PokemonCards_PokemonCardId",
                        column: x => x.PokemonCardId,
                        principalSchema: "preprod",
                        principalTable: "PokemonCards",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PokemonWeaknesses_PokemonTypes_TypeId",
                        column: x => x.TypeId,
                        principalSchema: "preprod",
                        principalTable: "PokemonTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Boosters_CardExtensionId",
                schema: "preprod",
                table: "Boosters",
                column: "CardExtensionId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_BoosterTranslations_BoosterId_Culture",
                schema: "preprod",
                table: "BoosterTranslations",
                columns: new[] { "BoosterId", "Culture" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_CardExtensions_Series_Code",
                schema: "preprod",
                table: "CardExtensions",
                columns: new[] { "Series", "Code" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_CardExtensionTranslations_CardExtensionId_Culture",
                schema: "preprod",
                table: "CardExtensionTranslations",
                columns: new[] { "CardExtensionId", "Culture" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Cards_BoosterId",
                schema: "preprod",
                table: "Cards",
                column: "BoosterId");

            migrationBuilder.CreateIndex(
                name: "IX_Cards_CardExtensionId",
                schema: "preprod",
                table: "Cards",
                column: "CardExtensionId");

            migrationBuilder.CreateIndex(
                name: "IX_Cards_CardRarityId",
                schema: "preprod",
                table: "Cards",
                column: "CardRarityId");

            migrationBuilder.CreateIndex(
                name: "IX_Cards_PromoSeriesId",
                schema: "preprod",
                table: "Cards",
                column: "PromoSeriesId");

            migrationBuilder.CreateIndex(
                name: "IX_CardTranslations_CardId_Culture",
                schema: "preprod",
                table: "CardTranslations",
                columns: new[] { "CardId", "Culture" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_PokemonAbilityTranslations_PokemonAbilityId_Culture",
                schema: "preprod",
                table: "PokemonAbilityTranslations",
                columns: new[] { "PokemonAbilityId", "Culture" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_PokemonAttackCosts_CostsId",
                schema: "preprod",
                table: "PokemonAttackCosts",
                column: "CostsId");

            migrationBuilder.CreateIndex(
                name: "IX_PokemonAttackPokemonCard_PokemonCardId",
                schema: "preprod",
                table: "PokemonAttackPokemonCard",
                column: "PokemonCardId");

            migrationBuilder.CreateIndex(
                name: "IX_PokemonAttackTranslations_PokemonAttackId_Culture",
                schema: "preprod",
                table: "PokemonAttackTranslations",
                columns: new[] { "PokemonAttackId", "Culture" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_PokemonCards_PokemonAbilityId",
                schema: "preprod",
                table: "PokemonCards",
                column: "PokemonAbilityId");

            migrationBuilder.CreateIndex(
                name: "IX_PokemonCards_StageId",
                schema: "preprod",
                table: "PokemonCards",
                column: "StageId");

            migrationBuilder.CreateIndex(
                name: "IX_PokemonCards_TypeId",
                schema: "preprod",
                table: "PokemonCards",
                column: "TypeId");

            migrationBuilder.CreateIndex(
                name: "IX_PokemonStageTranslations_PokemonStageId_Culture",
                schema: "preprod",
                table: "PokemonStageTranslations",
                columns: new[] { "PokemonStageId", "Culture" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_PokemonTypeTranslations_PokemonTypeId_Culture",
                schema: "preprod",
                table: "PokemonTypeTranslations",
                columns: new[] { "PokemonTypeId", "Culture" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_PokemonWeaknesses_PokemonCardId",
                schema: "preprod",
                table: "PokemonWeaknesses",
                column: "PokemonCardId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_PokemonWeaknesses_TypeId",
                schema: "preprod",
                table: "PokemonWeaknesses",
                column: "TypeId");

            migrationBuilder.CreateIndex(
                name: "IX_PromoSeries_Code",
                schema: "preprod",
                table: "PromoSeries",
                column: "Code",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_PromoSeriesTranslations_PromoSeriesId_Culture",
                schema: "preprod",
                table: "PromoSeriesTranslations",
                columns: new[] { "PromoSeriesId", "Culture" },
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BoosterTranslations",
                schema: "preprod");

            migrationBuilder.DropTable(
                name: "CardExtensionTranslations",
                schema: "preprod");

            migrationBuilder.DropTable(
                name: "CardTranslations",
                schema: "preprod");

            migrationBuilder.DropTable(
                name: "FossilCards",
                schema: "preprod");

            migrationBuilder.DropTable(
                name: "ItemCards",
                schema: "preprod");

            migrationBuilder.DropTable(
                name: "PokemonAbilityTranslations",
                schema: "preprod");

            migrationBuilder.DropTable(
                name: "PokemonAttackCosts",
                schema: "preprod");

            migrationBuilder.DropTable(
                name: "PokemonAttackPokemonCard",
                schema: "preprod");

            migrationBuilder.DropTable(
                name: "PokemonAttackTranslations",
                schema: "preprod");

            migrationBuilder.DropTable(
                name: "PokemonStageTranslations",
                schema: "preprod");

            migrationBuilder.DropTable(
                name: "PokemonToolCards",
                schema: "preprod");

            migrationBuilder.DropTable(
                name: "PokemonTypeTranslations",
                schema: "preprod");

            migrationBuilder.DropTable(
                name: "PokemonWeaknesses",
                schema: "preprod");

            migrationBuilder.DropTable(
                name: "PromoSeriesTranslations",
                schema: "preprod");

            migrationBuilder.DropTable(
                name: "SupporterCards",
                schema: "preprod");

            migrationBuilder.DropTable(
                name: "PokemonAttacks",
                schema: "preprod");

            migrationBuilder.DropTable(
                name: "PokemonCards",
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
                name: "Boosters",
                schema: "preprod");

            migrationBuilder.DropTable(
                name: "CardRarities",
                schema: "preprod");

            migrationBuilder.DropTable(
                name: "PromoSeries",
                schema: "preprod");

            migrationBuilder.DropTable(
                name: "CardExtensions",
                schema: "preprod");
        }
    }
}

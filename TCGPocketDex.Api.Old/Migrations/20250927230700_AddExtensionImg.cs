using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TCGPocketDex.Api.Old.Migrations
{
    /// <inheritdoc />
    public partial class AddExtensionImg : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ImageUrl",
                schema: "preprod",
                table: "CardExtensionTranslations",
                type: "character varying(300)",
                maxLength: 300,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ImageUrl",
                schema: "preprod",
                table: "BoosterTranslations",
                type: "character varying(300)",
                maxLength: 300,
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ImageUrl",
                schema: "preprod",
                table: "CardExtensionTranslations");

            migrationBuilder.DropColumn(
                name: "ImageUrl",
                schema: "preprod",
                table: "BoosterTranslations");
        }
    }
}

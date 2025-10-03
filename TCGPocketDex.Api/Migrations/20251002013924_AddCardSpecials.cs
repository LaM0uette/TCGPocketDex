using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TCGPocketDex.Api.Migrations
{
    /// <inheritdoc />
    public partial class AddCardSpecials : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsPromo",
                schema: "preprod",
                table: "Cards");

            migrationBuilder.AddColumn<int>(
                name: "Specials",
                schema: "preprod",
                table: "Cards",
                type: "integer",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Specials",
                schema: "preprod",
                table: "Cards");

            migrationBuilder.AddColumn<bool>(
                name: "IsPromo",
                schema: "preprod",
                table: "Cards",
                type: "boolean",
                nullable: false,
                defaultValue: false);
        }
    }
}

using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TCGPocketDex.Api.Old.Migrations
{
    /// <inheritdoc />
    public partial class AddCollectionNumberCard : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ExtensionCardNumber",
                schema: "preprod",
                table: "Cards",
                type: "integer",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ExtensionCardNumber",
                schema: "preprod",
                table: "Cards");
        }
    }
}

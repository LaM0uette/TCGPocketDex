using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TCGPocketDex.Api.Old.Migrations
{
    public partial class UpdateBoosterIndex : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Boosters_CardExtensionId",
                schema: "preprod",
                table: "Boosters");

            migrationBuilder.CreateIndex(
                name: "IX_Boosters_CardExtensionId",
                schema: "preprod",
                table: "Boosters",
                column: "CardExtensionId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Boosters_CardExtensionId",
                schema: "preprod",
                table: "Boosters");

            migrationBuilder.CreateIndex(
                name: "IX_Boosters_CardExtensionId",
                schema: "preprod",
                table: "Boosters",
                column: "CardExtensionId",
                unique: true);
        }
    }
}

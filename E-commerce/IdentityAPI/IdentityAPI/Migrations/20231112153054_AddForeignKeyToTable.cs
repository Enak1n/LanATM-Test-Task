using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace IdentityAPI.Migrations
{
    /// <inheritdoc />
    public partial class AddForeignKeyToTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddForeignKey(
            name: "FK_Users_Addresses_AddressId",
            table: "Users",
            column: "AddressId",
            principalTable: "Addresses",
            principalColumn: "Id",
            onDelete: ReferentialAction.Cascade);

            migrationBuilder.CreateIndex(
            name: "IX_Users_AddressId",
            table: "Users",
            column: "AddressId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Users_Addresses_AddressId",
                table: "Users");

            migrationBuilder.DropIndex(
                name: "IX_Users_AddressId",
                table: "Users");
        }
    }
}

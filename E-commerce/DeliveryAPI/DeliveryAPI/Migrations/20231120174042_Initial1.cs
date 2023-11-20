using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DeliveryAPI.Migrations
{
    /// <inheritdoc />
    public partial class Initial1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Couriers_Deliveries_DeliveryId",
                table: "Couriers");

            migrationBuilder.DropIndex(
                name: "IX_Couriers_DeliveryId",
                table: "Couriers");

            migrationBuilder.AddColumn<Guid>(
                name: "CourierId",
                table: "Deliveries",
                type: "uuid",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Deliveries_CourierId",
                table: "Deliveries",
                column: "CourierId");

            migrationBuilder.AddForeignKey(
                name: "FK_Deliveries_Couriers_CourierId",
                table: "Deliveries",
                column: "CourierId",
                principalTable: "Couriers",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Deliveries_Couriers_CourierId",
                table: "Deliveries");

            migrationBuilder.DropIndex(
                name: "IX_Deliveries_CourierId",
                table: "Deliveries");

            migrationBuilder.DropColumn(
                name: "CourierId",
                table: "Deliveries");

            migrationBuilder.CreateIndex(
                name: "IX_Couriers_DeliveryId",
                table: "Couriers",
                column: "DeliveryId");

            migrationBuilder.AddForeignKey(
                name: "FK_Couriers_Deliveries_DeliveryId",
                table: "Couriers",
                column: "DeliveryId",
                principalTable: "Deliveries",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}

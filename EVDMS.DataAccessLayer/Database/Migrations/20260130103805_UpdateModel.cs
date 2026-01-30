using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EVDMS.DataAccessLayer.Database.Migrations
{
    /// <inheritdoc />
    public partial class UpdateModel : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Config_Vehicles_VehicleId",
                table: "Config");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Config",
                table: "Config");

            migrationBuilder.RenameTable(
                name: "Config",
                newName: "Configs");

            migrationBuilder.RenameIndex(
                name: "IX_Config_VehicleId",
                table: "Configs",
                newName: "IX_Configs_VehicleId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Configs",
                table: "Configs",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Configs_Vehicles_VehicleId",
                table: "Configs",
                column: "VehicleId",
                principalTable: "Vehicles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Configs_Vehicles_VehicleId",
                table: "Configs");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Configs",
                table: "Configs");

            migrationBuilder.RenameTable(
                name: "Configs",
                newName: "Config");

            migrationBuilder.RenameIndex(
                name: "IX_Configs_VehicleId",
                table: "Config",
                newName: "IX_Config_VehicleId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Config",
                table: "Config",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Config_Vehicles_VehicleId",
                table: "Config",
                column: "VehicleId",
                principalTable: "Vehicles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}

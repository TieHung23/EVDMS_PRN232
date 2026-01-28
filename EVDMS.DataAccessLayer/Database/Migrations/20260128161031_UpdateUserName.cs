using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EVDMS.DataAccessLayer.Database.Migrations
{
    /// <inheritdoc />
    public partial class UpdateUserName : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("00000064-0001-0000-0000-000000000000"),
                column: "UserName",
                value: "TieHung23");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("00000064-0001-0000-0000-000000000000"),
                column: "UserName",
                value: "");
        }
    }
}

using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EVDMS.DataAccessLayer.Database.Migrations
{
    /// <inheritdoc />
    public partial class changepassword : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("00000064-0001-0000-0000-000000000000"),
                column: "HashedPassword",
                value: "$2b$10$81Kb12PBFTcp6Y4lxasV1uyC/8gOhRJk7CgPBH.wjrsSUKcDhpDue");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("00000064-0001-0000-0000-000000000000"),
                column: "HashedPassword",
                value: "$2a$11$0uhjzQiyqR.PruEMpOERluaHHR2w96U8rE1ZKES9ecyACHNPnEqqm");
        }
    }
}

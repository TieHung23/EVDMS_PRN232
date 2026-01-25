using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace EVDMS.DataAccessLayer.Database.Migrations
{
    /// <inheritdoc />
    public partial class Initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Dealers",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Code = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedAtTick = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ModifiedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifiedAtTick = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ModifiedBy = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Dealers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Role",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Role", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Vehicles",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ModelName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Brand = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    VehicleType = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ReleaseYear = table.Column<int>(type: "int", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedAtTick = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ModifiedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifiedAtTick = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ModifiedBy = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Vehicles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UserName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    HashedPassword = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FullName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    RoleId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    DealerId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedAtTick = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ModifiedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifiedAtTick = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ModifiedBy = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Users_Dealers_DealerId",
                        column: x => x.DealerId,
                        principalTable: "Dealers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Users_Role_RoleId",
                        column: x => x.RoleId,
                        principalTable: "Role",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Config",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    VehicleId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedAtTick = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ModifiedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifiedAtTick = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ModifiedBy = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Config", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Config_Vehicles_VehicleId",
                        column: x => x.VehicleId,
                        principalTable: "Vehicles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Inventories",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    DealerId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    VehicleId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Quantity = table.Column<int>(type: "int", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedAtTick = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ModifiedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifiedAtTick = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ModifiedBy = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Inventories", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Inventories_Dealers_DealerId",
                        column: x => x.DealerId,
                        principalTable: "Dealers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Inventories_Vehicles_VehicleId",
                        column: x => x.VehicleId,
                        principalTable: "Vehicles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Dealers",
                columns: new[] { "Id", "Code", "CreatedAt", "CreatedAtTick", "CreatedBy", "Email", "IsActive", "IsDeleted", "ModifiedAt", "ModifiedAtTick", "ModifiedBy", "Name" },
                values: new object[,]
                {
                    { new Guid("0000012c-0001-0000-0000-000000000000"), "DL-000", new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "638396640000000000", "", "hungptse183180@fpt.edu.vn", true, false, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "638396640000000000", "", "Default Dealer" },
                    { new Guid("0000012c-0002-0000-0000-000000000000"), "DL-001", new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "638396640000000000", "", "hanoi.dealer@evdms.com", true, false, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "638396640000000000", "", "Hanoi Central Dealer" },
                    { new Guid("0000012c-0003-0000-0000-000000000000"), "DL-002", new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "638396640000000000", "", "hcm.dealer@evdms.com", true, false, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "638396640000000000", "", "Ho Chi Minh City Dealer" },
                    { new Guid("0000012c-0004-0000-0000-000000000000"), "DL-003", new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "638396640000000000", "", "danang.dealer@evdms.com", true, false, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "638396640000000000", "", "Da Nang Dealer" },
                    { new Guid("0000012c-0005-0000-0000-000000000000"), "DL-004", new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "638396640000000000", "", "cantho.dealer@evdms.com", true, false, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "638396640000000000", "", "Can Tho Dealer" }
                });

            migrationBuilder.InsertData(
                table: "Role",
                columns: new[] { "Id", "Description", "Name" },
                values: new object[,]
                {
                    { new Guid("00000064-0001-0000-0000-000000000000"), "Staff member of a dealership.", "Dealer Staff" },
                    { new Guid("00000064-0002-0000-0000-000000000000"), "Manager of a dealership.", "Dealer Manager" },
                    { new Guid("00000064-0003-0000-0000-000000000000"), "Electric Vehicle Manufacturer staff.", "EVM Staff" },
                    { new Guid("00000064-0004-0000-0000-000000000000"), "System Administrator.", "Admin" }
                });

            migrationBuilder.InsertData(
                table: "Vehicles",
                columns: new[] { "Id", "Brand", "CreatedAt", "CreatedAtTick", "CreatedBy", "Description", "IsActive", "IsDeleted", "ModelName", "ModifiedAt", "ModifiedAtTick", "ModifiedBy", "ReleaseYear", "VehicleType" },
                values: new object[,]
                {
                    { new Guid("00000064-0001-0000-0000-000000000000"), "Tesla", new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "638396640000000000", "System User", "An all-electric premium sedan produced by Tesla.", true, false, "Model S", new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "638396640000000000", "System User", 2020, "Sedan" },
                    { new Guid("00000064-0002-0000-0000-000000000000"), "Tesla", new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "638396640000000000", "System User", "A compact electric sedan designed for mass adoption.", true, false, "Model 3", new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "638396640000000000", "System User", 2019, "Sedan" },
                    { new Guid("00000064-0003-0000-0000-000000000000"), "Tesla", new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "638396640000000000", "System User", "A luxury electric SUV featuring falcon-wing doors.", true, false, "Model X", new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "638396640000000000", "System User", 2021, "SUV" },
                    { new Guid("00000064-0004-0000-0000-000000000000"), "Tesla", new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "638396640000000000", "System User", "A versatile electric SUV designed for families.", true, false, "Model Y", new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "638396640000000000", "System User", 2022, "SUV" },
                    { new Guid("00000064-0005-0000-0000-000000000000"), "Honda", new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "638396640000000000", "System User", "A reliable compact sedan with fuel-efficient performance.", true, false, "Civic", new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "638396640000000000", "System User", 2018, "Sedan" },
                    { new Guid("00000064-0006-0000-0000-000000000000"), "Honda", new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "638396640000000000", "System User", "A mid-size sedan offering comfort and advanced safety.", true, false, "Accord", new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "638396640000000000", "System User", 2019, "Sedan" },
                    { new Guid("00000064-0007-0000-0000-000000000000"), "Toyota", new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "638396640000000000", "System User", "A popular sedan known for durability and reliability.", true, false, "Camry", new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "638396640000000000", "System User", 2017, "Sedan" },
                    { new Guid("00000064-0008-0000-0000-000000000000"), "Toyota", new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "638396640000000000", "System User", "A compact sedan with excellent fuel economy.", true, false, "Corolla", new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "638396640000000000", "System User", 2016, "Sedan" },
                    { new Guid("00000064-0009-0000-0000-000000000000"), "Ford", new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "638396640000000000", "System User", "An iconic sports car delivering powerful performance.", true, false, "Mustang", new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "638396640000000000", "System User", 2021, "Coupe" },
                    { new Guid("00000064-000a-0000-0000-000000000000"), "Ford", new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "638396640000000000", "System User", "A rugged pickup truck built for work and adventure.", true, false, "Ranger", new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "638396640000000000", "System User", 2022, "Pickup" }
                });

            migrationBuilder.InsertData(
                table: "Config",
                columns: new[] { "Id", "CreatedAt", "CreatedAtTick", "CreatedBy", "Description", "IsActive", "IsDeleted", "ModifiedAt", "ModifiedAtTick", "ModifiedBy", "Name", "VehicleId" },
                values: new object[,]
                {
                    { new Guid("000000c8-0001-0000-0000-000000000000"), new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "638396640000000000", "System User", "Allows configuration of exterior color options.", true, false, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "638396640000000000", "System User", "Color", new Guid("00000064-0001-0000-0000-000000000000") },
                    { new Guid("000000c8-0002-0000-0000-000000000000"), new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "638396640000000000", "System User", "Manual or automatic transmission options.", true, false, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "638396640000000000", "System User", "Transmission", new Guid("00000064-0001-0000-0000-000000000000") },
                    { new Guid("000000c8-0003-0000-0000-000000000000"), new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "638396640000000000", "System User", "Interior material options such as leather or fabric.", true, false, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "638396640000000000", "System User", "Interior", new Guid("00000064-0001-0000-0000-000000000000") },
                    { new Guid("000000c8-0004-0000-0000-000000000000"), new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "638396640000000000", "System User", "Selectable wheel sizes for better performance or comfort.", true, false, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "638396640000000000", "System User", "WheelSize", new Guid("00000064-0001-0000-0000-000000000000") },
                    { new Guid("000000c8-0005-0000-0000-000000000000"), new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "638396640000000000", "System User", "Multiple exterior paint options available.", true, false, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "638396640000000000", "System User", "Color", new Guid("00000064-0002-0000-0000-000000000000") },
                    { new Guid("000000c8-0006-0000-0000-000000000000"), new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "638396640000000000", "System User", "Different battery capacities affecting driving range.", true, false, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "638396640000000000", "System User", "BatteryPack", new Guid("00000064-0002-0000-0000-000000000000") },
                    { new Guid("000000c8-0007-0000-0000-000000000000"), new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "638396640000000000", "System User", "Advanced driver assistance and autopilot features.", true, false, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "638396640000000000", "System User", "Autopilot", new Guid("00000064-0002-0000-0000-000000000000") },
                    { new Guid("000000c8-0008-0000-0000-000000000000"), new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "638396640000000000", "System User", "Standard or premium audio system options.", true, false, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "638396640000000000", "System User", "SoundSystem", new Guid("00000064-0002-0000-0000-000000000000") },
                    { new Guid("000000c8-0009-0000-0000-000000000000"), new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "638396640000000000", "System User", "Selectable driving modes: Eco, Normal, Sport.", true, false, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "638396640000000000", "System User", "DriveMode", new Guid("00000064-0003-0000-0000-000000000000") },
                    { new Guid("000000c8-000a-0000-0000-000000000000"), new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "638396640000000000", "System User", "Standard or adaptive suspension configuration.", true, false, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "638396640000000000", "System User", "Suspension", new Guid("00000064-0003-0000-0000-000000000000") },
                    { new Guid("000000c8-000b-0000-0000-000000000000"), new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "638396640000000000", "System User", "Flexible seating layout for passengers and cargo.", true, false, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "638396640000000000", "System User", "SeatLayout", new Guid("00000064-0003-0000-0000-000000000000") },
                    { new Guid("000000c8-000c-0000-0000-000000000000"), new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "638396640000000000", "System User", "Enhanced safety features and collision avoidance.", true, false, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "638396640000000000", "System User", "SafetyPackage", new Guid("00000064-0003-0000-0000-000000000000") },
                    { new Guid("000000c8-000d-0000-0000-000000000000"), new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "638396640000000000", "System User", "Additional towing capabilities and accessories.", true, false, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "638396640000000000", "System User", "TowingPackage", new Guid("00000064-0004-0000-0000-000000000000") },
                    { new Guid("000000c8-000e-0000-0000-000000000000"), new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "638396640000000000", "System User", "Panoramic glass roof or standard roof options.", true, false, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "638396640000000000", "System User", "RoofType", new Guid("00000064-0004-0000-0000-000000000000") },
                    { new Guid("000000c8-000f-0000-0000-000000000000"), new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "638396640000000000", "System User", "Standard or adaptive LED lighting system.", true, false, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "638396640000000000", "System User", "Lighting", new Guid("00000064-0004-0000-0000-000000000000") },
                    { new Guid("000000c8-0010-0000-0000-000000000000"), new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "638396640000000000", "System User", "Dual-zone or tri-zone climate control system.", true, false, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "638396640000000000", "System User", "ClimateControl", new Guid("00000064-0004-0000-0000-000000000000") },
                    { new Guid("000000c8-0011-0000-0000-000000000000"), new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "638396640000000000", "System User", "Gasoline or hybrid engine configurations.", true, false, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "638396640000000000", "System User", "EngineType", new Guid("00000064-0005-0000-0000-000000000000") },
                    { new Guid("000000c8-0012-0000-0000-000000000000"), new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "638396640000000000", "System User", "Optimized fuel consumption modes.", true, false, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "638396640000000000", "System User", "FuelEfficiency", new Guid("00000064-0005-0000-0000-000000000000") },
                    { new Guid("000000c8-0013-0000-0000-000000000000"), new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "638396640000000000", "System User", "Touchscreen infotainment system with navigation.", true, false, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "638396640000000000", "System User", "Infotainment", new Guid("00000064-0005-0000-0000-000000000000") },
                    { new Guid("000000c8-0014-0000-0000-000000000000"), new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "638396640000000000", "System User", "Extended warranty and service plans.", true, false, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "638396640000000000", "System User", "Warranty", new Guid("00000064-0005-0000-0000-000000000000") }
                });

            migrationBuilder.InsertData(
                table: "Inventories",
                columns: new[] { "Id", "CreatedAt", "CreatedAtTick", "CreatedBy", "DealerId", "IsActive", "IsDeleted", "ModifiedAt", "ModifiedAtTick", "ModifiedBy", "Quantity", "VehicleId" },
                values: new object[,]
                {
                    { new Guid("00000190-0001-0000-0000-000000000000"), new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "638396640000000000", "System User", new Guid("0000012c-0001-0000-0000-000000000000"), true, false, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "638396640000000000", "System User", 10, new Guid("00000064-0001-0000-0000-000000000000") },
                    { new Guid("00000190-0002-0000-0000-000000000000"), new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "638396640000000000", "System User", new Guid("0000012c-0001-0000-0000-000000000000"), true, false, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "638396640000000000", "System User", 8, new Guid("00000064-0002-0000-0000-000000000000") },
                    { new Guid("00000190-0003-0000-0000-000000000000"), new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "638396640000000000", "System User", new Guid("0000012c-0002-0000-0000-000000000000"), true, false, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "638396640000000000", "System User", 12, new Guid("00000064-0003-0000-0000-000000000000") },
                    { new Guid("00000190-0004-0000-0000-000000000000"), new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "638396640000000000", "System User", new Guid("0000012c-0002-0000-0000-000000000000"), true, false, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "638396640000000000", "System User", 6, new Guid("00000064-0004-0000-0000-000000000000") },
                    { new Guid("00000190-0005-0000-0000-000000000000"), new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "638396640000000000", "System User", new Guid("0000012c-0003-0000-0000-000000000000"), true, false, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "638396640000000000", "System User", 15, new Guid("00000064-0005-0000-0000-000000000000") },
                    { new Guid("00000190-0006-0000-0000-000000000000"), new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "638396640000000000", "System User", new Guid("0000012c-0003-0000-0000-000000000000"), true, false, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "638396640000000000", "System User", 9, new Guid("00000064-0006-0000-0000-000000000000") },
                    { new Guid("00000190-0007-0000-0000-000000000000"), new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "638396640000000000", "System User", new Guid("0000012c-0004-0000-0000-000000000000"), true, false, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "638396640000000000", "System User", 7, new Guid("00000064-0007-0000-0000-000000000000") },
                    { new Guid("00000190-0008-0000-0000-000000000000"), new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "638396640000000000", "System User", new Guid("0000012c-0004-0000-0000-000000000000"), true, false, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "638396640000000000", "System User", 11, new Guid("00000064-0008-0000-0000-000000000000") },
                    { new Guid("00000190-0009-0000-0000-000000000000"), new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "638396640000000000", "System User", new Guid("0000012c-0005-0000-0000-000000000000"), true, false, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "638396640000000000", "System User", 5, new Guid("00000064-0009-0000-0000-000000000000") },
                    { new Guid("00000190-000a-0000-0000-000000000000"), new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "638396640000000000", "System User", new Guid("0000012c-0005-0000-0000-000000000000"), true, false, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "638396640000000000", "System User", 14, new Guid("00000064-000a-0000-0000-000000000000") }
                });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "CreatedAt", "CreatedAtTick", "CreatedBy", "DealerId", "FullName", "HashedPassword", "IsActive", "IsDeleted", "ModifiedAt", "ModifiedAtTick", "ModifiedBy", "RoleId", "UserName" },
                values: new object[] { new Guid("00000064-0001-0000-0000-000000000000"), new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "638396640000000000", "", new Guid("0000012c-0001-0000-0000-000000000000"), "System Admin", "$2a$11$0uhjzQiyqR.PruEMpOERluaHHR2w96U8rE1ZKES9ecyACHNPnEqqm", true, false, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "638396640000000000", "", new Guid("00000064-0004-0000-0000-000000000000"), "" });

            migrationBuilder.CreateIndex(
                name: "IX_Config_VehicleId",
                table: "Config",
                column: "VehicleId");

            migrationBuilder.CreateIndex(
                name: "IX_Inventories_DealerId",
                table: "Inventories",
                column: "DealerId");

            migrationBuilder.CreateIndex(
                name: "IX_Inventories_VehicleId",
                table: "Inventories",
                column: "VehicleId");

            migrationBuilder.CreateIndex(
                name: "IX_Users_DealerId",
                table: "Users",
                column: "DealerId");

            migrationBuilder.CreateIndex(
                name: "IX_Users_RoleId",
                table: "Users",
                column: "RoleId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Config");

            migrationBuilder.DropTable(
                name: "Inventories");

            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropTable(
                name: "Vehicles");

            migrationBuilder.DropTable(
                name: "Dealers");

            migrationBuilder.DropTable(
                name: "Role");
        }
    }
}

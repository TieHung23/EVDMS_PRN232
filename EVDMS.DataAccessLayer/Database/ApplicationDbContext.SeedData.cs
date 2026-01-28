using EVDMS.Core.Model;
using Microsoft.EntityFrameworkCore;

namespace EVDMS.DataAccessLayer.Database;

public partial class ApplicationDbContext
{
    private const string SystemUserName = "System User";
    private readonly DateTime SystemDate = new DateTime(2024, 1, 1, 0, 0, 0, DateTimeKind.Utc);
    private string SystemDateTicks => SystemDate.Ticks.ToString();
    private readonly Guid SystemUserId = NewGuid(100, 1);

    private void SeedRoles(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Role>().HasData(
            new Role { Id = NewGuid(100, 1), Name = "Dealer Staff", Description = "Staff member of a dealership."},
            new Role { Id = NewGuid(100, 2), Name = "Dealer Manager", Description = "Manager of a dealership."},
            new Role { Id = NewGuid(100, 3), Name = "EVM Staff", Description = "Electric Vehicle Manufacturer staff."},
            new Role { Id = NewGuid(100, 4), Name = "Admin", Description = "System Administrator."}
        );
    }

    private void SeedDealers(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Dealer>().HasData(
            new Dealer
            {
                Id = NewGuid(300, 1),
                Name = "Default Dealer",
                Code = "DL-000",
                Email = "hungptse183180@fpt.edu.vn",
                CreatedAt = SystemDate,
                CreatedAtTick = SystemDateTicks,
                CreatedBy = "",
                ModifiedAt = SystemDate,
                ModifiedAtTick = SystemDateTicks,
                ModifiedBy = "",
                IsActive = true,
                IsDeleted = false,
            },
            new Dealer
            {
                Id = NewGuid(300, 2),
                Name = "Hanoi Central Dealer",
                Code = "DL-001",
                Email = "hanoi.dealer@evdms.com",
                CreatedAt = SystemDate,
                CreatedAtTick = SystemDateTicks,
                CreatedBy = "",
                ModifiedAt = SystemDate,
                ModifiedAtTick = SystemDateTicks,
                ModifiedBy = "",
                IsActive = true,
                IsDeleted = false,
            },
            new Dealer
            {
                Id = NewGuid(300, 3),
                Name = "Ho Chi Minh City Dealer",
                Code = "DL-002",
                Email = "hcm.dealer@evdms.com",
                CreatedAt = SystemDate,
                CreatedAtTick = SystemDateTicks,
                CreatedBy = "",
                ModifiedAt = SystemDate,
                ModifiedAtTick = SystemDateTicks,
                ModifiedBy = "",
                IsActive = true,
                IsDeleted = false,
            },
            new Dealer
            {
                Id = NewGuid(300, 4),
                Name = "Da Nang Dealer",
                Code = "DL-003",
                Email = "danang.dealer@evdms.com",
                CreatedAt = SystemDate,
                CreatedAtTick = SystemDateTicks,
                CreatedBy = "",
                ModifiedAt = SystemDate,
                ModifiedAtTick = SystemDateTicks,
                ModifiedBy = "",
                IsActive = true,
                IsDeleted = false,
            },
            new Dealer
            {
                Id = NewGuid(300, 5),
                Name = "Can Tho Dealer",
                Code = "DL-004",
                Email = "cantho.dealer@evdms.com",
                CreatedAt = SystemDate,
                CreatedAtTick = SystemDateTicks,
                CreatedBy = "",
                ModifiedAt = SystemDate,
                ModifiedAtTick = SystemDateTicks,
                ModifiedBy = "",
                IsActive = true,
                IsDeleted = false,
            }
        );
    }


    private static Guid NewGuid(int entityType, int id)
    {
        byte[] bytes = new byte[16];
        BitConverter.GetBytes(entityType).CopyTo(bytes, 0);
        BitConverter.GetBytes(id).CopyTo(bytes, 4);
        return new Guid(bytes);
    }

    private void SeedInitialAccounts(ModelBuilder modelBuilder)
    {
        var defaultHashedPassword = "$2a$11$0uhjzQiyqR.PruEMpOERluaHHR2w96U8rE1ZKES9ecyACHNPnEqqm";

        modelBuilder.Entity<User>().HasData(
            new User
            {
                Id = SystemUserId,
                HashedPassword = defaultHashedPassword,
                FullName = "System Admin",
                UserName = "TieHung23",
                IsActive = true,
                IsDeleted = false,
                DealerId = NewGuid(300, 1),
                RoleId = NewGuid(100, 4),
                ModifiedAt = SystemDate,
                ModifiedAtTick = SystemDateTicks,
                ModifiedBy = "",
                CreatedAt = SystemDate,
                CreatedAtTick = SystemDateTicks,
                CreatedBy = "",
            }
        );
    }

    private void SeedConfigs(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Config>().HasData(
            // Vehicle 1
            new Config
            {
                Id = NewGuid(200, 1),
                Name = "Color",
                Description = "Allows configuration of exterior color options.",
                CreatedAt = SystemDate,
                CreatedAtTick = SystemDateTicks,
                CreatedBy = SystemUserName,
                ModifiedAt = SystemDate,
                ModifiedAtTick = SystemDateTicks,
                ModifiedBy = SystemUserName,
                IsActive = true,
                IsDeleted = false,
                VehicleId = NewGuid(100, 1)
            },
            new Config
            {
                Id = NewGuid(200, 2),
                Name = "Transmission",
                Description = "Manual or automatic transmission options.",
                CreatedAt = SystemDate,
                CreatedAtTick = SystemDateTicks,
                CreatedBy = SystemUserName,
                ModifiedAt = SystemDate,
                ModifiedAtTick = SystemDateTicks,
                ModifiedBy = SystemUserName,
                IsActive = true,
                IsDeleted = false,
                VehicleId = NewGuid(100, 1)
            },
            new Config
            {
                Id = NewGuid(200, 3),
                Name = "Interior",
                Description = "Interior material options such as leather or fabric.",
                CreatedAt = SystemDate,
                CreatedAtTick = SystemDateTicks,
                CreatedBy = SystemUserName,
                ModifiedAt = SystemDate,
                ModifiedAtTick = SystemDateTicks,
                ModifiedBy = SystemUserName,
                IsActive = true,
                IsDeleted = false,
                VehicleId = NewGuid(100, 1)
            },
            new Config
            {
                Id = NewGuid(200, 4),
                Name = "WheelSize",
                Description = "Selectable wheel sizes for better performance or comfort.",
                CreatedAt = SystemDate,
                CreatedAtTick = SystemDateTicks,
                CreatedBy = SystemUserName,
                ModifiedAt = SystemDate,
                ModifiedAtTick = SystemDateTicks,
                ModifiedBy = SystemUserName,
                IsActive = true,
                IsDeleted = false,
                VehicleId = NewGuid(100, 1)
            },

            // Vehicle 2
            new Config
            {
                Id = NewGuid(200, 5),
                Name = "Color",
                Description = "Multiple exterior paint options available.",
                CreatedAt = SystemDate,
                CreatedAtTick = SystemDateTicks,
                CreatedBy = SystemUserName,
                ModifiedAt = SystemDate,
                ModifiedAtTick = SystemDateTicks,
                ModifiedBy = SystemUserName,
                IsActive = true,
                IsDeleted = false,
                VehicleId = NewGuid(100, 2)
            },
            new Config
            {
                Id = NewGuid(200, 6),
                Name = "BatteryPack",
                Description = "Different battery capacities affecting driving range.",
                CreatedAt = SystemDate,
                CreatedAtTick = SystemDateTicks,
                CreatedBy = SystemUserName,
                ModifiedAt = SystemDate,
                ModifiedAtTick = SystemDateTicks,
                ModifiedBy = SystemUserName,
                IsActive = true,
                IsDeleted = false,
                VehicleId = NewGuid(100, 2)
            },
            new Config
            {
                Id = NewGuid(200, 7),
                Name = "Autopilot",
                Description = "Advanced driver assistance and autopilot features.",
                CreatedAt = SystemDate,
                CreatedAtTick = SystemDateTicks,
                CreatedBy = SystemUserName,
                ModifiedAt = SystemDate,
                ModifiedAtTick = SystemDateTicks,
                ModifiedBy = SystemUserName,
                IsActive = true,
                IsDeleted = false,
                VehicleId = NewGuid(100, 2)
            },
            new Config
            {
                Id = NewGuid(200, 8),
                Name = "SoundSystem",
                Description = "Standard or premium audio system options.",
                CreatedAt = SystemDate,
                CreatedAtTick = SystemDateTicks,
                CreatedBy = SystemUserName,
                ModifiedAt = SystemDate,
                ModifiedAtTick = SystemDateTicks,
                ModifiedBy = SystemUserName,
                IsActive = true,
                IsDeleted = false,
                VehicleId = NewGuid(100, 2)
            },

            // Vehicle 3
            new Config
            {
                Id = NewGuid(200, 9),
                Name = "DriveMode",
                Description = "Selectable driving modes: Eco, Normal, Sport.",
                CreatedAt = SystemDate,
                CreatedAtTick = SystemDateTicks,
                CreatedBy = SystemUserName,
                ModifiedAt = SystemDate,
                ModifiedAtTick = SystemDateTicks,
                ModifiedBy = SystemUserName,
                IsActive = true,
                IsDeleted = false,
                VehicleId = NewGuid(100, 3)
            },
            new Config
            {
                Id = NewGuid(200, 10),
                Name = "Suspension",
                Description = "Standard or adaptive suspension configuration.",
                CreatedAt = SystemDate,
                CreatedAtTick = SystemDateTicks,
                CreatedBy = SystemUserName,
                ModifiedAt = SystemDate,
                ModifiedAtTick = SystemDateTicks,
                ModifiedBy = SystemUserName,
                IsActive = true,
                IsDeleted = false,
                VehicleId = NewGuid(100, 3)
            },
            new Config
            {
                Id = NewGuid(200, 11),
                Name = "SeatLayout",
                Description = "Flexible seating layout for passengers and cargo.",
                CreatedAt = SystemDate,
                CreatedAtTick = SystemDateTicks,
                CreatedBy = SystemUserName,
                ModifiedAt = SystemDate,
                ModifiedAtTick = SystemDateTicks,
                ModifiedBy = SystemUserName,
                IsActive = true,
                IsDeleted = false,
                VehicleId = NewGuid(100, 3)
            },
            new Config
            {
                Id = NewGuid(200, 12),
                Name = "SafetyPackage",
                Description = "Enhanced safety features and collision avoidance.",
                CreatedAt = SystemDate,
                CreatedAtTick = SystemDateTicks,
                CreatedBy = SystemUserName,
                ModifiedAt = SystemDate,
                ModifiedAtTick = SystemDateTicks,
                ModifiedBy = SystemUserName,
                IsActive = true,
                IsDeleted = false,
                VehicleId = NewGuid(100, 3)
            },

            // Vehicle 4
            new Config
            {
                Id = NewGuid(200, 13),
                Name = "TowingPackage",
                Description = "Additional towing capabilities and accessories.",
                CreatedAt = SystemDate,
                CreatedAtTick = SystemDateTicks,
                CreatedBy = SystemUserName,
                ModifiedAt = SystemDate,
                ModifiedAtTick = SystemDateTicks,
                ModifiedBy = SystemUserName,
                IsActive = true,
                IsDeleted = false,
                VehicleId = NewGuid(100, 4)
            },
            new Config
            {
                Id = NewGuid(200, 14),
                Name = "RoofType",
                Description = "Panoramic glass roof or standard roof options.",
                CreatedAt = SystemDate,
                CreatedAtTick = SystemDateTicks,
                CreatedBy = SystemUserName,
                ModifiedAt = SystemDate,
                ModifiedAtTick = SystemDateTicks,
                ModifiedBy = SystemUserName,
                IsActive = true,
                IsDeleted = false,
                VehicleId = NewGuid(100, 4)
            },
            new Config
            {
                Id = NewGuid(200, 15),
                Name = "Lighting",
                Description = "Standard or adaptive LED lighting system.",
                CreatedAt = SystemDate,
                CreatedAtTick = SystemDateTicks,
                CreatedBy = SystemUserName,
                ModifiedAt = SystemDate,
                ModifiedAtTick = SystemDateTicks,
                ModifiedBy = SystemUserName,
                IsActive = true,
                IsDeleted = false,
                VehicleId = NewGuid(100, 4)
            },
            new Config
            {
                Id = NewGuid(200, 16),
                Name = "ClimateControl",
                Description = "Dual-zone or tri-zone climate control system.",
                CreatedAt = SystemDate,
                CreatedAtTick = SystemDateTicks,
                CreatedBy = SystemUserName,
                ModifiedAt = SystemDate,
                ModifiedAtTick = SystemDateTicks,
                ModifiedBy = SystemUserName,
                IsActive = true,
                IsDeleted = false,
                VehicleId = NewGuid(100, 4)
            },

            // Vehicle 5
            new Config
            {
                Id = NewGuid(200, 17),
                Name = "EngineType",
                Description = "Gasoline or hybrid engine configurations.",
                CreatedAt = SystemDate,
                CreatedAtTick = SystemDateTicks,
                CreatedBy = SystemUserName,
                ModifiedAt = SystemDate,
                ModifiedAtTick = SystemDateTicks,
                ModifiedBy = SystemUserName,
                IsActive = true,
                IsDeleted = false,
                VehicleId = NewGuid(100, 5)
            },
            new Config
            {
                Id = NewGuid(200, 18),
                Name = "FuelEfficiency",
                Description = "Optimized fuel consumption modes.",
                CreatedAt = SystemDate,
                CreatedAtTick = SystemDateTicks,
                CreatedBy = SystemUserName,
                ModifiedAt = SystemDate,
                ModifiedAtTick = SystemDateTicks,
                ModifiedBy = SystemUserName,
                IsActive = true,
                IsDeleted = false,
                VehicleId = NewGuid(100, 5)
            },
            new Config
            {
                Id = NewGuid(200, 19),
                Name = "Infotainment",
                Description = "Touchscreen infotainment system with navigation.",
                CreatedAt = SystemDate,
                CreatedAtTick = SystemDateTicks,
                CreatedBy = SystemUserName,
                ModifiedAt = SystemDate,
                ModifiedAtTick = SystemDateTicks,
                ModifiedBy = SystemUserName,
                IsActive = true,
                IsDeleted = false,
                VehicleId = NewGuid(100, 5)
            },
            new Config
            {
                Id = NewGuid(200, 20),
                Name = "Warranty",
                Description = "Extended warranty and service plans.",
                CreatedAt = SystemDate,
                CreatedAtTick = SystemDateTicks,
                CreatedBy = SystemUserName,
                ModifiedAt = SystemDate,
                ModifiedAtTick = SystemDateTicks,
                ModifiedBy = SystemUserName,
                IsActive = true,
                IsDeleted = false,
                VehicleId = NewGuid(100, 5)
            }
        );
    }

    private void SeedVehicles(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Vehicle>().HasData(
            new Vehicle
            {
                Id = NewGuid(100, 1),
                ModelName = "Model S",
                Brand = "Tesla",
                VehicleType = "Sedan",
                Description = "An all-electric premium sedan produced by Tesla.",
                ReleaseYear = 2020,
                CreatedAt = SystemDate,
                CreatedAtTick = SystemDateTicks,
                CreatedBy = SystemUserName,
                ModifiedAt = SystemDate,
                ModifiedAtTick = SystemDateTicks,
                ModifiedBy = SystemUserName,
                IsActive = true,
                IsDeleted = false,
            },
            new Vehicle
            {
                Id = NewGuid(100, 2),
                ModelName = "Model 3",
                Brand = "Tesla",
                VehicleType = "Sedan",
                Description = "A compact electric sedan designed for mass adoption.",
                ReleaseYear = 2019,
                CreatedAt = SystemDate,
                CreatedAtTick = SystemDateTicks,
                CreatedBy = SystemUserName,
                ModifiedAt = SystemDate,
                ModifiedAtTick = SystemDateTicks,
                ModifiedBy = SystemUserName,
                IsActive = true,
                IsDeleted = false,
            },
            new Vehicle
            {
                Id = NewGuid(100, 3),
                ModelName = "Model X",
                Brand = "Tesla",
                VehicleType = "SUV",
                Description = "A luxury electric SUV featuring falcon-wing doors.",
                ReleaseYear = 2021,
                CreatedAt = SystemDate,
                CreatedAtTick = SystemDateTicks,
                CreatedBy = SystemUserName,
                ModifiedAt = SystemDate,
                ModifiedAtTick = SystemDateTicks,
                ModifiedBy = SystemUserName,
                IsActive = true,
                IsDeleted = false,
            },
            new Vehicle
            {
                Id = NewGuid(100, 4),
                ModelName = "Model Y",
                Brand = "Tesla",
                VehicleType = "SUV",
                Description = "A versatile electric SUV designed for families.",
                ReleaseYear = 2022,
                CreatedAt = SystemDate,
                CreatedAtTick = SystemDateTicks,
                CreatedBy = SystemUserName,
                ModifiedAt = SystemDate,
                ModifiedAtTick = SystemDateTicks,
                ModifiedBy = SystemUserName,
                IsActive = true,
                IsDeleted = false,
            },
            new Vehicle
            {
                Id = NewGuid(100, 5),
                ModelName = "Civic",
                Brand = "Honda",
                VehicleType = "Sedan",
                Description = "A reliable compact sedan with fuel-efficient performance.",
                ReleaseYear = 2018,
                CreatedAt = SystemDate,
                CreatedAtTick = SystemDateTicks,
                CreatedBy = SystemUserName,
                ModifiedAt = SystemDate,
                ModifiedAtTick = SystemDateTicks,
                ModifiedBy = SystemUserName,
                IsActive = true,
                IsDeleted = false,
            },
            new Vehicle
            {
                Id = NewGuid(100, 6),
                ModelName = "Accord",
                Brand = "Honda",
                VehicleType = "Sedan",
                Description = "A mid-size sedan offering comfort and advanced safety.",
                ReleaseYear = 2019,
                CreatedAt = SystemDate,
                CreatedAtTick = SystemDateTicks,
                CreatedBy = SystemUserName,
                ModifiedAt = SystemDate,
                ModifiedAtTick = SystemDateTicks,
                ModifiedBy = SystemUserName,
                IsActive = true,
                IsDeleted = false,
            },
            new Vehicle
            {
                Id = NewGuid(100, 7),
                ModelName = "Camry",
                Brand = "Toyota",
                VehicleType = "Sedan",
                Description = "A popular sedan known for durability and reliability.",
                ReleaseYear = 2017,
                CreatedAt = SystemDate,
                CreatedAtTick = SystemDateTicks,
                CreatedBy = SystemUserName,
                ModifiedAt = SystemDate,
                ModifiedAtTick = SystemDateTicks,
                ModifiedBy = SystemUserName,
                IsActive = true,
                IsDeleted = false,
            },
            new Vehicle
            {
                Id = NewGuid(100, 8),
                ModelName = "Corolla",
                Brand = "Toyota",
                VehicleType = "Sedan",
                Description = "A compact sedan with excellent fuel economy.",
                ReleaseYear = 2016,
                CreatedAt = SystemDate,
                CreatedAtTick = SystemDateTicks,
                CreatedBy = SystemUserName,
                ModifiedAt = SystemDate,
                ModifiedAtTick = SystemDateTicks,
                ModifiedBy = SystemUserName,
                IsActive = true,
                IsDeleted = false,
            },
            new Vehicle
            {
                Id = NewGuid(100, 9),
                ModelName = "Mustang",
                Brand = "Ford",
                VehicleType = "Coupe",
                Description = "An iconic sports car delivering powerful performance.",
                ReleaseYear = 2021,
                CreatedAt = SystemDate,
                CreatedAtTick = SystemDateTicks,
                CreatedBy = SystemUserName,
                ModifiedAt = SystemDate,
                ModifiedAtTick = SystemDateTicks,
                ModifiedBy = SystemUserName,
                IsActive = true,
                IsDeleted = false,
            },
            new Vehicle
            {
                Id = NewGuid(100, 10),
                ModelName = "Ranger",
                Brand = "Ford",
                VehicleType = "Pickup",
                Description = "A rugged pickup truck built for work and adventure.",
                ReleaseYear = 2022,
                CreatedAt = SystemDate,
                CreatedAtTick = SystemDateTicks,
                CreatedBy = SystemUserName,
                ModifiedAt = SystemDate,
                ModifiedAtTick = SystemDateTicks,
                ModifiedBy = SystemUserName,
                IsActive = true,
                IsDeleted = false,
            }
);

    }

    private void SeedInventories(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Inventory>().HasData(
            new Inventory
            {
                Id = NewGuid(400, 1),
                DealerId = NewGuid(300, 1),
                VehicleId = NewGuid(100, 1),
                Quantity = 10,
                CreatedAt = SystemDate,
                CreatedAtTick = SystemDateTicks,
                CreatedBy = SystemUserName,
                ModifiedAt = SystemDate,
                ModifiedAtTick = SystemDateTicks,
                ModifiedBy = SystemUserName,
                IsActive = true,
                IsDeleted = false,
            },
            new Inventory
            {
                Id = NewGuid(400, 2),
                DealerId = NewGuid(300, 1),
                VehicleId = NewGuid(100, 2),
                Quantity = 8,
                CreatedAt = SystemDate,
                CreatedAtTick = SystemDateTicks,
                CreatedBy = SystemUserName,
                ModifiedAt = SystemDate,
                ModifiedAtTick = SystemDateTicks,
                ModifiedBy = SystemUserName,
                IsActive = true,
                IsDeleted = false,
            },
            new Inventory
            {
                Id = NewGuid(400, 3),
                DealerId = NewGuid(300, 2),
                VehicleId = NewGuid(100, 3),
                Quantity = 12,
                CreatedAt = SystemDate,
                CreatedAtTick = SystemDateTicks,
                CreatedBy = SystemUserName,
                ModifiedAt = SystemDate,
                ModifiedAtTick = SystemDateTicks,
                ModifiedBy = SystemUserName,
                IsActive = true,
                IsDeleted = false,
            },
            new Inventory
            {
                Id = NewGuid(400, 4),
                DealerId = NewGuid(300, 2),
                VehicleId = NewGuid(100, 4),
                Quantity = 6,
                CreatedAt = SystemDate,
                CreatedAtTick = SystemDateTicks,
                CreatedBy = SystemUserName,
                ModifiedAt = SystemDate,
                ModifiedAtTick = SystemDateTicks,
                ModifiedBy = SystemUserName,
                IsActive = true,
                IsDeleted = false,
            },
            new Inventory
            {
                Id = NewGuid(400, 5),
                DealerId = NewGuid(300, 3),
                VehicleId = NewGuid(100, 5),
                Quantity = 15,
                CreatedAt = SystemDate,
                CreatedAtTick = SystemDateTicks,
                CreatedBy = SystemUserName,
                ModifiedAt = SystemDate,
                ModifiedAtTick = SystemDateTicks,
                ModifiedBy = SystemUserName,
                IsActive = true,
                IsDeleted = false,
            },
            new Inventory
            {
                Id = NewGuid(400, 6),
                DealerId = NewGuid(300, 3),
                VehicleId = NewGuid(100, 6),
                Quantity = 9,
                CreatedAt = SystemDate,
                CreatedAtTick = SystemDateTicks,
                CreatedBy = SystemUserName,
                ModifiedAt = SystemDate,
                ModifiedAtTick = SystemDateTicks,
                ModifiedBy = SystemUserName,
                IsActive = true,
                IsDeleted = false,
            },
            new Inventory
            {
                Id = NewGuid(400, 7),
                DealerId = NewGuid(300, 4),
                VehicleId = NewGuid(100, 7),
                Quantity = 7,
                CreatedAt = SystemDate,
                CreatedAtTick = SystemDateTicks,
                CreatedBy = SystemUserName,
                ModifiedAt = SystemDate,
                ModifiedAtTick = SystemDateTicks,
                ModifiedBy = SystemUserName,
                IsActive = true,
                IsDeleted = false,
            },
            new Inventory
            {
                Id = NewGuid(400, 8),
                DealerId = NewGuid(300, 4),
                VehicleId = NewGuid(100, 8),
                Quantity = 11,
                CreatedAt = SystemDate,
                CreatedAtTick = SystemDateTicks,
                CreatedBy = SystemUserName,
                ModifiedAt = SystemDate,
                ModifiedAtTick = SystemDateTicks,
                ModifiedBy = SystemUserName,
                IsActive = true,
                IsDeleted = false,
            },
            new Inventory
            {
                Id = NewGuid(400, 9),
                DealerId = NewGuid(300, 5),
                VehicleId = NewGuid(100, 9),
                Quantity = 5,
                CreatedAt = SystemDate,
                CreatedAtTick = SystemDateTicks,
                CreatedBy = SystemUserName,
                ModifiedAt = SystemDate,
                ModifiedAtTick = SystemDateTicks,
                ModifiedBy = SystemUserName,
                IsActive = true,
                IsDeleted = false,
            },
            new Inventory
            {
                Id = NewGuid(400, 10),
                DealerId = NewGuid(300, 5),
                VehicleId = NewGuid(100, 10),
                Quantity = 14,
                CreatedAt = SystemDate,
                CreatedAtTick = SystemDateTicks,
                CreatedBy = SystemUserName,
                ModifiedAt = SystemDate,
                ModifiedAtTick = SystemDateTicks,
                ModifiedBy = SystemUserName,
                IsActive = true,
                IsDeleted = false,
            }
        );
    }
}
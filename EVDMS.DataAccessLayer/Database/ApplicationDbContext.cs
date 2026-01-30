using EVDMS.Core.Model;
using Microsoft.EntityFrameworkCore;

namespace EVDMS.DataAccessLayer.Database;

public partial class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        SeedRoles(modelBuilder);
        SeedDealers(modelBuilder);
        SeedVehicles(modelBuilder);
        SeedConfigs(modelBuilder);
        SeedInitialAccounts(modelBuilder);
        SeedInventories(modelBuilder);
    }


    public DbSet<Role> Roles { get; set;  }
    public DbSet<User> Users { get; set;  }
    public DbSet<Dealer> Dealers { get; set;  }
    public DbSet<Vehicle> Vehicles { get; set;  }
    public DbSet<Inventory> Inventories { get; set;  }
    public DbSet<Config> Configs { get; set; }
    public DbSet<Role> Role { get; set; }
}
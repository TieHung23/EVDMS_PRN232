using EVDMS.Core.Model;
using Microsoft.EntityFrameworkCore;

namespace EVDMS.DataAccessLayer.Database;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
    {
    }


    public DbSet<Role> Roles { get; set;  }
    public DbSet<User> Users { get; set;  }
    public DbSet<Dealer> Dealers { get; set;  }
    public DbSet<Vehicle> Vehicles { get; set;  }
    public DbSet<Inventory> Inventories { get; set;  }
    public DbSet<Role> Role { get; set; }
}
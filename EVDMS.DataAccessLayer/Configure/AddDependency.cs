using EVDMS.DataAccessLayer.Database;
using EVDMS.DataAccessLayer.Repository.Abstraction;
using EVDMS.DataAccessLayer.Repository.Implement;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace EVDMS.DataAccessLayer.Configure;

public static class AddDependency
{
    public static IServiceCollection AddDataAccessLayer(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<ApplicationDbContext>(options =>
            options.UseSqlServer(configuration.GetConnectionString("DefaultConnection"),
            b => b.MigrationsAssembly("EVDMS.DataAccessLayer")));


        services.AddScoped(
            typeof(IGenericRepository<,>),
            typeof(GenericRepository<,>)
            );
        services.AddScoped<IUnitOfWork, UnitOfWork>();

        return services;
    }
}
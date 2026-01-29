using EVDMS.BusinessLogicLayer.Configure.Option;
using EVDMS.BusinessLogicLayer.Helper;
using EVDMS.BusinessLogicLayer.Service.Abstraction;
using EVDMS.BusinessLogicLayer.Service.Implement;
using EVDMS.DataAccessLayer.Configure;
using EVDMS.DataAccessLayer.Database;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace EVDMS.BusinessLogicLayer.Configure;

public static class AddDependency
{
    public static IServiceCollection AddBusinessLogicLayer(this IServiceCollection services)
    {
        services.AddScoped<IDealerService, DealerService>();
        services.AddScoped<IVehicleService, VehicleService>();
        // Add Business Logic Layer dependencies here
        services.AddScoped<IRoleService, RoleService>();
        services.AddScoped<IAuthService, AuthService>();


        // Add helpers
        services.AddScoped<IJwtHelper, JwtHelper>();
        return services;
    }

    public static IServiceCollection AddDataAccessLayer_Wrap(this IServiceCollection services, IConfiguration configuration)
    {
        return services.AddDataAccessLayer(configuration);
    }

    public static IServiceScope AddSeedData(this IServiceScope scope)
    {
        var context = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
        context.Database.Migrate();

        return scope;
    }

    public static IServiceCollection AddOptions(this IServiceCollection services, IConfiguration configuration)
    {
        services.Configure<JwtModel>(configuration.GetSection("JwtSettings"));

        return services;
    }
}

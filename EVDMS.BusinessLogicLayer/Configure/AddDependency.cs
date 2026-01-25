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
        // Add Business Logic Layer dependencies here

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
}
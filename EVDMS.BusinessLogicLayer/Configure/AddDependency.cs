using EVDMS.DataAccessLayer.Configure;
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
}
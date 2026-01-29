namespace EVDMS.Api.Configure;

public static class AuthorizationConfigure
{
    public static IServiceCollection ConfigureAuthorization(this IServiceCollection services)
    {
        services.AddAuthorizationBuilder()
            .AddPolicy("AdminPolicy", policy =>
                policy.RequireRole("Admin"))
            .AddPolicy("EVMStaffPolicy", policy =>
                policy.RequireRole("Admin", "EVM Staff"))
            .AddPolicy("DealerManagerPolicy", policy =>
                policy.RequireRole("Admin", "EVM Staff", "Dealer Manager"))
            .AddPolicy("DealerStaffPolicy", policy =>
                policy.RequireRole("Admin", "EVM Staff", "Dealer Manager", "Dealer Staff"));

        return services;
    }
}
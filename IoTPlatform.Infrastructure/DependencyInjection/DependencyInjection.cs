using IoTPlatform.Application.Interfaces;
using IoTPlatform.Infrastructure.Data;
using IoTPlatform.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace IoTPlatform.Infrastructure.DependencyInjection;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        services.AddDbContext<IoTPlatformDbContext>(options =>
            options.UseSqlServer(
                configuration.GetConnectionString("DefaultConnection")));

        services.AddScoped<IDeviceAssetRepository, DeviceAssetRepository>();
        services.AddScoped<ITelemetryMetricRepository, TelemetryMetricRepository>();
        services.AddScoped<IAlertRuleRepository, AlertRuleRepository>();
        services.AddScoped<IMaintenanceLogRepository, MaintenanceLogRepository>();

        return services;
    }
}
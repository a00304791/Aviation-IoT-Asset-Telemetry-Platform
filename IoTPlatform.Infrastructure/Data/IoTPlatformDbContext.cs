

using IoTPlatform.Domain.Entities;
using IoTPlatform.Infrastructure.Data.Configurations;
using Microsoft.EntityFrameworkCore;

namespace IoTPlatform.Infrastructure.Data;

public class IoTPlatformDbContext : DbContext
{
    public IoTPlatformDbContext(
        DbContextOptions<IoTPlatformDbContext> options)
        : base(options)
    {
    }

    public DbSet<DeviceAsset> DeviceAssets => Set<DeviceAsset>();

    public DbSet<TelemetryMetric> TelemetryMetrics => Set<TelemetryMetric>();

    public DbSet<AlertRule> AlertRules => Set<AlertRule>();

    public DbSet<MaintenanceLog> MaintenanceLogs => Set<MaintenanceLog>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.ApplyConfiguration(new DeviceAssetConfiguration());
        modelBuilder.ApplyConfiguration(new TelemetryMetricConfiguration());
        modelBuilder.ApplyConfiguration(new AlertRuleConfiguration());
        modelBuilder.ApplyConfiguration(new MaintenanceLogConfiguration());
    }
}

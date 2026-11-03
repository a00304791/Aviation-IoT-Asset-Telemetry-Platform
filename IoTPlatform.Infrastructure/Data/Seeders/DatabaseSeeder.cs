using IoTPlatform.Domain.Entities;
using IoTPlatform.Domain.ValueObjects;
using Microsoft.EntityFrameworkCore;

namespace IoTPlatform.Infrastructure.Data.Seeders;

public class DatabaseSeeder
{
    private readonly IoTPlatformDbContext _context;

    public DatabaseSeeder(IoTPlatformDbContext context)
    {
        _context = context;
    }

    public async Task SeedDeviceAssetsAsync(string seedDataDirectory)
    {
        // Avoid inserting the same assets more than once.
        if (await _context.DeviceAssets.AnyAsync())
            return;

        string filePath = Path.Combine(
            seedDataDirectory,
            "device-assets.json");

        var records =
            await JsonSeedLoader.LoadAsync<DeviceAssetSeedData>(filePath);

        foreach (var record in records)
        {
            var asset = new DeviceAsset(
                new SerialNumber(record.SerialNumber),
                record.Name,
                record.ComponentType,
                new OperatingRange(
                    record.MinimumOperatingTemperature,
                    record.MaximumOperatingTemperature));

            await _context.DeviceAssets.AddAsync(asset);
        }

        await _context.SaveChangesAsync();
    }

public async Task SeedTelemetryMetricsAsync(string seedDataDirectory)
{
    if (await _context.TelemetryMetrics.AnyAsync())
        return;

    string filePath = Path.Combine(
        seedDataDirectory,
        "telemetry-metrics.json");

    var records =
        await JsonSeedLoader.LoadAsync<TelemetryMetricSeedData>(filePath);

    foreach (var record in records)
    {
        var deviceAsset = await _context.DeviceAssets
            .SingleOrDefaultAsync(d =>
                d.SerialNumber.Value == record.DeviceSerialNumber);

        if (deviceAsset is null)
        {
            throw new InvalidOperationException(
                $"Device asset not found: {record.DeviceSerialNumber}");
        }

        var metric = new TelemetryMetric(
            deviceAsset.Id,
            record.MetricType,
            record.Value,
            record.Unit,
            record.RecordedAt);

        await _context.TelemetryMetrics.AddAsync(metric);
    }

    await _context.SaveChangesAsync();
}

private class TelemetryMetricSeedData
{
    public string DeviceSerialNumber { get; set; } = string.Empty;
    public TelemetryMetricType MetricType { get; set; }
    public decimal Value { get; set; }
    public string Unit { get; set; } = string.Empty;
    public DateTime RecordedAt { get; set; }
}

public async Task SeedAlertRulesAsync(string seedDataDirectory)
{
    if (await _context.AlertRules.AnyAsync())
        return;

    string filePath = Path.Combine(
        seedDataDirectory,
        "alert-rules.json");

    var records =
        await JsonSeedLoader.LoadAsync<AlertRuleSeedData>(filePath);

    foreach (var record in records)
    {
        var deviceAsset = await _context.DeviceAssets
            .SingleOrDefaultAsync(d =>
                d.SerialNumber.Value == record.DeviceSerialNumber);

        if (deviceAsset is null)
        {
            throw new InvalidOperationException(
                $"Device asset not found: {record.DeviceSerialNumber}");
        }

        var alertRule = new AlertRule(
            deviceAsset.Id,
            record.Name,
            record.MetricType,
            new OperatingRange(
                record.MinimumThreshold,
                record.MaximumThreshold));

        if (!record.IsEnabled)
            alertRule.Disable();

        await _context.AlertRules.AddAsync(alertRule);
    }

    await _context.SaveChangesAsync();
}

private class AlertRuleSeedData
{
    public string DeviceSerialNumber { get; set; } = string.Empty;
    public string Name { get; set; } = string.Empty;
    public TelemetryMetricType MetricType { get; set; }
    public decimal MinimumThreshold { get; set; }
    public decimal MaximumThreshold { get; set; }
    public bool IsEnabled { get; set; }
}

public async Task SeedMaintenanceLogsAsync(string seedDataDirectory)
{
    if (await _context.MaintenanceLogs.AnyAsync())
        return;

    string filePath = Path.Combine(
        seedDataDirectory,
        "maintenance-logs.json");

    var records =
        await JsonSeedLoader.LoadAsync<MaintenanceLogSeedData>(filePath);

    foreach (var record in records)
    {
        var deviceAsset = await _context.DeviceAssets
            .SingleOrDefaultAsync(d =>
                d.SerialNumber.Value == record.DeviceSerialNumber);

        if (deviceAsset is null)
        {
            throw new InvalidOperationException(
                $"Device asset not found: {record.DeviceSerialNumber}");
        }

        var maintenanceLog = new MaintenanceLog(
            deviceAsset.Id,
            record.MaintenanceType,
            record.Description,
            record.PerformedAt,
            record.TechnicianName);

        await _context.MaintenanceLogs.AddAsync(maintenanceLog);
    }

    await _context.SaveChangesAsync();
}

private class MaintenanceLogSeedData
{
    public string DeviceSerialNumber { get; set; } = string.Empty;
    public string MaintenanceType { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public DateTime PerformedAt { get; set; }
    public string TechnicianName { get; set; } = string.Empty;
}

public async Task SeedAllAsync(string seedDataDirectory)
{
    await SeedDeviceAssetsAsync(seedDataDirectory);
    await SeedTelemetryMetricsAsync(seedDataDirectory);
    await SeedAlertRulesAsync(seedDataDirectory);
    await SeedMaintenanceLogsAsync(seedDataDirectory);
}

    private class DeviceAssetSeedData
    {
        public string SerialNumber { get; set; } = string.Empty;
        public string Name { get; set; } = string.Empty;
        public string ComponentType { get; set; } = string.Empty;
        public decimal MinimumOperatingTemperature { get; set; }
        public decimal MaximumOperatingTemperature { get; set; }
    }
}
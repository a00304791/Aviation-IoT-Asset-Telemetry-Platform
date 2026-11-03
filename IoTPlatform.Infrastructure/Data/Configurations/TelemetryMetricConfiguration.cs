
using IoTPlatform.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace IoTPlatform.Infrastructure.Data.Configurations;

public class TelemetryMetricConfiguration
    : IEntityTypeConfiguration<TelemetryMetric>
{
    public void Configure(EntityTypeBuilder<TelemetryMetric> builder)
    {
        // Database table
        builder.ToTable("TelemetryMetrics");

        // Primary key
        builder.HasKey(t => t.Id);

        // Device asset foreign key
        builder.Property(t => t.DeviceAssetId)
            .IsRequired();

        builder.HasOne<DeviceAsset>()
            .WithMany()
            .HasForeignKey(t => t.DeviceAssetId)
            .OnDelete(DeleteBehavior.Restrict);

        // Metric type
        builder.Property(t => t.MetricType)
            .HasConversion<string>()
            .IsRequired();

        // Measurement value
        builder.Property(t => t.Value)
            .HasPrecision(18, 4)
            .IsRequired();

        // Measurement unit
        builder.Property(t => t.Unit)
            .IsRequired()
            .HasMaxLength(50);

        // Timestamp
        builder.Property(t => t.RecordedAt)
            .IsRequired();

        // Index for device telemetry history
        builder.HasIndex(t => new { t.DeviceAssetId, t.RecordedAt });
    }
}

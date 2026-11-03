
using IoTPlatform.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace IoTPlatform.Infrastructure.Data.Configurations;

public class AlertRuleConfiguration
    : IEntityTypeConfiguration<AlertRule>
{
    public void Configure(EntityTypeBuilder<AlertRule> builder)
    {
        // Database table and threshold constraints
        builder.ToTable("AlertRules", table =>
        {
            table.HasCheckConstraint(
                "CK_AlertRules_ThresholdRange",
                "[MinimumThreshold] > 0 AND [MaximumThreshold] > [MinimumThreshold]");
        });

        // Primary key
        builder.HasKey(a => a.Id);

        // Device asset foreign key
        builder.Property(a => a.DeviceAssetId)
            .IsRequired();

        builder.HasOne<DeviceAsset>()
            .WithMany()
            .HasForeignKey(a => a.DeviceAssetId)
            .OnDelete(DeleteBehavior.Restrict);

        // Alert name
        builder.Property(a => a.Name)
            .IsRequired()
            .HasMaxLength(100);

        // Metric type
        builder.Property(a => a.MetricType)
            .HasConversion<string>()
            .IsRequired();

        // Threshold range value object
        builder.OwnsOne(a => a.ThresholdRange, range =>
        {
            range.Property(r => r.Minimum)
                .HasColumnName("MinimumThreshold")
                .HasPrecision(10, 2)
                .IsRequired();

            range.Property(r => r.Maximum)
                .HasColumnName("MaximumThreshold")
                .HasPrecision(10, 2)
                .IsRequired();
        });

        // Enabled or disabled
        builder.Property(a => a.IsEnabled)
            .IsRequired();

        // Index for finding rules belonging to an asset
        builder.HasIndex(a => a.DeviceAssetId);
    }
}

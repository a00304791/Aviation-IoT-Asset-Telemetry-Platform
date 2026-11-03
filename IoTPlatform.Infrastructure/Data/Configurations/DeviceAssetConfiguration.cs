
using IoTPlatform.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace IoTPlatform.Infrastructure.Data.Configurations;

public class DeviceAssetConfiguration : IEntityTypeConfiguration<DeviceAsset>
{
    public void Configure(EntityTypeBuilder<DeviceAsset> builder)
    {
        builder.ToTable("DeviceAssets");
        // Primary key
        builder.HasKey(d => d.Id);

        // Serial number value object
        builder.Property(d => d.SerialNumber)
            .HasConversion(
                serialNumber => serialNumber.Value,
                value => new IoTPlatform.Domain.ValueObjects.SerialNumber(value))
            .HasMaxLength(50)
            .IsRequired();

        builder.HasIndex(d => d.SerialNumber)
            .IsUnique();

        // Asset name
        builder.Property(d => d.Name)
            .IsRequired()
            .HasMaxLength(100);

        // Component type
        builder.Property(d => d.ComponentType)
            .IsRequired()
            .HasMaxLength(100);

        // Device status enum
        builder.Property(d => d.Status)
            .HasConversion<string>()
            .IsRequired();

        // Operating temperature range value object
        builder.OwnsOne(d => d.OperatingTemperatureRange, range =>
        {
            range.Property(r => r.Minimum)
                .HasColumnName("MinimumOperatingTemperature")
                .HasPrecision(10, 2)
                .IsRequired();

            range.Property(r => r.Maximum)
                .HasColumnName("MaximumOperatingTemperature")
                .HasPrecision(10, 2)
                .IsRequired();
        });
    }
}

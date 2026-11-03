
using IoTPlatform.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace IoTPlatform.Infrastructure.Data.Configurations;

public class MaintenanceLogConfiguration
    : IEntityTypeConfiguration<MaintenanceLog>
{
    public void Configure(EntityTypeBuilder<MaintenanceLog> builder)
    {
        // Database table
        builder.ToTable("MaintenanceLogs");

        // Primary key
        builder.HasKey(m => m.Id);

        // Device asset foreign key
        builder.Property(m => m.DeviceAssetId)
            .IsRequired();

        builder.HasOne<DeviceAsset>()
            .WithMany()
            .HasForeignKey(m => m.DeviceAssetId)
            .OnDelete(DeleteBehavior.Restrict);

        // Maintenance type
        builder.Property(m => m.MaintenanceType)
            .IsRequired()
            .HasMaxLength(100);

        // Description
        builder.Property(m => m.Description)
            .IsRequired()
            .HasMaxLength(1000);

        // Date performed
        builder.Property(m => m.PerformedAt)
            .IsRequired();

        // Technician name
        builder.Property(m => m.TechnicianName)
            .IsRequired()
            .HasMaxLength(150);

        // Indexes
        builder.HasIndex(m => m.DeviceAssetId);

        builder.HasIndex(m => m.PerformedAt);
    }
}

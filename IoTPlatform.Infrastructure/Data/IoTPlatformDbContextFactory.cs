using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace IoTPlatform.Infrastructure.Data;

public class IoTPlatformDbContextFactory
    : IDesignTimeDbContextFactory<IoTPlatformDbContext>
{
    public IoTPlatformDbContext CreateDbContext(string[] args)
    {
        var optionsBuilder =
            new DbContextOptionsBuilder<IoTPlatformDbContext>();

        optionsBuilder.UseSqlServer(
            "Server=(localdb)\\MSSQLLocalDB;Database=AviationIoTPlatform;Trusted_Connection=True;TrustServerCertificate=True;");

        return new IoTPlatformDbContext(optionsBuilder.Options);
    }
}
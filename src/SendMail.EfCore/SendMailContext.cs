using System.Text.Json;
using Microsoft.EntityFrameworkCore;

namespace SendMail.EfCore;

public class SendMailContext : DbContext
{
    public SendMailContext(DbContextOptions options) : base(options)
    {
    }

    public DbSet<ServicePermission> ServicePermissions { get; init; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<ServicePermission>(s =>
        {
            s.Property(e => e.Id)
             .HasConversion(v => v.Value, v => new ApiKey(v));
            s.Property(e => e.Data)
             .HasColumnType("json")
             .HasConversion(v => JsonSerializer.Serialize(v, new JsonSerializerOptions()),
                            v => JsonSerializer.Deserialize<ServicePermissionData>(v, new JsonSerializerOptions()));
        });
    }
}

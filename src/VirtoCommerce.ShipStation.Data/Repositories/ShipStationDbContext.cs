using EntityFrameworkCore.Triggers;
using Microsoft.EntityFrameworkCore;

namespace VirtoCommerce.ShipStation.Data.Repositories;

public class ShipStationDbContext : DbContextWithTriggers
{
    public ShipStationDbContext(DbContextOptions<ShipStationDbContext> options)
      : base(options)
    {
    }

    protected ShipStationDbContext(DbContextOptions options)
        : base(options)
    {
    }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        //        modelBuilder.Entity<ShipStationEntity>().ToTable("MyModule").HasKey(x => x.Id);
        //        modelBuilder.Entity<ShipStationEntity>().Property(x => x.Id).HasMaxLength(128);
        //        base.OnModelCreating(modelBuilder);
    }
}

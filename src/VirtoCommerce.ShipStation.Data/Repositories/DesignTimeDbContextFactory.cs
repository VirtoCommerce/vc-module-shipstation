using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace VirtoCommerce.ShipStation.Data.Repositories;

public class DesignTimeDbContextFactory : IDesignTimeDbContextFactory<ShipStationDbContext>
{
    public ShipStationDbContext CreateDbContext(string[] args)
    {
        var builder = new DbContextOptionsBuilder<ShipStationDbContext>();

        builder.UseSqlServer("Data Source=(local);Initial Catalog=VirtoCommerce3;Persist Security Info=True;User ID=virto;Password=virto;MultipleActiveResultSets=True;Connect Timeout=30");

        return new ShipStationDbContext(builder.Options);
    }
}

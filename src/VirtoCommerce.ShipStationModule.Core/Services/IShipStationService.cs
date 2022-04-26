using System;
using System.Threading.Tasks;
using VirtoCommerce.OrdersModule.Core.Model;
using VirtoCommerce.ShipStationModule.Core.Models;

namespace VirtoCommerce.ShipStationModule.Core.Services;

public interface IShipStationService
{
    Task<ShipStationOrdersResponse> GetOrdersAsync(string storeId, DateTime startDate, DateTime endDate, int page);

    Task<CustomerOrder> UpdateOrderAsync(ShipNotice shipNotice);
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VirtoCommerce.OrdersModule.Core.Model;
using VirtoCommerce.OrdersModule.Core.Model.Search;
using VirtoCommerce.OrdersModule.Core.Services;
using VirtoCommerce.ShipStationModule.Core.Models;
using VirtoCommerce.ShipStationModule.Core.Services;
using VirtoCommerce.ShipStationModule.Data.Extensions;

namespace VirtoCommerce.ShipStationModule.Data.Services;

public class ShipStationService : IShipStationService
{
    private readonly ICustomerOrderSearchService _customerOrderSearchService;
    private readonly ICustomerOrderService _customerOrderService;

    public ShipStationService(
        ICustomerOrderSearchService customerOrderSearchService,
        ICustomerOrderService customerOrderService
        )
    {
        _customerOrderSearchService = customerOrderSearchService;
        _customerOrderService = customerOrderService;
    }

    public virtual async Task<ShipStationOrdersResponse> GetOrdersAsync(string storeId, DateTime startDate, DateTime endDate, int page)
    {
        var customerOrderSearchCriteria = new CustomerOrderSearchCriteria
        {
            StartDate = startDate,
            EndDate = endDate,
            StoreIds = new[] { storeId },
            ResponseGroup = CustomerOrderResponseGroup.Full.ToString(),
        };

        if (page > 1)
        {
            customerOrderSearchCriteria.Skip = customerOrderSearchCriteria.Take * (page - 1);
        }

        var searchResult = await _customerOrderSearchService.SearchAsync(customerOrderSearchCriteria);

        var result = new ShipStationOrdersResponse
        {
            Order = searchResult.Results.Select(ToShipStationOrder).ToArray(),
            Pages = (int)Math.Round((decimal)searchResult.TotalCount / customerOrderSearchCriteria.Take, MidpointRounding.ToPositiveInfinity),
        };

        return result;
    }

    public virtual async Task<CustomerOrder> UpdateOrderAsync(ShipNotice shipNotice)
    {
        var customerOrderSearchCriteria = new CustomerOrderSearchCriteria
        {
            Number = shipNotice.OrderNumber,
            ResponseGroup = CustomerOrderResponseGroup.Full.ToString(),
        };

        var updatedOrder = (await _customerOrderSearchService.SearchAsync(customerOrderSearchCriteria)).Results.FirstOrDefault();

        if (updatedOrder is not null)
        {
            updatedOrder = PatchCustomerOrder(updatedOrder, shipNotice);

            await _customerOrderService.SaveChangesAsync(new List<CustomerOrder> { updatedOrder });
        }

        return updatedOrder;
    }


    protected virtual CustomerOrder PatchCustomerOrder(CustomerOrder customerOrder, ShipNotice shipNotice)
    {
        return customerOrder.Patch(shipNotice);
    }

    protected virtual ShipStationOrder ToShipStationOrder(CustomerOrder order)
    {
        return order.ToShipStationOrder();
    }
}

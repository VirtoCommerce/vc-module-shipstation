using System;
using System.Linq;
using System.Threading.Tasks;
using VirtoCommerce.OrdersModule.Core.Model;
using VirtoCommerce.OrdersModule.Core.Model.Search;
using VirtoCommerce.Platform.Core.GenericCrud;
using VirtoCommerce.ShipStationModule.Core.Models;
using VirtoCommerce.ShipStationModule.Core.Services;
using VirtoCommerce.ShipStationModule.Data.Extensions;

namespace VirtoCommerce.ShipStationModule.Data.Services;

public class ShipStationService : IShipStationService
{
    private readonly ISearchService<CustomerOrderSearchCriteria, CustomerOrderSearchResult, CustomerOrder> _customerOrderSearchService;
    private readonly ICrudService<CustomerOrder> _customerOrderService;

    public ShipStationService(
        ISearchService<CustomerOrderSearchCriteria, CustomerOrderSearchResult, CustomerOrder> customerOrderSearchService,
        ICrudService<CustomerOrder> customerOrderService
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

    protected virtual ShipStationOrder ToShipStationOrder(CustomerOrder order)
    {
        return order.ToShipStationOrder();
    }

    public virtual Task UpdateOrderAsync()
    {
        return Task.CompletedTask;
    }
}

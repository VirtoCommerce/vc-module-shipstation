using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using VirtoCommerce.OrdersModule.Core.Model;
using VirtoCommerce.OrdersModule.Core.Model.Search;
using VirtoCommerce.Platform.Core.Common;
using VirtoCommerce.Platform.Core.GenericCrud;
using VirtoCommerce.ShipStationModule.Core.Models;
using VirtoCommerce.ShipStationModule.Data.Extensions;
using VirtoCommerce.ShipStationModule.Web.Attributes;

namespace VirtoCommerce.ShipStationModule.Web.Controllers.Api;

[Route("api/shipstation/{storeId}")]
[ControllerXmlResponse]
public class ShipStationController : Controller
{
    private readonly ISearchService<CustomerOrderSearchCriteria, CustomerOrderSearchResult, CustomerOrder>
        _customerOrderSearchService;

    private readonly ICrudService<CustomerOrder> _customerOrderService;

    public ShipStationController(
        ISearchService<CustomerOrderSearchCriteria, CustomerOrderSearchResult, CustomerOrder>
            customerOrderSearchService,
        ICrudService<CustomerOrder> customerOrderService)
    {
        _customerOrderSearchService = customerOrderSearchService;
        _customerOrderService = customerOrderService;
    }

    [HttpGet]
    [Route("")]
    // TODO: Add authorization
    public async Task<ActionResult<string>> GetOrders([FromRoute] string storeId, [FromQuery] string action,
        [FromQuery] string start_date, [FromQuery] string end_date, [FromQuery] int page)
    {
        if (action.EqualsInvariant("export"))
        {
            var customerOrderSearchCriteria = new CustomerOrderSearchCriteria
            {
                StartDate = DateTime.Parse(start_date),
                EndDate = DateTime.Parse(end_date),
                StoreIds = new[] { storeId },
            };

            if (page > 1)
            {
                customerOrderSearchCriteria.Skip = customerOrderSearchCriteria.Take * (page - 1);
            }

            var searchResult = await _customerOrderSearchService.SearchAsync(customerOrderSearchCriteria);

            var result = new ShipStationOrdersResponse
            {
                Order = searchResult.Results.Select(x => x.ToShipStationOrder()).ToArray(),
                Pages = (int)Math.Round((decimal)(searchResult.TotalCount / customerOrderSearchCriteria.Take), MidpointRounding.ToPositiveInfinity),
            };

            return Ok(result);
        }

        return BadRequest();
    }

    [HttpPost]
    [Route("")]
    public async Task<ActionResult> UpdateOrder([FromRoute] string storeId, [FromRoute] string order_number,
        [FromRoute] string carrier, [FromRoute] string service, [FromRoute] string tracking_number,
        [FromBody] ShipNotice shipNotice)
    {
        var customerOrderSearchCriteria = new CustomerOrderSearchCriteria();

        var customerOrderSearchResult = await _customerOrderSearchService.SearchAsync(customerOrderSearchCriteria);

        var customerOrder = customerOrderSearchResult.Results.FirstOrDefault();

        if (customerOrder != null)
        {

        }

        return Ok();
    }

}

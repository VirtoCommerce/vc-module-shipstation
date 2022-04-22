using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using VirtoCommerce.Platform.Core.Common;
using VirtoCommerce.ShipStationModule.Core.Models;
using VirtoCommerce.ShipStationModule.Core.Services;
using VirtoCommerce.ShipStationModule.Web.Attributes;

namespace VirtoCommerce.ShipStationModule.Web.Controllers.Api;

[Route("api/shipstation/{storeId}")]
[ControllerXmlResponse]
public class ShipStationController : Controller
{
    private readonly IShipStationService _shipStationService;

    public ShipStationController(IShipStationService shipStationService)
    {
        _shipStationService = shipStationService;
    }

    [HttpGet]
    [Route("")]
    // TODO: Add authorization
    public async Task<ActionResult<string>> GetOrders([FromRoute] string storeId, [FromQuery] string action,
        [FromQuery] string start_date, [FromQuery] string end_date, [FromQuery] int page)
    {
        if (action.EqualsInvariant("export"))
        {
            var startDate = DateTime.Parse(start_date);
            var endDate = DateTime.Parse(end_date);

            var result = await _shipStationService.GetOrdersAsync(storeId, startDate, endDate, page);

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
        return Ok();
    }

}

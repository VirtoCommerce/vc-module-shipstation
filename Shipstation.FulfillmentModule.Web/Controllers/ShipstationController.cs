using Shipstation.FulfillmentModule.Web.Converters;
using Shipstation.FulfillmentModule.Web.Models.Notice;
using Shipstation.FulfillmentModule.Web.Models.Order;
using Shipstation.FulfillmentModule.Web.Services;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web.Http;
using System.Web.Http.Description;
using VirtoCommerce.Domain.Order.Model;
using VirtoCommerce.Domain.Order.Services;

namespace Shipstation.FulfillmentModule.Web.Controllers
{
    [ApiExplorerSettings(IgnoreApi = true)]
    [RoutePrefix("api/fulfillment/shipstation")]
    [ControllerConfig]
    public class ShipstationController : ApiController
    {
        private readonly ICustomerOrderService _orderService;
        private readonly ICustomerOrderSearchService _orderSearchService;

        public ShipstationController(ICustomerOrderService orderService, ICustomerOrderSearchService orderSearchService)
        {
            _orderSearchService = orderSearchService;
            _orderService = orderService;
        }

        [HttpGet]
        [Route("")]
        [ResponseType(typeof(Orders))]
        [IdentityBasicAuthentication]
        public IHttpActionResult GetNewOrders(string action, string start_date, string end_date, int page)
        {
            if (action == "export")
            {
                var shipstationOrders = new Orders();

                var searchCriteria = new CustomerOrderSearchCriteria{};

                if (start_date != null)
                    searchCriteria.StartDate = DateTime.Parse(start_date, new CultureInfo("en-US"));

                if (end_date != null)
                    searchCriteria.EndDate = DateTime.Parse(end_date, new CultureInfo("en-US"));

                //if page more than 1 shipstation requests second or later page to be returned. move start position to that page.
                if (page > 1)
                {
                    searchCriteria.Skip += searchCriteria.Take * (page - 1);
                }

                var searchResult = _orderSearchService.SearchCustomerOrders(searchCriteria);

                if (searchResult.Results != null && searchResult.Results.Any())
                {
                    var shipstationOrdersList = new List<OrdersOrder>();
                    foreach (var order in searchResult.Results) {
                        shipstationOrdersList.Add(order.ToShipstationOrder());
                    };
                    shipstationOrders.Order = shipstationOrdersList.ToArray();

                    //if first page was requested and total orders more than returned add to response overall pages count that shipstation should request.
                    if ((page == 1) && searchResult.Results.Count() > searchCriteria.Skip)
                    {
                        shipstationOrders.pages = (short)(searchResult.Results.Count() / searchCriteria.Skip);
                        shipstationOrders.pages += (short)(searchResult.Results.Count() % searchCriteria.Skip == 0 ? 0 : 1);
                        shipstationOrders.pagesSpecified = true;
                    }
                }

                return Ok(shipstationOrders);
            }

            return BadRequest();
        }

        [HttpPost]
        [Route("")]
        [IdentityBasicAuthentication]
        public IHttpActionResult UpdateOrders(string action, string order_number, string carrier, string service, string tracking_number, ShipNotice shipnotice)
        {
            var searchCriteria = new CustomerOrderSearchCriteria {
                Number = shipnotice.OrderNumber
            };
            var order = _orderSearchService.SearchCustomerOrders(searchCriteria);

            if (order == null)
            {
                return BadRequest("Order not found");
            }

            var updatedOrder = _orderService.GetByIds(new[] { order.Results.FirstOrDefault().Id }).FirstOrDefault();

            updatedOrder.Patch(shipnotice);

            _orderService.SaveChanges(new[] { updatedOrder });
            return Ok(shipnotice);
        }
    }
}

using System.Linq;
using System.Xml;
using VirtoCommerce.CoreModule.Core.Common;
using VirtoCommerce.OrdersModule.Core.Model;
using VirtoCommerce.Platform.Core.Common;
using VirtoCommerce.ShipStationModule.Core.Models;

namespace VirtoCommerce.ShipStationModule.Data.Extensions;

public static class CustomerOrderConverter
{
    public static ShipStationOrder ToShipStationOrder(this CustomerOrder customerOrder)
    {
        var result = new ShipStationOrder
        {
            OrderNumber = ToCDataSection(customerOrder.Number),
            OrderId = ToCDataSection(customerOrder.Id),
            OrderStatus = ToCDataSection(customerOrder.Status),
            OrderDate = customerOrder.CreatedDate.ToString("MM'/'dd'/'yyyy HH:mm"),
            LastModified = GetLastModified(customerOrder),
            OrderTotal = customerOrder.Sum,
            ShippingAmount = customerOrder.Shipments.Sum(x => x.Sum),
            TaxAmount = customerOrder.TaxTotal,
            ShippingMethod = ToCDataSection(GetShippingMethodCode(customerOrder)),
            PaymentMethod = ToCDataSection(GetPaymentMethod(customerOrder)),
            Items = customerOrder
                .Shipments
                .Where(x => !x.Items.IsNullOrEmpty())
                .SelectMany(x => x.Items.Select(y =>
                {
                    var lineItem = y.LineItem;

                    var item = new ShipStationItem
                    {
                        Sku = ToCDataSection(lineItem.Sku),
                        ImageUrl = ToCDataSection(lineItem.ImageUrl),
                        Name = ToCDataSection(lineItem.Name),
                        Quantity = lineItem.Quantity,
                        UnitPrice = GetUnitPrice(customerOrder, lineItem),
                        Weight = GetWeight(customerOrder, lineItem),
                        WeightUnits = GetWeightUnit(customerOrder, lineItem),
                    };

                    return item;
                }))
                .ToArray(),
            Customer = new ShipStationCustomer { CustomerCode = ToCDataSection(customerOrder.CustomerId), }
        };

        if (!customerOrder.Addresses.IsNullOrEmpty())
        {
            var billAddress = customerOrder.Addresses.FirstOrDefault(x => x.AddressType is AddressType.Billing or AddressType.BillingAndShipping) ??
                              customerOrder.Addresses.FirstOrDefault();

            if (billAddress is not null)
            {
                result.Customer.BillTo = new ShipStationBillTo
                {
                    Company = ToCDataSection(billAddress.Organization),
                    Name = ToCDataSection($"{billAddress.FirstName} {billAddress.LastName}"),
                    Phone = ToCDataSection(billAddress.Phone),
                };
            }

            var shipAddress = customerOrder.Addresses.FirstOrDefault(x => x.AddressType is AddressType.Shipping or AddressType.BillingAndShipping);

            if (shipAddress is not null)
            {
                result.Customer.ShipTo = new ShipStationShipTo
                {
                    Company = ToCDataSection(shipAddress.Organization),
                    Name = ToCDataSection($"{shipAddress.FirstName} {shipAddress.LastName}"),
                    Phone = ToCDataSection(shipAddress.Phone),
                    Address1 = ToCDataSection(shipAddress.Line1),
                    Address2 = ToCDataSection(shipAddress.Line2),
                    City = ToCDataSection(shipAddress.City),
                    PostalCode = ToCDataSection(shipAddress.PostalCode),
                    Country = ToCDataSection(shipAddress.CountryCode.ConvertToTwoLetterCountryCode()),
                    State = ToCDataSection(shipAddress.RegionId ?? shipAddress.RegionName),
                };
            }
        }

        return result;
    }

    public static CustomerOrder Patch(this CustomerOrder customerOrder, ShipNotice shipNotice)
    {
        if (!customerOrder.Shipments.IsNullOrEmpty())
        {
            foreach (var shipment in customerOrder.Shipments)
            {
                shipment.Status = "Sent";
                shipment.Number = shipNotice.TrackingNumber;
            }

            customerOrder.Status = "Completed";
        }

        return customerOrder;
    }


    private static XmlCDataSection ToCDataSection(string value)
    {
        return new XmlDocument().CreateCDataSection(value);
    }

    private static string GetLastModified(CustomerOrder customerOrder) =>
        customerOrder.ModifiedDate?.ToString("MM'/'dd'/'yyyy HH:mm") ??
        customerOrder.CreatedDate.ToString("MM'/'dd'/'yyyy HH:mm");

    private static string GetShippingMethodCode(CustomerOrder customerOrder) => customerOrder.Shipments.FirstOrDefault()?.ShipmentMethodCode;

    private static string GetPaymentMethod(CustomerOrder customerOrder) => customerOrder.InPayments.FirstOrDefault()?.GatewayCode;

    private static decimal GetUnitPrice(CustomerOrder customerOrder, LineItem lineItem) => customerOrder.Items
        .SingleOrDefault(i => i.ProductId.EqualsInvariant(lineItem.ProductId))?.Price ??
        decimal.Zero;

    private static decimal GetWeight(CustomerOrder customerOrder, LineItem lineItem) => customerOrder.Items
        .SingleOrDefault(i => i.ProductId.EqualsInvariant(lineItem.ProductId))?.Weight ??
        decimal.Zero;

    private static string GetWeightUnit(CustomerOrder customerOrder, LineItem lineItem) => customerOrder.Items
        .SingleOrDefault(i => i.ProductId.EqualsInvariant(lineItem.ProductId))?.WeightUnit;
}

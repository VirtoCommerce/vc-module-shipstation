using System.Linq;
using System.Xml;
using VirtoCommerce.CoreModule.Core.Common;
using VirtoCommerce.OrdersModule.Core.Model;
using VirtoCommerce.Platform.Core.Common;
using VirtoCommerce.ShipStationModule.Core;
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
            OrderDate = customerOrder.CreatedDate.ToString(ModuleConstants.DateTimeFormat),
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
            Customer = new ShipStationCustomer
            {
                CustomerCode = ToCDataSection(customerOrder.CustomerId),
                BillTo = new ShipStationBillTo(),
                ShipTo = new ShipStationShipTo(),
            }
        };

        if (!customerOrder.Addresses.IsNullOrEmpty())
        {
            var billAddress = customerOrder.Addresses.FirstOrDefault(x => x.AddressType is AddressType.Billing or AddressType.BillingAndShipping) ??
                              customerOrder.Addresses.FirstOrDefault();

            if (billAddress is not null)
            {
                result.Customer.BillTo.Company = ToCDataSection(billAddress.Organization);
                result.Customer.BillTo.Name = ToCDataSection($"{billAddress.FirstName} {billAddress.LastName}");
                result.Customer.BillTo.Phone = ToCDataSection(billAddress.Phone);
            }

            var shipAddress = customerOrder.Addresses.FirstOrDefault(x => x.AddressType is AddressType.Shipping or AddressType.BillingAndShipping);

            if (shipAddress is not null)
            {
                result.Customer.ShipTo.Company = ToCDataSection(shipAddress.Organization);
                result.Customer.ShipTo.Name = ToCDataSection($"{shipAddress.FirstName} {shipAddress.LastName}");
                result.Customer.ShipTo.Phone = ToCDataSection(shipAddress.Phone);
                result.Customer.ShipTo.Address1 = ToCDataSection(shipAddress.Line1);
                result.Customer.ShipTo.Address2 = ToCDataSection(shipAddress.Line2);
                result.Customer.ShipTo.City = ToCDataSection(shipAddress.City);
                result.Customer.ShipTo.PostalCode = ToCDataSection(shipAddress.PostalCode);
                result.Customer.ShipTo.Country = ToCDataSection(shipAddress.CountryCode.ConvertToTwoLetterCountryCode());
                result.Customer.ShipTo.State = ToCDataSection(shipAddress.RegionId ?? shipAddress.RegionName);
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
        customerOrder.ModifiedDate?.ToString(ModuleConstants.DateTimeFormat) ??
        customerOrder.CreatedDate.ToString(ModuleConstants.DateTimeFormat);

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

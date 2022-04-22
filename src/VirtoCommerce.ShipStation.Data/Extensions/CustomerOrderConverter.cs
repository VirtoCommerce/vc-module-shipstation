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
            LastModified =
                customerOrder.ModifiedDate?.ToString("MM'/'dd'/'yyyy HH:mm") ??
                customerOrder.CreatedDate.ToString("MM'/'dd'/'yyyy HH:mm"),
            OrderTotal = customerOrder.Sum,
            ShippingAmount = customerOrder.Shipments.Sum(x => x.Sum),
            TaxAmount = customerOrder.TaxTotal,
            ShippingMethod = ToCDataSection(customerOrder.Shipments.FirstOrDefault()?.ShipmentMethodCode),
            PaymentMethod = ToCDataSection(customerOrder.InPayments.FirstOrDefault()?.GatewayCode),
            Items = customerOrder
                .Shipments
                .Where(x => x.Items != null && x.Items.Any())
                .SelectMany(x => x.Items.Select(y =>
                {
                    var lineItem = y.LineItem;

                    var item = new ShipStationItem
                    {
                        Sku = ToCDataSection(lineItem.Sku),
                        ImageUrl = ToCDataSection(lineItem.ImageUrl),
                        Name = ToCDataSection(lineItem.Name),
                        Quantity = lineItem.Quantity,
                        UnitPrice =
                            customerOrder.Items.SingleOrDefault(i => i.ProductId.EqualsInvariant(lineItem.ProductId))
                                ?.Price ?? decimal.Zero,
                        Weight =
                            customerOrder.Items.SingleOrDefault(i => i.ProductId.EqualsInvariant(lineItem.ProductId))
                                ?.Weight ?? decimal.Zero,
                        WeightUnits = customerOrder.Items
                            .SingleOrDefault(i => i.ProductId.EqualsInvariant(lineItem.ProductId))?.WeightUnit,

                    };

                    return item;
                }))
                .ToArray(),
            Customer = new ShipStationCustomer { CustomerCode = ToCDataSection(customerOrder.CustomerId), }
        };

        if (customerOrder.Addresses is not null && customerOrder.Addresses.Any())
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

    private static XmlCDataSection ToCDataSection(string value)
    {
        return new XmlDocument().CreateCDataSection(value);
    }
}

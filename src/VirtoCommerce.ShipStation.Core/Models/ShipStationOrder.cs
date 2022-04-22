using System;
using System.Xml;
using System.Xml.Serialization;

namespace VirtoCommerce.ShipStationModule.Core.Models;

[Serializable]
public class ShipStationOrder
{
    [XmlElement("OrderID")]
    public XmlCDataSection OrderId { get; set; }

    public XmlCDataSection OrderNumber { get; set; }

    public string OrderDate { get; set; }

    public XmlCDataSection OrderStatus { get; set; }

    public string LastModified { get; set; }

    public XmlCDataSection ShippingMethod { get; set; }

    public XmlCDataSection PaymentMethod { get; set; }

    public string CurrencyCode { get; set; }

    public decimal OrderTotal { get; set; }

    public decimal TaxAmount { get; set; }

    public decimal ShippingAmount { get; set; }

    public XmlCDataSection CustomerNotes { get; set; }

    public XmlCDataSection InternalNotes { get; set; }

    public bool Gift { get; set; }

    public string GiftMessage { get; set; }

    public string CustomField1 { get; set; }

    public string CustomField2 { get; set; }

    public string CustomField3 { get; set; }

    public ShipStationCustomer Customer { get; set; }

    [XmlArrayItem("Item")]
    public ShipStationItem[] Items { get; set; }
}

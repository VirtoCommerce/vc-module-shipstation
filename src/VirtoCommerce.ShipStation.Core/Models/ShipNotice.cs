using System.Xml.Serialization;

namespace VirtoCommerce.ShipStationModule.Core.Models;

public class ShipNotice
{
    public string OrderNumber { get; set; }

    [XmlElement("OrderID")]
    public string OrderId { get; set; }

    public string CustomerCode { get; set; }

    public string CustomerNotes { get; set; }

    public string InternalNotes { get; set; }

    public string NotesToCustomer { get; set; }

    public string NotifyCustomer { get; set; }

    public string LabelCreateDate { get; set; }

    public string ShipDate { get; set; }

    public string Carrier { get; set; }

    public string Service { get; set; }

    public string TrackingNumber { get; set; }

    public decimal ShippingCost { get; set; }

    public string CustomField1 { get; set; }

    public string CustomField2 { get; set; }

    public string CustomField3 { get; set; }

    public ShipNoticeRecipient Recipient { get; set; }

    [XmlArrayItem("Item")]
    public ShipNoticeItem[] Items { get; set; }
}

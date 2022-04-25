using System.Xml.Serialization;

namespace VirtoCommerce.ShipStationModule.Core.Models;

public class ShipNoticeItem
{
    [XmlElement("SKU")]
    public string Sku { get; set; }

    public string Name { get; set; }

    public int Quantity { get; set; }

    [XmlElement("LineItemID")]
    public string LineItemId { get; set; }
}

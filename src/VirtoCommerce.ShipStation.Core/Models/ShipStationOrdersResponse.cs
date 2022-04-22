using System.Xml.Serialization;

namespace VirtoCommerce.ShipStationModule.Core.Models;

[XmlRoot("Orders")]
public class ShipStationOrdersResponse
{
    [XmlElement]
    public ShipStationOrder[] Order { get; set; }

    [XmlAttribute("pages")]
    public int Pages { get; set; }
}

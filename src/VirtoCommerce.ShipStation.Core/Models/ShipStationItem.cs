using System.Xml;
using System.Xml.Serialization;

namespace VirtoCommerce.ShipStationModule.Core.Models;

public class ShipStationItem
{
    [XmlElement("SKU")]
    public XmlCDataSection Sku { get; set; }

    public XmlCDataSection Name { get; set; }

    public XmlCDataSection ImageUrl { get; set; }

    public decimal Weight { get; set; }

    public string WeightUnits { get; set; }

    public int Quantity { get; set; }

    public decimal UnitPrice { get; set; }

    public XmlCDataSection Location { get; set; }

    [XmlArrayItem("Option")]
    public ShipStationOption[] Options { get; set; }

    public bool Adjustment { get; set; }
}

public class ShipStationOption
{
    public XmlCDataSection Name { get; set; }

    public XmlCDataSection Value { get; set; }

    public decimal Weight { get; set; }
}

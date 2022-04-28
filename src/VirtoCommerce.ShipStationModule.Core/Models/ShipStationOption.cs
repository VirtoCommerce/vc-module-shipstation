using System.Xml;

namespace VirtoCommerce.ShipStationModule.Core.Models;

public class ShipStationOption
{
    public XmlCDataSection Name { get; set; }

    public XmlCDataSection Value { get; set; }

    public decimal Weight { get; set; }
}

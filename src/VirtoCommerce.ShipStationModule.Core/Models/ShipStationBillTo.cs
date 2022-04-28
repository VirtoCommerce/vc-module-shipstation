using System.Xml;

namespace VirtoCommerce.ShipStationModule.Core.Models;

public class ShipStationBillTo
{
    public XmlCDataSection Name { get; set; }

    public XmlCDataSection Company { get; set; }

    public XmlCDataSection Phone { get; set; }

    public XmlCDataSection Email { get; set; }
}

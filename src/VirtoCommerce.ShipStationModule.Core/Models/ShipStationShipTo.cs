using System.Xml;

namespace VirtoCommerce.ShipStationModule.Core.Models;

public class ShipStationShipTo
{
    public XmlCDataSection Name { get; set; }

    public XmlCDataSection Company { get; set; }

    public XmlCDataSection Address1 { get; set; }

    public XmlCDataSection Address2 { get; set; }

    public XmlCDataSection City { get; set; }

    public XmlCDataSection State { get; set; }

    public XmlCDataSection PostalCode { get; set; }

    public XmlCDataSection Country { get; set; }

    public XmlCDataSection Phone { get; set; }

}

using System.Xml;

namespace VirtoCommerce.ShipStationModule.Core.Models;

public class ShipStationCustomer
{
    public XmlCDataSection CustomerCode { get; set; }

    public ShipStationBillTo BillTo { get; set; }

    public ShipStationShipTo ShipTo { get; set; }
}

public class ShipStationBillTo
{
    public XmlCDataSection Name { get; set; }

    public XmlCDataSection Company { get; set; }

    public XmlCDataSection Phone { get; set; }

    public XmlCDataSection Email { get; set; }
}

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

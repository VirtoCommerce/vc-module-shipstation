using System.Xml;

namespace VirtoCommerce.ShipStationModule.Core.Models;

public class ShipStationCustomer
{
    public XmlCDataSection CustomerCode { get; set; }

    public ShipStationBillTo BillTo { get; set; }

    public ShipStationShipTo ShipTo { get; set; }
}

using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace VirtoCommerce.ShipStationModule.Web.Attributes;

public class FromXmlBodyAttribute : ModelBinderAttribute
{
    public FromXmlBodyAttribute() : base(typeof(XmlBinder))
    {
        BindingSource = BindingSource.Body;
    }
}

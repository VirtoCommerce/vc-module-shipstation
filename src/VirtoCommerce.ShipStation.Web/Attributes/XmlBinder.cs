using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Formatters;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.ModelBinding.Binders;
using Microsoft.Extensions.Options;

namespace VirtoCommerce.ShipStationModule.Web.Attributes;

public class XmlBinder : IModelBinder
{
    private readonly BodyModelBinder _bodyModelBinder;

    public XmlBinder(IHttpRequestStreamReaderFactory readerFactory, IOptions<MvcOptions> options)
    {
        var mvcOptions = options.Value;
        var formatter = new XmlSerializerInputFormatter(mvcOptions);

        _bodyModelBinder = new BodyModelBinder(new IInputFormatter[] { formatter }, readerFactory);
    }

    public Task BindModelAsync(ModelBindingContext bindingContext)
    {
        return _bodyModelBinder.BindModelAsync(bindingContext);
    }
}
